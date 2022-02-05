using HistoryQuiz.Models;

namespace HistoryQuiz.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<Player> GetPlayerByInitialsAsync(string initials);
    }
}
