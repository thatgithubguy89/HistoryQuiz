using HistoryQuiz.Data;
using HistoryQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace HistoryQuiz.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _context;

        public PlayerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreatePlayerAsync(Player player)
        {
            if (player == null)
                throw new ArgumentNullException();

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlayerAsync(Player player)
        {
            if (player == null)
                throw new ArgumentNullException();

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            return await _context.Players.OrderByDescending(p => p.Score).ToListAsync();
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();

            return await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Player> GetPlayerByInitialsAsync(string initials)
        {
            if (string.IsNullOrWhiteSpace(initials))
                throw new ArgumentNullException();

            return await _context.Players.FirstOrDefaultAsync(p => p.Initials == initials);
        }

        public async Task UpdatePlayerAsync(Player player)
        {
            if (player == null)
                throw new ArgumentNullException();

            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }
    }
}
