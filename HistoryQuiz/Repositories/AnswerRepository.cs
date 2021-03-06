using HistoryQuiz.Data;
using HistoryQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace HistoryQuiz.Repositories
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        private readonly AppDbContext _context;

        public AnswerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Answer>> GetAllAsync()
        {
            return await _context.Answers
                .Include(a => a.Question)
                .OrderBy(a => a.Question.Title)
                .ToListAsync();
        }
    }
}
