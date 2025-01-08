namespace UrlShortener.Services
{
    public interface IUrlShortenerServices
    {
        Task<string> UniqueCodeGenerator();
    }
}
