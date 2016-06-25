namespace WebApplication.Controllers
{
    using AutoMapper;
    using Domain;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    public class ModificationController : BaseController
    {
        public IEnumerable<Modification> Modifications { get { return this.context.Modifications.Include("ServerModifications.Server"); } }

        public ModificationController()
        {
            Mapper.CreateMap<Modification, ModificationViewModel>()
               .ForMember(dst => dst.Servers, exp => exp.MapFrom(
                   src => src.ServerModifications != null ? src.ServerModifications
                       .Select(sm => new ServerModificationViewModel()
                       {
                           Id = sm.Server.Id,
                           Name = sm.Server.Name
                       }).ToList() : new List<ServerModificationViewModel>()));

            Mapper.CreateMap<Modification, ModificationEditViewModel>();
            Mapper.CreateMap<ModificationEditViewModel, Modification>();
        }

        public ActionResult Index()
        {
            var vm = Mapper.Map<IEnumerable<Modification>, IEnumerable<ModificationViewModel>> (this.Modifications);

            return View(vm);
        }

        public ActionResult Details(int id)
        {
            try
            {
                var mod = this.Modifications.First(s => s.Id == id);

                var vm = Mapper.Map<Modification, ModificationViewModel>(mod);

                return View(vm);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;

                return View();
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ModificationEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var mod = Mapper.Map<ModificationEditViewModel, Modification>(vm);

                this.context.Modifications.Add(mod);
                this.context.SaveChanges();

                return RedirectToAction("Details", new { id = mod.Id });
            }

            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var mod = this.Modifications.First(s => s.Id == id);

                var vm = Mapper.Map<Modification, ModificationEditViewModel>(mod);

                return View(vm);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Сервера не существует";

                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(ModificationEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var mod = Mapper.Map<ModificationEditViewModel, Modification>(vm);

                this.context.Entry(mod).State = EntityState.Modified;
                this.context.SaveChanges();

                return RedirectToAction("Details", new { id = mod.Id });
            }

            return View(vm);
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                var mod = this.Modifications.First(s => s.Id == id);

                var vm = Mapper.Map<Modification, ModificationEditViewModel>(mod);

                return View(vm);
            }
            catch (Exception)
            {
                ViewBag.Error = "Сервера не существует";

                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var mod = this.Modifications.First(s => s.Id == id);

            this.context.Modifications.Remove(mod);
            this.context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
