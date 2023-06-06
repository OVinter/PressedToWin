using Microsoft.AspNetCore.Http;
using System.Linq;

namespace HttpRequestsLibrary
{
    public static class HttpRequestExtensions
    {
        public static string GetAuthToken(this HttpRequest request)
        {
            return GetToken(request, "Authorization");
        }

        public static string GetIdentityToken(this HttpRequest request)
        {
            return GetToken(request, "Identity-Token");
        }

        private static string GetToken(HttpRequest request, string type)
        {
            var token = "";
            if (request != null && request.Headers.TryGetValue(type, out var value))
            {
                token = value.FirstOrDefault();
            }
            return token;
        }

        public static string GetBaseApiUrl(this HttpRequest request, string apiVersion)
        {
            return $"{request.Scheme}://{request.Host.Host}/api/v{apiVersion}";
        }
    }
}
