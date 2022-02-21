using System;

namespace SuperChat.ExternalServices.Models
{
    public class GetQuote
    {
        public class Request
        {
            public string StockCode { get; set; }
        }

        public class Response
        {
            public string Symbol { get; set; }
            public DateTime Date { get; set; }
            public double Open { get; set; }
            public double High { get; set; }
            public double Low { get; set; }
            public double Close { get; set; }
            public double Volume { get; set; }
        }
    }
}