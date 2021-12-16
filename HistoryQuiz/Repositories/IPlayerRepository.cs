using HistoryQuiz.Models;

namespace HistoryQuiz.Repositories
{
    public interface IPlayerRepository
    {
        Task CreatePlayerAsync(Player player);

        Task DeletePlayerAsync(Player player);

        Task<IEnumerable<Player>> GetAllPlayersAsync();

        Task<Player> GetPlayerByIdAsync(int id);

        Task<Player> GetPlayerByInitialsAsync(string initials);

        Task UpdatePlayerAsync(Player player);
    }
}
