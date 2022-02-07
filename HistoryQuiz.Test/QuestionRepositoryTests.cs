using HistoryQuiz.Data;
using HistoryQuiz.Models;
using HistoryQuiz.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryQuiz.Test
{
    public class QuestionRepositoryTests
    {
        private static DbContextOptions<AppDbContext> _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(databaseName: "AnswerTest")
           .Options;
        private AppDbContext _context;
        private IRepository<Question> _questionRepository;

        [SetUp]
        public void Setup()
        {
            _context = new AppDbContext(_dbContextOptions);
            _context.Database.EnsureCreated();
            _questionRepository = new QuestionRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task AddQuestionAsync()
        {
            var question = new Question() { Id = 1, Title = "test", Content = "test", Image = "test" };

            await _questionRepository.AddAsync(question);
            var result = await _questionRepository.GetByIdAsync(1);

            result.Content.ShouldBe("test");
        }

        [Test]
        public async Task AddQuestionAsync_GivenInvalidQuestionObject_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _questionRepository.AddAsync(null));
        }

        [Test]
        public async Task DeleteQuestionAsync()
        {
            var question = new Question() { Id = 1, Title = "test", Content = "test", Image = "test" };

            await _questionRepository.AddAsync(question);
            await _questionRepository.DeleteAsync(question);
            var result = await _questionRepository.GetByIdAsync(1);

            result.ShouldBeNull();
        }

        [Test]
        public async Task DeleteQuestionAsync_GivenInvalidQuestion_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _questionRepository.DeleteAsync(null));
        }

        [Test]
        public async Task GetAllQuestionsAsync()
        {
            List<Question> questions = new List<Question>()
            {
                new Question() { Id = 1, Title = "test1", Content = "test1", Image = "test1" },
                new Question() { Id = 2, Title = "test2", Content = "test2", Image = "test2" },
                new Question() { Id = 3, Title = "test3", Content = "test3", Image = "test3" }
            };

            await _context.Questions.AddRangeAsync(questions);
            await _context.SaveChangesAsync();
            var result = await _questionRepository.GetAllAsync();

            result.Count().ShouldBe(3);
        }

        [Test]
        public async Task GetQuestionByIdAsync()
        {
            var question = new Question() { Id = 1, Title = "test", Content = "test", Image = "test" };

            await _questionRepository.AddAsync(question);
            var result = await _questionRepository.GetByIdAsync(1);

            result.Content.ShouldBe("test");
        }

        [Test]
        public async Task GetQuestionByIdAsync_GivenIdForQuestionThatDoesNotExist_ShouldBeNull()
        {
            var result = await _questionRepository.GetByIdAsync(1);

            result.ShouldBeNull();
        }

        [Test]
        public async Task GetQuestionByIdAsync_GivenInvalidId_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _questionRepository.GetByIdAsync(0));
        }

        [Test]
        public async Task UpdateQuestionAsync()
        {
            var question = new Question() { Id = 1, Title = "test", Content = "test", Image = "test" };

            await _questionRepository.AddAsync(question);
            question.Content = "new";
            var result = await _questionRepository.GetByIdAsync(1);

            result.Content.ShouldBe("new");
        }

        [Test]
        public async Task UpdateQuestionAsync_GivenInvalidQuestionObject_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _questionRepository.UpdateAsync(null));
        }
    }
}