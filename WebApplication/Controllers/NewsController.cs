namespace WebApplication.Controllers
{
    #region Using Directives

    using System.Data.Entity;
    using Core;
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

            Mapper.CreateMap<News, NewsEditViewModel>()
                .ForMember(dst => dst.AuthorId, ext => ext.MapFrom(src => src.Author.Id))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image));

            Mapper.CreateMap<NewsEditViewModel, News>()
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image));
        }

        public IPagedList<NewsViewModel> GetNewsList(int? page)
        {
            int currentPage = page ?? 1;

            var news = this.context.News.Include("Author").OrderByDescending(n => n.Id).ToList();

            var vm = Mapper.Map<IEnumerable<News>, IEnumerable<NewsViewModel>>(news.ToList());

            return vm.ToPagedList(currentPage, this.NewsOnPage);
        }

        public IPagedList<CommentViewModel> GetCommentList(int? page, int newsId)
        {
            int currentPage = page ?? 1;

            var comments = this.context.Comments.Include("Author").Include("News").Where(c => c.News.Id == newsId).OrderByDescending(c => c.Id);

            var vm = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments.ToList());

            return vm.ToPagedList(currentPage, this.CommentsOnPage);
        }

        [HttpGet]
        [Authorize]
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

                    var news = Mapper.Map<NewsEditViewModel, News>(vm);

                    news.Author = this.CurrentUser;
                    news.Image = ImageManager.GetImageBytes(uploadImage);
                    news.TimeCreate = DateTime.Now;
                    news.ViewersCount = 1;

                    this.context.News.Add(news);
                    this.context.SaveChanges();

                    return RedirectToAction("Details", new { id = news.Id });
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
        public ActionResult Details(int id)
        {
            try
            {
                var news = this.context.News.Include("Author").First(n => n.Id == id);

                var vm = Mapper.Map<News, NewsViewModel>(news);

                return View(vm);
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var news = this.context.News.Include("Author").First(n => n.Id == id);

                var vm = Mapper.Map<News, NewsEditViewModel>(news);

                return View(vm);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(NewsEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var news = Mapper.Map<NewsEditViewModel, News>(vm);

                var uploadImage = Request.Files["uploadNewsImage"];

                news.Author = this.context.Users.First(u => u.Id == vm.AuthorId); //fix

                if(uploadImage.ContentLength > 0) news.Image = ImageManager.GetImageBytes(uploadImage);
                
                this.context.Entry(news).State = EntityState.Modified;
                this.context.SaveChanges();

                return RedirectToAction("Details", new { id = news.Id });
            }

            return View(vm);
        }

        public ContentResult RemoveNewImage(int newsId)
        {
            try
            {
                var news = this.context.News.First(n => n.Id == newsId);

                news.Image = null;

                this.context.Entry(news).State = EntityState.Modified;
                this.context.SaveChanges();

                return this.Content("OK");
            }
            catch (Exception e)
            {
                return this.Content(e.Message);
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                var news = this.context.News.First(s => s.Id == id);

                var vm = Mapper.Map<News, NewsViewModel>(news);

                return View(vm);
            }
            catch (Exception)
            {
                ViewBag.Error = "Новости не существует";

                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var news = this.context.News.Include("Author").First(s => s.Id == id);

                this.RemoveNewsComments(id);

                this.context.News.Remove(news);
                this.context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        private void RemoveNewsComments(int newsId)
        {
            try
            {
                var comments = this.context.Comments.Include("News").Where(c => c.News.Id == newsId);

                this.context.Comments.RemoveRange(comments);
                this.context.SaveChanges();
            }
            catch (Exception)
            {
            }
        }
    }
}
