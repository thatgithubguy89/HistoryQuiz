using HistoryQuiz.Models;

namespace HistoryQuiz.Repositories
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Task AddAnswerAsync(Answer answer, int questionId);
    }
}
