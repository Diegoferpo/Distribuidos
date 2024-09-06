namespace SoapApi.Models;

    public class BookModel
    {
        public Guid Id {get; set; }
        public String Title {get; set; } = null!;
        public String Author {get; set; } = null!;
        public String Publisher {get; set; } = null!;
        public DateTime PublishedDate {get; set; }
    }
