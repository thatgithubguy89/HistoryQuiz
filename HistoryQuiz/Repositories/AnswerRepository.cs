using HistoryQuiz.Data;
using HistoryQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace HistoryQuiz.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AppDbContext _context;

        public AnswerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAnswerAsync(Answer answer)
        {
            if (answer == null)
                throw new ArgumentNullException();

            await _context.AddAsync(answer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnswerAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();

            var answer = await GetAnswerByIdAsync(id);
            if (answer == null)
                throw new ArgumentNullException();

            _context.Remove(answer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Answer>> GetAllAnswersAsync()
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

        public async Task<Answer> GetAnswerByIdAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();

            return await _context.Answers.Include(a => a.Question).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAnswerAsync(Answer answer)
        {
            if (answer == null)
                throw new ArgumentNullException();

            _context.Update(answer);
            await _context.SaveChangesAsync();
        }
    }
}
