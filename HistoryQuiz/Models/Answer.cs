using System.ComponentModel.DataAnnotations;

namespace HistoryQuiz.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsCorrect { get; set; } = false;

        [Required]
        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
