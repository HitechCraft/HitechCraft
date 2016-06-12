namespace WebApplication.Controllers
{
    #region Using Directives

    using AutoMapper;
    using Domain;
    using Models;
    using PagedList;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    #endregion

    public class NewsController : BaseController
    {
        public int NewsOnPage { get { return 10; } protected set { } }

        public NewsController()
        {
            Mapper.CreateMap<News, NewsViewModel>()
                .ForMember(dst => dst.AuthorId, ext => ext.MapFrom(src => src.Author.Id))
                .ForMember(dst => dst.AuthorName, ext => ext.MapFrom(src => src.Author.UserName));
        }

        public IEnumerable<NewsViewModel> GetNewsList(int? page)
        {
            int currentPage = page ?? 1;

            var news = this.context.News.Include("Author");

            var vm = Mapper.Map<IEnumerable<News>, IEnumerable<NewsViewModel>>(news.ToList());

            return vm.ToPagedList(currentPage, this.NewsOnPage);
        }

        [HttpGet]
        public ActionResult NewsPartialList(int? page)
        {
            return PartialView("_NewsPartialList", this.GetNewsList(page));
        }
    }
}
