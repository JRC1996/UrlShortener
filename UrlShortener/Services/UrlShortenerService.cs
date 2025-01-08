
using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class UrlShortenerService : IUrlShortenerServices
    {
        private readonly UrlshortenerContext _context;

        private readonly Random _random = new Random();
  

        public UrlShortenerService(UrlshortenerContext context)
        {
            _context = context;
        }

        public async Task<string> UniqueCodeGenerator() 
        {

            var codeChars = new char[ShortUrlSettings.lenght];

            int maxValue  = ShortUrlSettings.alphabet.Length;

            var counter = 0;

           
            while (true) 
            {
                for (int i = 0; i < ShortUrlSettings.lenght; i++ ) 
                { 
                     var randomIndx = _random.Next(maxValue);

                     codeChars[i] = ShortUrlSettings.alphabet[randomIndx];
                }
            
               var code = new string(codeChars);
               counter++;

                if (counter == 7)
                {
                   Console.WriteLine("Max tries reached");
                   break;
                }


                if (!await _context.Urls.AnyAsync(c => c.Code == code))
                {
                    return code;

                }

              
            }

            throw new Exception("Cannot generate the code, max tries exceded.");
        }

    }
}
