using HistoryQuiz.Models;

namespace HistoryQuiz.Repositories
{
    public interface IAnswerRepository
    {
        Task AddAnswerAsync(Answer answer);

        Task DeleteAnswerAsync(int id);

        Task<IEnumerable<Answer>> GetAllAnswersAsync();

        Task<IEnumerable<Answer>> GetAllAnswersByQuestionIdAsync(int questionId);

        Task<Answer> GetAnswerByIdAsync(int id);

        Task UpdateAnswerAsync(Answer answer);
    }
}
