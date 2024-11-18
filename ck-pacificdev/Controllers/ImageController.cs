using ck_pacificdev.Services;
using Microsoft.AspNetCore.Mvc;

namespace ck_pacificdev.Controllers
{
    [ApiController]
    [Route("")]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet("Avatar")]
        public async Task<IActionResult> Avatar(string userIdentifier)
        {
            if (string.IsNullOrEmpty(userIdentifier))
            {
                return BadRequest("User identifier is required");
            }

            var imageUrl = await _imageService.GetImageUrlAsync(userIdentifier);
            return Ok(new { Url = imageUrl });
        }         
    }
}
   

