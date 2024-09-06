namespace SoapApi.Dtos
{
    public class BookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; } 
        public DateTime PublishedDate { get; set; }
    }
}
