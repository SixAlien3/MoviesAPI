using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace MoviesAPI.Infrastructure
{
    public class MovieApiHttpClient : IDisposable
    {
        internal static HttpClient Client { get; private set; }
        private List<KeyValuePair<string, string>> _queryString;
        private List<KeyValuePair<string, string>> _urlSegment;

        public static HttpClient GetClient()
        {
            Client = new HttpClient {BaseAddress = new Uri(MovieApiConstants.TmdbApiUri)};
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return Client;
        }

        private static string MakeUri(string method, params Tuple<string, string>[] entries)
        {
            // method should include ? (Question Mark)
            // 0 - TmdbUri   1- method   2- ApiKey   3 - language   4- page number
            var builder = new StringBuilder();
            builder.AppendFormat("{0}{1}api_key={2}&language={3}&api_sig={4}", MovieApiConstants.TmdbApiUri, method,
                MovieApiConstants.ApiKey, "en-US", "1");

            if (entries != null)
                foreach (var entry in entries)
                    builder.AppendFormat("&{0}={1}", entry.Item1, entry.Item2);
            return builder.ToString();
        }
      

        private static void ReleaseUnmanagedResources()
        {
            Client?.Dispose();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~MovieApiHttpClient()
        {
            ReleaseUnmanagedResources();
        }
    }
}