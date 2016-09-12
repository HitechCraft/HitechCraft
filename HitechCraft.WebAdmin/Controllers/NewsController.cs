namespace HitechCraft.WebAdmin.Controllers
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Web.Mvc;
    using BL.CQRS.Command;
    using BL.CQRS.Query;
    using Common.DI;
    using Common.Projector;
    using DAL.Domain;
    using Manager;
    using Models;
    using PagedList;

    #endregion

    public class NewsController : BaseController
    {
        public int NewsOnPage => 10;

        public NewsController(IContainer container) : base(container)
        {
        }

        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewsPartialList(int? page, string newsTitleFilter = "")
        {
            int currentPage = page ?? 1;

            var news = new EntityListQueryHandler<News, NewsViewModel>(Container)
                .Handle(new EntityListQuery<News, NewsViewModel>()
                {
                    Projector = Container.Resolve<IProjector<News, NewsViewModel>>()
                });

            if (!String.IsNullOrEmpty(newsTitleFilter))
                news =
                    news.Where(x => x.Title.Contains(newsTitleFilter) || x.FullText.Contains(newsTitleFilter)).ToList();

            return PartialView("_NewsPartialList", news.ToPagedList(currentPage, NewsOnPage));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NewsEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var uploadImage = Request.Files["uploadNewsImage"];

                    CommandExecutor.Execute(new NewsCreateCommand()
                    {
                        Title = vm.Title,
                        Image = ImageManager.GetImageBytes(uploadImage),
                        Text = vm.Text,
                        PlayerName = Admin.UserName
                    });
                    
                    return RedirectToAction("Index", "News");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty, e.Message);

                    return View();
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == 0 || id == null) throw new Exception();

                var vm = new EntityQueryHandler<News, NewsViewModel>(Container)
                    .Handle(new EntityQuery<News, NewsViewModel>()
                    {
                        Id = id,
                        Projector = Container.Resolve<IProjector<News, NewsViewModel>>()
                    });

                CommandExecutor.Execute(new NewsViewsUpdateCommand()
                {
                    NewsId = (int)id
                });

                return View(vm);
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == 0 || id == null) throw new Exception();

                var vm = new EntityQueryHandler<News, NewsEditViewModel>(Container)
                    .Handle(new EntityQuery<News, NewsEditViewModel>()
                    {
                        Id = id,
                        Projector = Container.Resolve<IProjector<News, NewsEditViewModel>>()
                    });

                return View(vm);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NewsEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var uploadImage = Request.Files["uploadNewsImage"];

                if (uploadImage != null && uploadImage.ContentLength > 0) vm.Image = ImageManager.GetImageBytes(uploadImage);

                CommandExecutor.Execute(Project<NewsEditViewModel, NewsUpdateCommand>(vm));

                return RedirectToAction("Details", new { id = vm.Id });
            }

            return View(vm);
        }

        [HttpPost]
        public JsonResult Delete(int? id)
        {
            try
            {
                this.CommandExecutor.Execute(new NewsRemoveCommand()
                {
                    Id = id.Value
                });

                return Json(new {status = "OK", message = "Новость удалена"});
            }
            catch (Exception e)
            {
                return Json(new { status = "NO", message = "Ошибка удаления новости: " + e.Message });
            }
        }
    }
}