using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication.Domain;

namespace WebApplication.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationDbContext context = new ApplicationDbContext();

        private string DefaultMaleUserId;
        private string DefaultFemaleUserId;

        private double DefaultUserGonts = 30.00;
        private double DefaultUserRubls = 5.00;

        public Currency UserCurrency
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userName = User.Identity.GetUserName().ToString();
                    return context.Currencies.First(c => c.username == userName);
                }

                return null;
            }
        }

        public double Gonts
        {
            get { return UserCurrency != null ? UserCurrency.balance : -1; }
        }

        public double Rubels
        {
            get { return UserCurrency != null ? UserCurrency.realmoney : -1; }
        }

        public BaseController()
        {
            this.DefaultMaleUserId = "882155f6-f5a9-4a26-a5dd-d51f58492906";
            this.DefaultFemaleUserId = "f2e20140-3ab1-4b1f-ba30-c03824b3a91b";
        }

        /// <summary>
        /// Обновление осуществляется прибавлением (вычетом) значения. Указываем разницу, а не новое кол-во валюты!!!
        /// </summary>
        /// <param name="amount">Разница (например +10 или -10)</param>
        public void UpdateGonts(double amount)
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
        public void UpdateRubels(double amount)
        {
            var currency = this.UserCurrency;

            currency.realmoney += amount;

            context.Entry(currency).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}