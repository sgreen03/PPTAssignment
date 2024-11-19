using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using ck_pacificdev.Data;
using Newtonsoft.Json;

namespace ck_pacificdev.Services
{    
    public class ImageService : IImageService
    {  
        private readonly DatabaseContext _context;
        private readonly HttpClient _httpClient;

        public ImageService(DatabaseContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;   
        }

        public async Task<string> GetImageUrlAsync(string userIdentifier)
        {
            string imageUrl = null;
            var lastDigit = userIdentifier[userIdentifier.Length - 1];

            if (userIdentifier.EndsWith("6") || userIdentifier.EndsWith("7") || userIdentifier.EndsWith("8")
                || userIdentifier.EndsWith("9"))
            {
                 var url = $"https://my-json-server.typicode.com/ck-pacificdev/tech-test/images/{lastDigit}";
                 var response = await _httpClient.GetStringAsync(url);
                 dynamic result = JsonConvert.DeserializeObject(response);
                 imageUrl = result?.url;
            }
            else if (userIdentifier.EndsWith("1") || userIdentifier.EndsWith("2") || userIdentifier.EndsWith("3")
                     || userIdentifier.EndsWith("4") || userIdentifier.EndsWith("5"))
            {
                var image = await _context.Images.FirstOrDefaultAsync(i => i.Id.ToString() == lastDigit.ToString());
                if (image != null)
                {
                    imageUrl = image.Url;
                }
            }
            else if (userIdentifier.Contains("a", System.StringComparison.CurrentCultureIgnoreCase) 
                     || userIdentifier.Contains("e", System.StringComparison.CurrentCultureIgnoreCase) 
                     || userIdentifier.Contains("i", System.StringComparison.CurrentCultureIgnoreCase)
                     || userIdentifier.Contains("o", System.StringComparison.CurrentCultureIgnoreCase) 
                     || userIdentifier.Contains("u", System.StringComparison.CurrentCultureIgnoreCase))
            {
                imageUrl = "https://api.dicebear.com/8.x/pixel-art/png?seed=vowel&size=150";
            }
            else if (!userIdentifier.All(Char.IsLetterOrDigit))
            {
                Random rnd = new Random();
                int randomSeed = rnd.Next(1, 6);

                imageUrl = $"https://api.dicebear.com/8.x/pixel-art/png?seed={randomSeed}&size=150";
            }
            else
            {
                imageUrl = "https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150";
            }

            return imageUrl;
        }
    }
}
