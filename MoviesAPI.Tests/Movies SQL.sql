/****** Script for SelectTopNRows command from SSMS  ******/
USE [MoviesDB]
GO

SELECT *
  FROM [MoviesDB].[dbo].[Movies]

  where ExternalId=13701

SELECT [Genre_Id]
      ,[Movie_Id]
  FROM [dbo].[GenreMovies]
GO

SELECT *
  FROM [dbo].[Genres]
GO

--delete from Genres where id > 20
--truncate table [dbo].[GenreMovies]


/****** Script for SelectTopNRows command from SSMS  ******/
SELECT *
  FROM [MoviesDB].[dbo].[Casts]
  where id =304291

SELECT *
  FROM [dbo].[MovieCasts]
  where Movie_Id=246
GO



--truncate table [MovieCasts]