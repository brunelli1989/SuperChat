using System;

namespace SuperChat.Domain.Events
{
    public class QuoteCalculatedEvent
    {
        public Guid Id { get; set; }
        public string CorrelationId { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
    }
}