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
    public class AnswerRepositoryTests
    {
        private static DbContextOptions<AppDbContext> _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(databaseName: "AnswerTest")
           .Options;
        private AppDbContext _context;
        private IRepository<Answer> _answerRepository;

        [SetUp]
        public void Setup()
        {
            _context = new AppDbContext(_dbContextOptions);
            _context.Database.EnsureCreated();
            _answerRepository = new AnswerRepository(_context);

            var question = new Question() { Id = 1, Title = "test", Content = "test", Image = "test" };
            _context.Questions.Add(question);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task AddAnswerAsync()
        {
            var answer = new Answer() { Id = 1, Content = "test", QuestionId = 1 };

            await _answerRepository.AddAsync(answer);
            var result = await _answerRepository.GetByIdAsync(1);

            result.Content.ShouldBe("test");
        }

        [Test]
        public async Task AddAnswerAsync_GivenInvalidAnswerObject_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _answerRepository.AddAsync(null));
        }

        [Test]
        public async Task DeleteAnswerAsync()
        {
            var answer = new Answer() { Id = 1, Content = "test", QuestionId = 1 };

            await _answerRepository.AddAsync(answer);
            await _answerRepository.DeleteAsync(answer);
            var result = await _answerRepository.GetByIdAsync(1);

            result.ShouldBeNull();
        }

        [Test]
        public async Task DeleteAnswerAsync_GivenInvalidId_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _answerRepository.DeleteAsync(null));
        }

        [Test]
        public async Task GetAllAnswersAsync()
        {
            List<Answer> answers = new List<Answer>()
            {
                new Answer() { Id = 1, Content = "test1", QuestionId = 1 },
                new Answer() { Id = 2, Content = "test2", QuestionId = 1 },
                new Answer() { Id = 3, Content = "test3", QuestionId = 1 },
            };

            await _context.Answers.AddRangeAsync(answers);
            await _context.SaveChangesAsync();
            var result = await _answerRepository.GetAllAsync();

            result.Count().ShouldBe(3);
        }

        [Test]
        public async Task GetAnswerByIdAsync()
        {
            var answer = new Answer() { Id = 1, Content = "test", QuestionId = 1 };

            await _answerRepository.AddAsync(answer);
            var result = await _answerRepository.GetByIdAsync(1);

            result.Content.ShouldBe("test");
        }

        [Test]
        public async Task GetAnswerByIdAsync_GivenIdForAnswerThatDoesNotExist_ShouldBeNull()
        {
            var result = await _answerRepository.GetByIdAsync(1);

            result.ShouldBeNull();
        }

        [Test]
        public async Task GetAnswerByIdAsync_GivenInvalidId_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _answerRepository.GetByIdAsync(0));
        }

        [Test]
        public async Task UpdateAnswerAsync()
        {
            var answer = new Answer() { Id = 1, Content = "test", QuestionId = 1 };

            await _answerRepository.AddAsync(answer);
            answer.Content = "new";
            await _answerRepository.UpdateAsync(answer);
            var result = await _answerRepository.GetByIdAsync(1);

            result.Content.ShouldBe("new");
        }

        [Test]
        public async Task UpdateAnswerAsync_GivenInvalidAnswer_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _answerRepository.UpdateAsync(null));
        }
    }
}
