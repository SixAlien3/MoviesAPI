using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Movies.Models;
using TMDbLib.Objects.Search;

namespace MoviesAPI.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            //var config = new MapperConfiguration(cfg =>
            //    cfg.CreateMap<SearchMovie, Movie>().ForMember(dest => dest.AverageVote,
            //        opts => opts.MapFrom(src => src.VoteAverage)));
            //IMapper iMapper = config.CreateMapper();

            Mapper.Initialize((config) =>
            {
                config.CreateMap<SearchMovie, Movie>()
                    .ForMember(dest => dest.AverageVote, opts => opts.MapFrom(src => src.VoteAverage))
                    .ForMember(dest => dest.BackdropUrl,
                        opts => opts.MapFrom(src => $"http://image.tmdb.org/t/p/w1280//{src.BackdropPath}"))
                    //.ForMember(dest => dest.Genres, opts => opts.MapFrom(src => src.GenreIds))
                    .ForMember(dest => dest.PosterUrl,
                        opts => opts.MapFrom(src => $"http://image.tmdb.org/t/p/w342//{src.PosterPath}"));
            });
        }
    }
}