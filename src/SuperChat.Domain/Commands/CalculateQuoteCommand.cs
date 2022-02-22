using System;

namespace SuperChat.Domain.Commands
{
    public class CalculateQuoteCommand
    {
        public Guid Id { get; set; }
        public string CorrelationId { get; set; }
        public string StockCode { get; set; }
        public DateTime RequestDate { get; set; }

        public CalculateQuoteCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}