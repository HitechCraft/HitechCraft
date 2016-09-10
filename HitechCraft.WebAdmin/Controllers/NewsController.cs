using System;
using System.Linq;
using System.Web.Mvc;
using HitechCraft.BL.CQRS.Command;
using HitechCraft.BL.CQRS.Query;
using HitechCraft.Common.DI;
using HitechCraft.Common.Projector;
using HitechCraft.DAL.Domain;
using HitechCraft.WebAdmin.Manager;
using HitechCraft.WebAdmin.Models;
using PagedList;

namespace HitechCraft.WebAdmin.Controllers
{
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

            var news = new EntityListQueryHandler<News, NewsViewModel>(this.Container)
                .Handle(new EntityListQuery<News, NewsViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<News, NewsViewModel>>()
                });

            if (!String.IsNullOrEmpty(newsTitleFilter))
                news =
                    news.Where(x => x.Title.Contains(newsTitleFilter) || x.FullText.Contains(newsTitleFilter)).ToList();

            return PartialView("_NewsPartialList", news.ToPagedList(currentPage, this.NewsOnPage));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewsEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var uploadImage = Request.Files["uploadNewsImage"];

                    this.CommandExecutor.Execute(new NewsCreateCommand()
                    {
                        Title = vm.Title,
                        Image = ImageManager.GetImageBytes(uploadImage),
                        Text = vm.Text,
                        PlayerName = this.Admin.UserName
                    });
                    
                    return RedirectToAction("Index", "Home");
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

                var vm = new EntityQueryHandler<News, NewsViewModel>(this.Container)
                    .Handle(new EntityQuery<News, NewsViewModel>()
                    {
                        Id = id,
                        Projector = this.Container.Resolve<IProjector<News, NewsViewModel>>()
                    });

                this.CommandExecutor.Execute(new NewsViewsUpdateCommand()
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
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == 0 || id == null) throw new Exception();

                var vm = new EntityQueryHandler<News, NewsEditViewModel>(this.Container)
                    .Handle(new EntityQuery<News, NewsEditViewModel>()
                    {
                        Id = id,
                        Projector = this.Container.Resolve<IProjector<News, NewsEditViewModel>>()
                    });

                return View(vm);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(NewsEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var uploadImage = Request.Files["uploadNewsImage"];

                if (uploadImage != null && uploadImage.ContentLength > 0) vm.Image = ImageManager.GetImageBytes(uploadImage);

                this.CommandExecutor.Execute(this.Project<NewsEditViewModel, NewsUpdateCommand>(vm));

                return RedirectToAction("Details", new { id = vm.Id });
            }

            return View(vm);
        }
    }
}