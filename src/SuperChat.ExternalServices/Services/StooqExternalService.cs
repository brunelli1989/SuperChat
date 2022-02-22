using CsvHelper;
using Microsoft.Extensions.Configuration;
using SuperChat.ExternalServices.Contracts;
using SuperChat.ExternalServices.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SuperChat.ExternalServices.Services
{
    public partial class StooqExternalService : IStooqExternalService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public StooqExternalService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(StooqExternalService));
            _url = configuration.GetSection("Stooq:RequestUri").Get<string>();
        }

        public async Task<IEnumerable<GetQuote.Response>> Get(GetQuote.Request request)
        {
            using (var stream = await _httpClient.GetStreamAsync(string.Format(_url, request.StockCode)))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<GetQuoteResponseMap>();
                var records = csv.GetRecords<GetQuote.Response>();
                return records;
            }
        }
    }
}