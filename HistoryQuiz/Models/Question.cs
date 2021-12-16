using System.ComponentModel.DataAnnotations;

namespace HistoryQuiz.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a question")]
        public string Content { get; set; }

        public string Image { get; set; }
    }
}
