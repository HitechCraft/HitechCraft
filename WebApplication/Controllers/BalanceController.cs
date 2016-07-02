using System;
using System.Collections.Generic;
using WebApplication.Core;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    using System.Web.Mvc;

    public class BalanceController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        #region IK Actions

        /// <summary>
        /// For ik actions
        /// </summary>
        /// <param name="pm">Payment params</param>
        /// <returns></returns>
        public ActionResult Payment(PaymentModel pm)
        {
            //TODO: add unick transaction ids
            if (this.IsValidTransaction(pm))
            {
                //some action here
            }

            return null;
        }

        /// <summary>
        /// Action that call on succeded payment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Success()
        {
            //redirect to some action
            return this.Content("Ok");
        }

        /// <summary>
        /// Action that call on failure payment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Fail()
        {
            //redirect to some action
            return this.Content("Fail");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check if transaction values compair IKConfig.values
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        private bool IsValidTransaction(PaymentModel pm)
        {
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
                && pm.ik_pm_no == IKConfig.TransactionID
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
    }
}