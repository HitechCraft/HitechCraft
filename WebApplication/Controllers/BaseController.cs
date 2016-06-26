﻿using System;

namespace WebApplication.Controllers
{
    #region Using Directives

    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Domain;

    #endregion

    public class BaseController : Controller
    {
        public ApplicationDbContext context = new ApplicationDbContext();
        
        public string DefaultMaleUserId
        {
            get { return "882155f6-f5a9-4a26-a5dd-d51f58492906"; }
        }

        public string DefaultFemaleUserId
        {
            get { return "f2e20140-3ab1-4b1f-ba30-c03824b3a91b"; }
        }
        
        public double DefaultUserGonts
        {
            get { return 30.00; }
        }

        public double DefaultUserRubels
        {
            get { return 5.00; }
        }

        public Currency UserCurrency
        {
            get
            {
                return this.GetUserCurrency();
            }
        }

        private Currency GetUserCurrency()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.GetUserName().ToString();

                try
                {
                    return context.Currencies.First(c => c.username == userName);
                }
                catch (Exception)
                {
                    context.Currencies.Add(new Currency
                    {
                        username = userName,
                        balance = this.DefaultUserGonts,
                        realmoney = this.DefaultUserRubels,
                        status = 0
                    });

                    context.SaveChanges();

                    return context.Currencies.First(c => c.username == userName);
                }
            }

            return null;
        }

        public double Gonts
        {
            get { return UserCurrency != null ? UserCurrency.balance : -1; }
        }

        public double Rubels
        {
            get { return UserCurrency != null ? UserCurrency.realmoney : -1; }
        }

        public ApplicationUser CurrentUser {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.Identity.GetUserId();
                    return context.Users.First(c => c.Id == userId);
                }

                return null;
            }
        }

        public Player CurrentPlayer { get; set; }

        public BaseController()
        {
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