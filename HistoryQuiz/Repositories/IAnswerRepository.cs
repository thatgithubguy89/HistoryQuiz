using HistoryQuiz.Models;

namespace HistoryQuiz.Repositories
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Task<IEnumerable<Answer>> GetAllAnswersByQuestionIdAsync(int questionId);
    }
}
