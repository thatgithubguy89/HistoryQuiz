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
            return await _context.Answers.Include(a => a.Question).OrderBy(a => a.Question.Title).ToListAsync();
        }

        public async Task<IEnumerable<Answer>> GetAllAnswersByQuestionIdAsync(int questionId)
        {
            if (questionId == 0)
                throw new ArgumentNullException();

            List<Answer> first = new List<Answer>();
            first = await _context.Answers.ToListAsync();
            List<Answer> second = new List<Answer>();

            foreach (var item in first)
            {
                if (item.QuestionId == questionId)
                    second.Add(item);
            }
            return second;
        }
    }
}
