namespace MoviesAPI.Utilities
{
    public class Utility
    {
        public static int GetGenreById(int tmdbGenreId)
        {
            switch (tmdbGenreId)
            {
                case 6:
                    return 28;
                case 1:
                    return 12;
                case 3:
                    return 16;
                case 7:
                    return 35;
                case 11:
                    return 80;
                case 12:
                    return 99;
                case 4:
                    return 18;
                case 17:
                    return 10751;
                case 2:
                    return 14;
                case 8:
                    return 36;
                case 5:
                    return 27;
                case 15:
                    return 10402;
                case 14:
                    return 9648;
                case 16:
                    return 10749;
                case 13:
                    return 878;
                case 20:
                    return 10770;
                case 10:
                    return 53;
                case 18:
                    return 10752;
                case 9:
                    return 37;
                default:
                    return 1;
            }
        }
    }
}