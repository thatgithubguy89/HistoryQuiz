using HistoryQuiz.Data;
using HistoryQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace HistoryQuiz.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddQuestionAsync(Question question)
        {
            if (question == null)
                throw new ArgumentNullException();

            await _context.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();

            var question = await GetQuestionByIdAsync(id);
            if (question == null)
                throw new ArgumentNullException();

            _context.Remove(question);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException();

            return await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            if (question == null)
                throw new ArgumentNullException();

            _context.Update(question);
            await _context.SaveChangesAsync();
        }
    }
}
