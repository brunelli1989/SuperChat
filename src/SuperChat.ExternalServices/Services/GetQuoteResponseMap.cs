using CsvHelper.Configuration;
using SuperChat.ExternalServices.Models;
using System;
using System.Globalization;

namespace SuperChat.ExternalServices.Services
{
    public partial class StooqExternalService
    {
        public sealed class GetQuoteResponseMap : ClassMap<GetQuote.Response>
        {
            public GetQuoteResponseMap()
            {
                AutoMap(CultureInfo.InvariantCulture);
                Map(m => m.Date).Convert(row => DateTime.Parse(row.Row["Date"]) + TimeSpan.Parse(row.Row["Time"]));
            }
        }
    }
}