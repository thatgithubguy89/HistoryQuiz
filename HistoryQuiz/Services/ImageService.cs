using Microsoft.AspNetCore.Components.Forms;

namespace HistoryQuiz.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string> AddImage(InputFileChangeEventArgs e)
        {
            string webRootPath = _hostEnvironment.WebRootPath; // Get path to wwwroot
            var files = e.GetMultipleFiles();
            string fileName = Path.GetRandomFileName(); // Give the file(s) a random name
            var uploads = Path.Combine(webRootPath, @"images"); // Get path to wwwroot\images
            var extension = Path.GetExtension(files[0].Name); // Get the extension for the file(s)
            long newSize = 500000000;

            using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                await files[0].OpenReadStream(newSize).CopyToAsync(fileStream);
            }

            return @"\images\" + fileName + extension; // Set image property to the new files path
        }

        public void DeleteImage(string image)
        {
            string webRootPath = _hostEnvironment.WebRootPath; // Get path to wwwroot
            var imagePath = Path.Combine(webRootPath, image.TrimStart('\\')); // Get the current image property path
            if (File.Exists(imagePath)) // If the file exists in wwwroot\images
            {
                File.Delete(imagePath); // Delete the file
            }
        }
    }
}
