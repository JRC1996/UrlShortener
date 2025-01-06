namespace UrlShortener.Services
{
    public interface IUrlShortenerServices
    {
        Task<string> ShortenUrl(string url);
        Task<string> GetOriginalUrl(string shortUrl);
    }
}
