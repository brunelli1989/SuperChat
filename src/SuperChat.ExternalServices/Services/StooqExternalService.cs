using Microsoft.Extensions.Configuration;
using SuperChat.ExternalServices.Contracts;
using SuperChat.ExternalServices.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.ExternalServices.Services
{
    public class StooqExternalService : IStooqExternalService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public StooqExternalService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(StooqExternalService));
            _url = configuration.GetSection("Stooq:RequestUri").Get<string>();
        }

        public async Task<List<GetQuote.Response>> Get(GetQuote.Request request)
        {
            var byteArray = await _httpClient.GetByteArrayAsync(string.Format(_url, request.StockCode));

            var csv = Encoding.UTF8.GetString(byteArray);

            var lines = csv.Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            lines.RemoveAt(0);

            var quotes = new List<GetQuote.Response>();

            foreach (var line in lines)
            {
                if (line.Contains("N/D"))
                    continue;

                var columns = line.Split(',');
                var quote = new GetQuote.Response
                {
                    Symbol = columns[0],
                    Date = DateTime.Parse(columns[1], CultureInfo.InvariantCulture) + TimeSpan.Parse(columns[2], CultureInfo.InvariantCulture),
                    Open = double.Parse(columns[3], CultureInfo.InvariantCulture),
                    High = double.Parse(columns[4], CultureInfo.InvariantCulture),
                    Low = double.Parse(columns[5], CultureInfo.InvariantCulture),
                    Close = double.Parse(columns[6], CultureInfo.InvariantCulture),
                    Volume = double.Parse(columns[7], CultureInfo.InvariantCulture)
                };
                quotes.Add(quote);
            }

            return quotes;

        }
    }
}