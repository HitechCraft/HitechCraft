using WebApplication.Areas.Launcher.Models.Json;

namespace WebApplication.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Collections.Generic;
    using Core;
    using Managers;
    using Models;
    using Domain;

    public enum CurrencyType
    {
        Gont,
        Rub
    }

    public class BalanceController : BaseController
    {
        /// <summary>
        /// Life in seconds
        /// </summary>
        /// TODO:Подумать над временем существования транзакции
        public int TransactionLife { get { return 30; } }

        /// <summary>
        /// Exchange rate of Rub to Gont (example 1 RUB = 1000 Gont means 0.001)
        /// </summary>
        public float RubToGont { get { return 0.001f; } }

        /// <summary>
        /// Exchange rate of Gont to Rub (example 2500 Gont = 1 RUB means 2500)
        /// </summary>
        public float GontToRub { get { return 2500f; } }

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Rubles = this.UserCurrency.realmoney;
            ViewBag.Gonts = this.UserCurrency.balance;

            ViewBag.PaymentResult = TempData["paymentResult"];
            ViewBag.PaymentStatus = TempData["paymentStatus"];

            return View();
        }

        [Authorize]
        public string CreateOrGetTransaction()
        {
            try
            {
                var transaction =
                    this.context.IkTransactions.Include("Player")
                    .First(tr => tr.Player.Name == this.CurrentPlayer.Name);

                this.CheckTransactionTimeLife(transaction.Time, transaction.Id);

                return transaction.Id;
            }
            catch (Exception)
            {
                var transaction = new IKTransaction()
                {
                    //TODO: generate here
                    Id = this.GenerateTransactionID(),
                    Player = this.CurrentPlayer,
                    Time = DateTime.Now
                };

                this.context.IkTransactions.Add(transaction);
                this.context.SaveChanges();

                return transaction.Id;
            }
        }

        [HttpPost]
        public JsonResult ExchangeRubToGont(int count)
        {
            try
            {
                this.UpdateGonts(Math.Round(count / this.RubToGont));
                this.UpdateRubles(-count);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = JsonStatus.NO, message = e.Message
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = JsonStatus.YES, message = "Обмен успешно осуществлен!"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExchangeGontToRub(int count)
        {
            try
            {
                this.UpdateRubles(Math.Round(count / this.GontToRub, 2));
                this.UpdateGonts(-count);
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
            //TODO: add unick transaction ids
            if (this.IsValidTransaction(pm))
            {
                this.MoneyEnrollment(CurrencyType.Rub, float.Parse(pm.ik_am), pm.ik_pm_no);
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
            this.RemoveTransaction(ik_pm_no);

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
            this.RemoveTransaction(ik_pm_no);

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
            if (!this.context.IkTransactions.Select(ikt => ikt.Id == pm.ik_pm_no).Any())
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

            var md5 = HashManager.GetMd5Bites(paramString);
            var base64 = HashManager.GetBase64Hash(md5);

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
        private void CheckTransactionTimeLife(DateTime time, string id)
        {
            if (DateTime.Now > time.AddSeconds(TransactionLife))
            {
                this.RemoveTransaction(id);

                throw new Exception();
            }
        }

        /// <summary>
        /// Generate random transaction id
        /// </summary>
        /// <returns></returns>
        private string GenerateTransactionID()
        {
            return
                HashManager.GetMd5Hash("h4432hhdshf8924ee320fvdkshgm5i9332" + DateTime.Now);
        }

        /// <summary>
        /// Enroll money to player balance
        /// </summary>
        /// <param name="currencyType">Type of currency (rub or gont)</param>
        /// <param name="amount">Amount to enroll</param>
        /// <param name="transactionID">IK Transaction ID</param>
        private void MoneyEnrollment(CurrencyType currencyType, float amount, string transactionID)
        {
            switch (currencyType)
            {
                case CurrencyType.Rub:
                    this.UpdateRubles(amount, transactionID);
                    break;
                case CurrencyType.Gont:
                    this.UpdateGonts(amount, transactionID);
                    break;
            }
        }

        /// <summary>
        /// Remove transaction after payment
        /// </summary>
        /// <param name="id"></param>
        private void RemoveTransaction(string id)
        {
            try
            {
                var transaction = this.context.IkTransactions.First(ikt => ikt.Id == id);

                this.context.IkTransactions.Remove(transaction);
                this.context.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Returns Player currency object
        /// </summary>
        /// <param name="transactionID">IK Transaction ID</param>
        /// <returns></returns>
        private Currency GetPlayerCurrency(string transactionID)
        {
            var transaction = this.context.IkTransactions.Include("Player").FirstOrDefault(pc => pc.Id == transactionID);

            return this.context.Currencies.FirstOrDefault(c => c.username == transaction.Player.Name);
        }

        /// <summary>
        /// Обновление осуществляется прибавлением (вычетом) значения. Указываем разницу, а не новое кол-во валюты!!!
        /// </summary>
        /// <param name="amount">Разница (например +10 или -10)</param>
        private void UpdateGonts(double amount, string transactionID)
        {
            var currency = this.GetPlayerCurrency(transactionID);

            currency.balance += amount;

            context.Entry(currency).State = EntityState.Modified;
            context.SaveChanges();
        }

        private void UpdateGonts(double amount)
        {
            var currency = this.UserCurrency;

            currency.balance += amount;

            context.Entry(currency).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Обновление осуществляется прибавлением (вычетом) значения. Указываем разницу, а не новое кол-во валюты!!!
        /// </summary>
        /// <param name="amount">Разница (например +10 или -10)</param>
        private void UpdateRubles(double amount, string transactionID)
        {
            var currency = this.GetPlayerCurrency(transactionID);

            currency.realmoney += amount;

            context.Entry(currency).State = EntityState.Modified;
            context.SaveChanges();
        }

        private void UpdateRubles(double amount)
        {
            var currency = this.UserCurrency;

            currency.realmoney += amount;

            context.Entry(currency).State = EntityState.Modified;
            context.SaveChanges();
        }

        #endregion
    }
}