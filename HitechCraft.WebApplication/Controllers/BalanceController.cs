using System.Globalization;
using HitechCraft.Core.DI;
using HitechCraft.Core.Entity;
using HitechCraft.Core.Helper;
using HitechCraft.Core.Models.Enum;
using HitechCraft.Core.Repository.Specification.IKTransaction;
using HitechCraft.Projector.Impl;

namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using System;
    using System.Collections.Generic;
    using Manager;
    using Models;
    using System.Linq;
    using BL.CQRS.Query;
    using BL.CQRS.Command;

    #endregion

    public class BalanceController : BaseController
    {
        /// <summary>
        /// Life in seconds
        /// </summary>
        public int TransactionLife => 30;

        /// <summary>
        /// Exchange rate of Rub to Gont (example 1 RUB = 1000 Gont means 0.001)
        /// </summary>
        public float RubToGont => 0.002f;

        /// <summary>
        /// Exchange rate of Gont to Rub (example 2500 Gont = 1 RUB means 2500)
        /// </summary>
        public float GontToRub => 2500f;

        public BalanceController(IContainer container) : base(container)
        {
        }

        [Authorize]
        // GET: Balance
        public ActionResult Index()
        {
            ViewBag.Rubles = this.Currency.Rubels;
            ViewBag.Gonts = this.Currency.Gonts;

            ViewBag.RublesToGonts = 1 / RubToGont;
            ViewBag.GontsToRubles = GontToRub;

            ViewBag.PaymentResult = TempData["paymentResult"];
            ViewBag.PaymentStatus = TempData["paymentStatus"];

            return View();
        }

        public string CreateOrGetTransaction()
        {
            try
            {
                var transaction = new EntityListQueryHandler<IKTransaction, IKTransactionViewModel>(this.Container)
                    .Handle(new EntityListQuery<IKTransaction, IKTransactionViewModel>()
                    {
                        Specification = new IKTransactionByPlayerNameSpec(this.Player.Name),
                        Projector = this.Container.Resolve<IProjector<IKTransaction, IKTransactionViewModel>>()
                    }).First();
                
                var newTransactionId = this.CheckTransactionTimeLife(transaction.Time, transaction.TransactionId);
                
                return newTransactionId != String.Empty ? newTransactionId : transaction.TransactionId;
            }
            catch (Exception e)
            {
                var transactionId = this.GenerateTransactionID();
                
                this.CommandExecutor.Execute(new IKTransactionCreateCommand()
                {
                    PlayerName = this.Player.Name,
                    TransactionId = transactionId
                });
                
                return transactionId;
            }
        }

        [HttpPost]
        public JsonResult ExchangeRubToGont(int count)
        {
            try
            {
                this.UpdateCurrency(Math.Round(count / this.RubToGont), -count);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = JsonStatus.NO,
                    message = e.Message
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = JsonStatus.YES,
                message = "Обмен успешно осуществлен!"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExchangeGontToRub(int count)
        {
            try
            {
                this.UpdateCurrency(-count, Math.Round(count / this.GontToRub, 2));
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = JsonStatus.NO,
                    message = e.Message
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = JsonStatus.YES,
                message = "Обмен успешно осуществлен!"
            }, JsonRequestBehavior.AllowGet);
        }

        #region IK Actions

        /// <summary>
        /// For ik actions
        /// </summary>
        /// <param name="pm">Payment params</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Payment(PaymentModel pm)
        {
            try
            {
                if (this.IsValidTransaction(pm))
                {
                    this.MoneyEnrollment(CurrencyType.Rub, float.Parse(pm.ik_am, CultureInfo.InvariantCulture), pm.ik_pm_no);
                    this.PayRefer(pm.ik_pm_no, float.Parse(pm.ik_am, CultureInfo.InvariantCulture));
                }

                LogHelper.Error(this.Player.Name + ": ошибка совершения оплаты. Невалидная транзакция " + pm.ik_pm_no, "IKPayment");
            }
            catch (Exception e)
            {
                LogHelper.Error(this.Player.Name + ": ошибка совершения оплаты: " + e.Message, "IKPayment");
            }

            return null;
        }

        /// <summary>
        /// Action that call on succeded payment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Success(string ik_pm_no)
        {
            if (ik_pm_no == null) return RedirectToAction("Index");
            
            this.UpdateTransaction(ik_pm_no);

            TempData["paymentResult"] = "Счет успешно пополнен!";
            TempData["paymentStatus"] = "OK";

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action that call on failure payment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Fail(string ik_pm_no)
        {
            if (ik_pm_no == null) return RedirectToAction("Index");

            this.UpdateTransaction(ik_pm_no);

            TempData["paymentResult"] = "Ошибка пополнения счета!";
            TempData["paymentStatus"] = "NO";

            return RedirectToAction("Index");
        }

        #endregion

        #region Private Methods for IK

        /// <summary>
        /// Check if transaction values compair IKConfig.values
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        private bool IsValidTransaction(PaymentModel pm)
        {
            var transactions = new EntityListQueryHandler<IKTransaction, IKTransactionViewModel>(this.Container)
                .Handle(new EntityListQuery<IKTransaction, IKTransactionViewModel>()
                {
                    Specification = new IKTransactionByTransactionIdSpec(pm.ik_pm_no),
                    Projector = this.Container.Resolve<IProjector<IKTransaction, IKTransactionViewModel>>()
                });
            
            if (!transactions.Any())
            {
                return false;
            }

            string generatedSign;
            
            //generate sign from pm values
            //check if test - take test key
            if (pm.ik_pw_via == "test_interkassa_test_xts")
            {
                generatedSign = this.IKSignBuilder(pm, IKConfig.IKSecretTest);
            }
            else
            {
                generatedSign = this.IKSignBuilder(pm, IKConfig.IKSecret);
            }

            return
                //check by ids
                pm.ik_co_id == IKConfig.IKID
                //&& pm.ik_pm_no == IKConfig.TransactionID
                && pm.ik_inv_st == "success"
                //check by IK sign
                && pm.ik_sign == generatedSign;
        }

        /// <summary>
        /// Build IK Sign by transaction values (and secrec key)
        /// </summary>
        /// <param name="pm"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        private string IKSignBuilder(PaymentModel pm, string secretKey)
        {
            var objects = this.GetParamsArray(pm);

            objects.Add(secretKey);

            var paramString = this.Implode(objects, ":");

            var md5 = HashHelper.GetMd5Bites(paramString);
            var base64 = HashHelper.GetBase64Hash(md5);

            return base64;
        }

        /// <summary>
        /// Returns sorted (in alphabet order) transaction values array
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        private List<string> GetParamsArray(PaymentModel pm)
        {
            //params in alphabet order by param name
            return new List<string>
            {
                //pm.ik_act,
                pm.ik_am,
                pm.ik_co_id,
                pm.ik_co_prs_id,
                pm.ik_co_rfn,
                pm.ik_cur,
                pm.ik_desc,
                pm.ik_inv_crt,
                pm.ik_inv_id,
                pm.ik_inv_prc,
                pm.ik_inv_st,
                pm.ik_pm_no,
                pm.ik_ps_price,
                pm.ik_pw_via,
                //pm.ik_sign,
                pm.ik_trn_id
            };
        }

        /// <summary>
        /// Implode (join objects) by separator
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        private string Implode(List<string> objects, string separator)
        {
            return String.Join(separator, objects);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check time life of transaction
        /// </summary>
        /// <param name="time"></param>
        /// <param name="id"></param>
        private string CheckTransactionTimeLife(DateTime time, string id)
        {
            if (DateTime.Now > time.AddSeconds(TransactionLife))
            {
                var newId = this.UpdateTransaction(id);

                return newId;
            }

            return String.Empty;
        }

        /// <summary>
        /// Generate random transaction id
        /// </summary>
        /// <returns></returns>
        private string GenerateTransactionID()
        {
            return
                HashHelper.GetMd5Hash("h4432hhdshf8924ee320fvdkshgm5i9332" + DateTime.Now + this.Player.Name);
        }

        /// <summary>
        /// Enroll money to player balance
        /// </summary>
        /// <param name="currencyType">Type of currency (rub or gont)</param>
        /// <param name="amount">Amount to enroll</param>
        /// <param name="transactionID">IK Transaction ID</param>
        private void MoneyEnrollment(CurrencyType currencyType, float amount, string transactionId)
        {
            try
            {
                switch (currencyType)
                {
                    case CurrencyType.Rub:
                        this.UpdateRubles(amount, transactionId);
                        break;
                    case CurrencyType.Gont:
                        this.UpdateGonts(amount);
                        break;
                }
            }
            catch (Exception e)
            {
                LogHelper.Error("Ошибка обновления счета! " + e.Message, "IKPayment");
            }
        }

        /// <summary>
        /// Update transaction
        /// </summary>
        /// <param name="id"></param>
        private string UpdateTransaction(string id)
        {
            var newId = this.GenerateTransactionID();
            
            this.CommandExecutor.Execute(new IKTransactionUpdateCommand()
            {
                TransactionId = id,
                NewTransactionId = newId
            });

            return newId;
        }
        
        /// <summary>
        /// Обновление осуществляется прибавлением (вычетом) значения. Указываем разницу, а не новое кол-во валюты!!!
        /// </summary>
        /// <param name="amount">Разница (например +10 или -10)</param>
        private void UpdateGonts(double amount)
        {
            this.CommandExecutor.Execute(new CurrencyUpdateCommand()
            {
                Id = this.Currency.Id,
                Gonts = amount,
                Rubles = 0
            });
        }

        /// <summary>
        /// Обновление осуществляется прибавлением (вычетом) значения. Указываем разницу, а не новое кол-во валюты!!!
        /// </summary>
        /// <param name="amount">Разница (например +10 или -10)</param>
        /// <param name="transactionId">Id of IK transaction</param>
        private void UpdateRubles(double amount, string transactionId)
        {
            this.CommandExecutor.Execute(new CurrencyUpdateCommand()
            {
                Id = this.Currency?.Id ?? 0,
                TransactionId = transactionId,
                Gonts = 0,
                Rubles = amount
            });
        }

        /// <summary>
        /// Обновление осуществляется прибавлением (вычетом) значения. Указываем разницу, а не новое кол-во валюты!!!
        /// </summary>
        /// <param name="gontsAmount">Кол-во игровой валюты</param>
        /// <param name="rublesAmount">Кол-во рублей</param>
        private void UpdateCurrency(double gontsAmount, double rublesAmount)
        {
            this.CommandExecutor.Execute(new CurrencyUpdateCommand()
            {
                Id = this.Currency.Id,
                Gonts = gontsAmount,
                Rubles = rublesAmount
            });
        }

        private void PayRefer(string transactionId, double rubleAmount)
        {
            var refererName = new EntityListQueryHandler<IKTransaction, IKTransactionViewModel>(this.Container)
                    .Handle(new EntityListQuery<IKTransaction, IKTransactionViewModel>()
                    {
                        Specification = new IKTransactionByTransactionIdSpec(transactionId),
                        Projector = this.Container.Resolve<IProjector<IKTransaction, IKTransactionViewModel>>()
                    }).First().PlayerName;

            var percents = 2;
            
            this.CommandExecutor.Execute(new ReferPayCommand()
            {
                RefererName = refererName,
                GiftPercents = percents,
                RubleAmount = rubleAmount
            });
        }

        #endregion
    }
}