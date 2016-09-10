using System;
using System.Linq;
using System.Web.Mvc;
using HitechCraft.BL.CQRS.Query;
using HitechCraft.Common.DI;
using HitechCraft.Common.Projector;
using HitechCraft.DAL.Domain;
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
    }
}