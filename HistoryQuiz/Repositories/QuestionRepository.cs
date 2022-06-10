using HistoryQuiz.Data;
using HistoryQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace HistoryQuiz.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .ToListAsync();
        }

        public override async Task<Question> GetByIdAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();

            return await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
