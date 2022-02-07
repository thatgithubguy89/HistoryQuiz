using Microsoft.AspNetCore.Components.Forms;

namespace HistoryQuiz.Services
{
    public interface IImageService
    {
        Task<string> AddImage(InputFileChangeEventArgs e);
        void DeleteImage(string image);
    }
}
