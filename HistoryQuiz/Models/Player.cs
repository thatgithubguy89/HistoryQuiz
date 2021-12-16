using System.ComponentModel.DataAnnotations;

namespace HistoryQuiz.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your initials")]
        [StringLength(3, ErrorMessage = "Initials can only be 3 characters")]
        public string Initials { get; set; }

        public int Score { get; set; } = 0;
    }
}
