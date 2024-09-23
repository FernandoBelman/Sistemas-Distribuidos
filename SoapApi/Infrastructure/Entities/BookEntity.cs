using System;

namespace SoapApi.Infrastructure.Entities
{
    public class BookEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; } 
        public DateTime PublishedDate { get; set; }
    }
}
