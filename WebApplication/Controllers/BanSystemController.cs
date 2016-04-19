using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class BanSystemController : Controller
    {
        // GET: Ban
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ban/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ban/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ban/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ban/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ban/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ban/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ban/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
