using System.Linq;

namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives
    
    using Common.DI;
    using DAL.Domain;
    using Models;
    using PagedList;
    using BL.CQRS.Query;
    using Common.Projector;
    using DAL.Repository.Specification;
    using System;
    using System.Web.Mvc;
    using Manager;
    using BL.CQRS.Command;

    #endregion

    public class NewsController : BaseController
    {
        private int NewsOnPage => 10;

        private int CommentsOnPage => 10;
        
        public NewsController(IContainer container) : base(container)
        {
        }

        #region News Actions

        [HttpGet]
        public ActionResult NewsPartialList(int? page)
        {
            return PartialView("_NewsPartialList", this.GetNewsList(page));
        }

        [HttpGet]
        public ActionResult CommentsPartialList(int? page, int newsId)
        {
            return PartialView("_CommentsPartialList", this.GetCommentList(page, newsId));
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
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
                        PlayerName = this.Player.Name
                    });

                    //TODO: redirect to details
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
        public ActionResult Details(int id)
        {
            try
            {
                var vm = new EntityQueryHandler<News, NewsViewModel>(this.Container)
                    .Handle(new EntityQuery<News, NewsViewModel>()
                    {
                        Id = id,
                        Projector = this.Container.Resolve<IProjector<News, NewsViewModel>>()
                    });

                this.CommandExecutor.Execute(new NewsViewsUpdateCommand()
                {
                    NewsId = id
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
        public ActionResult Edit(int id)
        {
            try
            {
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
                
                if (uploadImage.ContentLength > 0) vm.Image = ImageManager.GetImageBytes(uploadImage);

                this.CommandExecutor.Execute(this.Project<NewsEditViewModel, NewsUpdateCommand>(vm));

                return RedirectToAction("Details", new { id = vm.Id });
            }

            return View(vm);
        }

        [Authorize(Roles = "Administrator")]
        public ContentResult RemoveNewImage(int newsId)
        {
            try
            {
                this.CommandExecutor.Execute(new NewsImageRemoveCommand()
                {
                    NewsId = newsId
                });
                
                return this.Content("OK");
            }
            catch (Exception e)
            {
                return this.Content(e.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            try
            {
                var vm = new EntityQueryHandler<News, NewsViewModel>(this.Container)
                    .Handle(new EntityQuery<News, NewsViewModel>()
                    {
                        Id = id,
                        Projector = this.Container.Resolve<IProjector<News, NewsViewModel>>()
                    });

                return View(vm);
            }
            catch (Exception)
            {
                ViewBag.Error = "Новости не существует";

                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                this.CommandExecutor.Execute(new NewsRemoveCommand()
                {
                    Id = id
                });

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }

        #endregion

        #region Private Methods

        private IPagedList<NewsViewModel> GetNewsList(int? page)
        {
            int currentPage = page ?? 1;

            var vm = new EntityListQueryHandler<News, NewsViewModel>(this.Container)
                .Handle(new EntityListQuery<News, NewsViewModel>()
                {
                    Projector = this.Container.Resolve<IProjector<News, NewsViewModel>>()
                }).OrderByDescending(x => x.Id);

            return vm.ToPagedList(currentPage, this.NewsOnPage);
        }
        
        private IPagedList<CommentViewModel> GetCommentList(int? page, int newsId)
        {
            int currentPage = page ?? 1;

            var vm = new EntityListQueryHandler<Comment, CommentViewModel>(this.Container)
                .Handle(new EntityListQuery<Comment, CommentViewModel>()
                {
                    Specification = new CommentByNewsIdSpec(newsId),
                    Projector = this.Container.Resolve<IProjector<Comment, CommentViewModel>>()
                }).OrderByDescending(x => x.Id);


            return vm.ToPagedList(currentPage, this.CommentsOnPage);
        }

        #endregion
    }
}