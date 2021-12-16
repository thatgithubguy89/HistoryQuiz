using HistoryQuiz.Models;

namespace HistoryQuiz.Repositories
{
    public interface IQuestionRepository
    {
        Task AddQuestionAsync(Question question);

        Task DeleteQuestionAsync(int id);

        Task<IEnumerable<Question>> GetAllQuestionsAsync();

        Task<Question> GetQuestionByIdAsync(int id);

        Task UpdateQuestionAsync(Question question);
    }
}
