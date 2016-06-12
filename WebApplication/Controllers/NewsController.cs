namespace WebApplication.Controllers
{
    #region Using Directives

    using AutoMapper;
    using Domain;
    using Models;
    using PagedList;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    public class NewsController : BaseController
    {
        public int NewsOnPage { get { return 10; } protected set { } }

        public int CommentsOnPage { get { return 10; } protected set { } }

        public NewsController()
        {
            Mapper.CreateMap<News, NewsViewModel>()
                .ForMember(dst => dst.AuthorId, ext => ext.MapFrom(src => src.Author.Id))
                .ForMember(dst => dst.AuthorName, ext => ext.MapFrom(src => src.Author.UserName));

            Mapper.CreateMap<Comment, CommentViewModel>()
                .ForMember(dst => dst.AuthorId, ext => ext.MapFrom(src => src.Author.Id))
                .ForMember(dst => dst.AuthorName, ext => ext.MapFrom(src => src.Author.UserName))
                .ForMember(dst => dst.NewsId, ext => ext.MapFrom(src => src.News.Id));
        }

        public IPagedList<NewsViewModel> GetNewsList(int? page)
        {
            int currentPage = page ?? 1;

            var news = this.context.News.Include("Author");

            var vm = Mapper.Map<IEnumerable<News>, IEnumerable<NewsViewModel>>(news.ToList());

            return vm.ToPagedList(currentPage, this.NewsOnPage);
        }

        public IPagedList<CommentViewModel> GetCommentList(int? page)
        {
            int currentPage = page ?? 1;

            var comments = this.context.Comments.Include("Author").Include("News");

            var vm = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments.ToList());

            return vm.ToPagedList(currentPage, this.CommentsOnPage);
        }

        [HttpGet]
        public ActionResult NewsPartialList(int? page)
        {
            return PartialView("_NewsPartialList", this.GetNewsList(page));
        }

        [HttpGet]
        public ActionResult CommentsPartialList(int? page)
        {
            return PartialView("_CommentsPartialList", this.GetCommentList(page));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                var news = this.context.News.Find(id);

                var vm = Mapper.Map<News, NewsViewModel>(news);

                return View(vm);
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }
    }
}
