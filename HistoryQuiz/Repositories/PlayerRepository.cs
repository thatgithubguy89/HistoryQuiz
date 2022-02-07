using HistoryQuiz.Data;
using HistoryQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace HistoryQuiz.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        private readonly AppDbContext _context;

        public PlayerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _context.Players.OrderByDescending(p => p.Score).ToListAsync();
        }

        public async Task<Player> GetPlayerByInitialsAsync(string initials)
        {
            if (string.IsNullOrWhiteSpace(initials))
                throw new ArgumentNullException();

            return await _context.Players.FirstOrDefaultAsync(p => p.Initials == initials);
        }
    }
}
