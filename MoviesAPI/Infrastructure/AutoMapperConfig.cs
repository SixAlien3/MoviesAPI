using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
                    .ForMember(dest => dest.Genres, opts => opts.ResolveUsing(src => GetAllMovieGenres(src.GenreIds) ))
                    .ForMember(dest => dest.PosterUrl,
                        opts => opts.MapFrom(src => $"http://image.tmdb.org/t/p/w342//{src.PosterPath}"));
            });
        }

        private static Genre GetGenreById(int tmdbGenreId)
        {
            switch (tmdbGenreId)
            {
                case 28: return new Genre() {Id = 6, Name = "Action"};
                case 12: return new Genre() {Id = 1, Name = "Adventure"};
                case 16: return new Genre() {Id = 3, Name = "Animation"};
                case 35: return new Genre() {Id = 7, Name = "Comedy"};
                case 80: return new Genre() {Id = 11, Name = "Crime"};
                case 99: return new Genre() {Id = 12, Name = "Documentary"};
                case 18: return new Genre() {Id = 4, Name = "Drama"};
                case 10751: return new Genre() {Id = 17, Name = "Family"};
                case 14: return new Genre() {Id = 2, Name = "Fantasy"};
                case 36: return new Genre() {Id = 8, Name = "History"};
                case 27: return new Genre() {Id = 5, Name = "Horror"};
                case 10402: return new Genre() {Id = 15, Name = "Music"};
                case 9648: return new Genre() {Id = 14, Name = "Mystery"};
                case 10749: return new Genre() {Id = 16, Name = "Romance"};
                case 878: return new Genre() {Id = 13, Name = "Science Fiction"};
                case 10770: return new Genre() {Id = 20, Name = "TV Movie"};
                case 53: return new Genre() {Id = 10, Name = "Thriller"};
                case 10752: return new Genre() {Id = 18, Name = "War"};
                case 37: return new Genre() {Id = 9, Name = "Western"};
                default: return new Genre() {Id = 1, Name = "Action"};
            }
        }

        public static List<Genre> GetAllMovieGenres(List<int> genreIds)
        {
            List<Genre> genres = new List<Genre>();
            foreach (var g in genreIds)
            {
                genres.Add(GetGenreById(g));
            }

            return genres;
        }
    }
}