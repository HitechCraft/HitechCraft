using System.Linq;
using HitechCraft.Core.DI;
using HitechCraft.Core.Entity;
using HitechCraft.Core.Repository.Specification.News;
using HitechCraft.Projector.Impl;

namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using Models;
    using PagedList;
    using BL.CQRS.Query;
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