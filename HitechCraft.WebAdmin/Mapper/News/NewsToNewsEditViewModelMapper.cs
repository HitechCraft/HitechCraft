﻿using HitechCraft.WebAdmin.Models;

namespace HitechCraft.WebAdmin.Mapper
{
    public class NewsToNewsEditViewModelMapper : BaseMapper<DAL.Domain.News, NewsEditViewModel>
    {
        public NewsToNewsEditViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<DAL.Domain.News, NewsEditViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.Title, ext => ext.MapFrom(src => src.Title))
                .ForMember(dst => dst.Text, ext => ext.MapFrom(src => src.Text))
                .ForMember(dst => dst.AuthorName, ext => ext.MapFrom(src => src.Author.Name));
        }
    }
}