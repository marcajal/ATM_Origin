using ATM_Origin.Core.RequestFilters;
using ATM_Origin_Infrastucture.Interfaces;
using System;

namespace ATM_Origin_Infrastucture
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPostPaginationUri(TarjetaRequestFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
