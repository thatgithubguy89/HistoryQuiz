//using HistoryQuiz.Data;
//using HistoryQuiz.Models;
//using HistoryQuiz.Repositories;
//using Microsoft.EntityFrameworkCore;
//using NUnit.Framework;
//using Shouldly;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace HistoryQuiz.Test
//{
//    public class QuestionRepositoryTests
//    {
//        private static DbContextOptions<AppDbContext> _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
//           .UseInMemoryDatabase(databaseName: "AnswerTest")
//           .Options;
//        private AppDbContext _context;
//        private IQuestionRepository _questionRepository;

//        [SetUp]
//        public void Setup()
//        {
//            _context = new AppDbContext(_dbContextOptions);
//            _context.Database.EnsureCreated();
//            _questionRepository = new QuestionRepository(_context);
//        }

//        [TearDown]
//        public void Teardown()
//        {
//            _context.Database.EnsureDeleted();
//        }

//        [Test]
//        public async Task AddQuestionAsync()
//        {
//            var question = new Question() { Id = 1, Title = "test", Content = "test", Image = "test" };

//            await _questionRepository.AddQuestionAsync(question);

//            var result = await _questionRepository.GetQuestionByIdAsync(1);
//            result.Content.ShouldBe("test");
//        }

//        [Test]
//        public async Task AddQuestionAsync_GivenInvalidQuestionObject_ShouldThrowArgumentNullException()
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _questionRepository.AddQuestionAsync(null));
//        }

//        [Test]
//        public async Task DeleteQuestionAsync()
//        {
//            var question = new Question() { Id = 1, Title = "test", Content = "test", Image = "test" };

//            await _questionRepository.AddQuestionAsync(question);
//            await _questionRepository.DeleteQuestionAsync(1);

//            var result = await _questionRepository.GetQuestionByIdAsync(1);
//            result.ShouldBeNull();
//        }

//        [TestCase(0)]
//        [TestCase(1)]
//        public async Task DeleteQuestionAsync_GivenInvalidId_ShouldThrowArgumentNullException(int id)
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _questionRepository.DeleteQuestionAsync(id));
//        }

//        [Test]
//        public async Task GetAllQuestionsAsync()
//        {
//            List<Question> questions = new List<Question>()
//            {
//                new Question() { Id = 1, Title = "test1", Content = "test1", Image = "test1" },
//                new Question() { Id = 2, Title = "test2", Content = "test2", Image = "test2" },
//                new Question() { Id = 3, Title = "test3", Content = "test3", Image = "test3" }
//            };

//            await _context.Questions.AddRangeAsync(questions);
//            await _context.SaveChangesAsync();

//            var result = await _questionRepository.GetAllQuestionsAsync();
//            result.Count().ShouldBe(3);
//        }

//        [Test]
//        public async Task GetQuestionByIdAsync()
//        {
//            var question = new Question() { Id = 1, Title = "test", Content = "test", Image = "test" };

//            await _questionRepository.AddQuestionAsync(question);

//            var result = await _questionRepository.GetQuestionByIdAsync(1);
//            result.Content.ShouldBe("test");
//        }

//        [Test]
//        public async Task GetQuestionByIdAsync_GivenIdForQuestionThatDoesNotExist_ShouldBeNull()
//        {
//            var result = await _questionRepository.GetQuestionByIdAsync(1);
//            result.ShouldBeNull();
//        }

//        [Test]
//        public async Task GetQuestionByIdAsync_GivenInvalidId_ShouldThrowArgumentNullException()
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _questionRepository.GetQuestionByIdAsync(0));
//        }

//        [Test]
//        public async Task UpdateQuestionAsync()
//        {
//            var question = new Question() { Id = 1, Title = "test", Content = "test", Image = "test" };

//            await _questionRepository.AddQuestionAsync(question);
//            question.Content = "new";

//            var result = await _questionRepository.GetQuestionByIdAsync(1);
//            result.Content.ShouldBe("new");
//        }

//        [Test]
//        public async Task UpdateQuestionAsync_GivenInvalidQuestionObject_ShouldThrowArgumentNullException()
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _questionRepository.UpdateQuestionAsync(null));
//        }
//    }
//}