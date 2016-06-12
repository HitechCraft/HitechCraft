namespace WebApplication.Controllers
{
    #region Using Directives

    using AutoMapper;
    using Domain;
    using Models;
    using System.Collections.Generic;

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

        //public IEnumerable<NewsViewModel> GetNewsList(int? page)
        //{
        //    int currentPage = page ?? 1;
            
        //    var vm = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects.ToList());
        //}
    }
}
