namespace UrlShortener.Models
{
    public class UrlShortener
    {

        public int Id { get; set; }

        public string Url { get; set; }

        public string ShortUrl { get; set; }

        public string Code { get; set; }

        public DateTime Date { get; set; }

    }
}
