//using HistoryQuiz.Data;
//using HistoryQuiz.Models;
//using HistoryQuiz.Repositories;
//using Microsoft.EntityFrameworkCore;
//using NUnit.Framework;
//using Shouldly;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HistoryQuiz.Test
//{
//    public class AnswerRepositoryTests
//    {
//        private static DbContextOptions<AppDbContext> _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
//           .UseInMemoryDatabase(databaseName: "AnswerTest")
//           .Options;
//        private AppDbContext _context;
//        private IAnswerRepository _answerRepository;

//        [SetUp]
//        public void Setup()
//        {
//            _context = new AppDbContext(_dbContextOptions);
//            _context.Database.EnsureCreated();
//            _answerRepository = new AnswerRepository(_context);

//            var question = new Question() { Id = 1, Title = "test", Content = "test", Image = "test" };
//            _context.Questions.Add(question);
//        }

//        [TearDown]
//        public void Teardown()
//        {
//            _context.Database.EnsureDeleted();
//        }

//        [Test]
//        public async Task AddAnswerAsync()
//        {
//            var answer = new Answer() { Id = 1, Content = "test", QuestionId = 1 };

//            await _answerRepository.AddAnswerAsync(answer);

//            var result = await _answerRepository.GetAnswerByIdAsync(1);
//            result.Content.ShouldBe("test");
//        }

//        [Test]
//        public async Task AddAnswerAsync_GivenInvalidAnswerObject_ShouldThrowArgumentNullException()
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _answerRepository.AddAnswerAsync(null));
//        }

//        [Test]
//        public async Task DeleteAnswerAsync()
//        {
//            var answer = new Answer() { Id = 1, Content = "test", QuestionId = 1 };

//            await _answerRepository.AddAnswerAsync(answer);
//            await _answerRepository.DeleteAnswerAsync(1);

//            var result = await _answerRepository.GetAnswerByIdAsync(1);
//            result.ShouldBeNull();
//        }

//        [TestCase(0)]
//        [TestCase(1)]
//        public async Task DeleteAnswerAsync_GivenInvalidId_ShouldThrowArgumentNullException(int id)
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _answerRepository.DeleteAnswerAsync(id));
//        }

//        [Test]
//        public async Task GetAllAnswersAsync()
//        {
//            List<Answer> answers = new List<Answer>()
//            {
//                new Answer() { Id = 1, Content = "test1", QuestionId = 1 },
//                new Answer() { Id = 2, Content = "test2", QuestionId = 1 },
//                new Answer() { Id = 3, Content = "test3", QuestionId = 1 },
//            };

//            await _context.Answers.AddRangeAsync(answers);
//            await _context.SaveChangesAsync();

//            var result = await _answerRepository.GetAllAnswersAsync();
//            result.Count().ShouldBe(3);
//        }

//        [Test]
//        public async Task GetAllAnswersByQuestionIdAsync()
//        {
//            List<Answer> answers = new List<Answer>()
//            {
//                new Answer() { Id = 1, Content = "test1", QuestionId = 1 },
//                new Answer() { Id = 2, Content = "test2", QuestionId = 1 },
//                new Answer() { Id = 3, Content = "test3", QuestionId = 1 },
//            };

//            await _context.Answers.AddRangeAsync(answers);
//            await _context.SaveChangesAsync();

//            var result = await _answerRepository.GetAllAnswersByQuestionIdAsync(1);
//            result.Count().ShouldBe(3);
//        }

//        [Test]
//        public async Task GetAllAnswersByQuestionIdAsync_GivenQuestionIdForAnswersThatDoNotExist_ShouldBeZero()
//        {
//            var result = await _answerRepository.GetAllAnswersByQuestionIdAsync(1);
//            result.Count().ShouldBe(0);
//        }

//        [TestCase]
//        public async Task GetAllAnswersByQuestionIdAsync_GivenInvalidQuestionId_ShouldThrowArgumentNullException()
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _answerRepository.GetAllAnswersByQuestionIdAsync(0));
//        }

//        [Test]
//        public async Task GetAnswerByIdAsync()
//        {
//            var answer = new Answer() { Id = 1, Content = "test", QuestionId = 1 };

//            await _answerRepository.AddAnswerAsync(answer);

//            var result = await _answerRepository.GetAnswerByIdAsync(1);
//            result.Content.ShouldBe("test");
//        }

//        [Test]
//        public async Task GetAnswerByIdAsync_GivenIdForAnswerThatDoesNotExist_ShouldBeNull()
//        {
//            var result = await _answerRepository.GetAnswerByIdAsync(1);
//            result.ShouldBeNull();
//        }

//        [Test]
//        public async Task GetAnswerByIdAsync_GivenInvalidId_ShouldThrowArgumentNullException()
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _answerRepository.GetAnswerByIdAsync(0));
//        }

//        [Test]
//        public async Task UpdateAnswerAsync()
//        {
//            var answer = new Answer() { Id = 1, Content = "test", QuestionId = 1 };

//            await _answerRepository.AddAnswerAsync(answer);
//            answer.Content = "new";
//            await _answerRepository.UpdateAnswerAsync(answer);

//            var result = await _answerRepository.GetAnswerByIdAsync(1);
//            result.Content.ShouldBe("new");
//        }

//        [Test]
//        public async Task UpdateAnswerAsync_GivenInvalidAnswer_ShouldThrowArgumentNullException()
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _answerRepository.UpdateAnswerAsync(null));
//        }
//    }
//}
