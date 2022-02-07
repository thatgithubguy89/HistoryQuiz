//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using HistoryQuiz.Data;
//using HistoryQuiz.Models;
//using HistoryQuiz.Repositories;
//using Microsoft.EntityFrameworkCore;
//using NUnit.Framework;
//using Shouldly;

//namespace HistoryQuiz.Test
//{
//    public class PlayerRepositoryTests
//    {
//        private static DbContextOptions<AppDbContext> _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
//            .UseInMemoryDatabase(databaseName: "PlayerTest")
//            .Options;
//        private AppDbContext _context;
//        private IPlayerRepository _playerRepository;

//        [SetUp]
//        public void Setup()
//        {
//            _context = new AppDbContext(_dbContextOptions);
//            _context.Database.EnsureCreated();
//            _playerRepository = new PlayerRepository(_context);
//        }

//        [TearDown]
//        public void Teardown()
//        {
//            _context.Database.EnsureDeleted();
//        }

//        [Test]
//        public async Task CreatePlayerAsync()
//        {
//            var player = new Player() { Id = 1, Initials = "plr" };

//            await _playerRepository.CreatePlayerAsync(player);

//            var result = await _playerRepository.GetPlayerByIdAsync(1);
//            result.Initials.ShouldBe("plr");
//        }

//        [Test]
//        public async Task CreatePlayerAsync_GivenInvalidPlayerObject_ShouldThrowArgumentNullException()
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _playerRepository.CreatePlayerAsync(null));
//        }

//        [Test]
//        public async Task DeletePlayerAsync()
//        {
//            var player = new Player() { Id = 1, Initials = "plr" };

//            await _playerRepository.CreatePlayerAsync(player);
//            await _playerRepository.DeletePlayerAsync(player);

//            var result = await _playerRepository.GetPlayerByIdAsync(1);
//            result.ShouldBeNull();
//        }

//        [Test]
//        public async Task DeletePlayerAsync_GivenInvalidPlayerObject_ShouldThrowArgumentNullException()
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _playerRepository.DeletePlayerAsync(null));
//        }

//        [Test]
//        public async Task GetAllPlayersAsync()
//        {
//            List<Player> players = new List<Player>()
//            {
//                new Player() { Id = 1, Initials = "pl1" },
//                new Player() { Id = 2, Initials = "pl2" },
//                new Player() { Id = 3, Initials = "pl3" }
//            };

//            await _context.Players.AddRangeAsync(players);
//            await _context.SaveChangesAsync();

//            var result = await _playerRepository.GetAllPlayersAsync();
//            result.Count().ShouldBe(3);
//        }

//        [Test]
//        public async Task GetPlayerByIdAsync()
//        {
//            var player = new Player() { Id = 1, Initials = "plr" };

//            await _playerRepository.CreatePlayerAsync(player);

//            var result = await _playerRepository.GetPlayerByIdAsync(1);
//            result.Initials.ShouldBe("plr");
//        }

//        [Test]
//        public async Task GetPlayerByIdAsync_GivenIdForPlayerThatDoesNotExist_ShouldBeNull()
//        {
//            var result = await _playerRepository.GetPlayerByIdAsync(1);
//            result.ShouldBeNull();
//        }

//        [Test]
//        public async Task GetPlayerByIdAsync_GivenInvalidId_ShouldThrowArgumentNullException()
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _playerRepository.GetPlayerByIdAsync(0));
//        }

//        [Test]
//        public async Task GetPlayerByInitialsAsync()
//        {
//            var player = new Player() { Id = 1, Initials = "plr" };

//            await _playerRepository.CreatePlayerAsync(player);

//            var result = await _playerRepository.GetPlayerByInitialsAsync("plr");
//            result.Id.ShouldBe(1);
//        }

//        [Test]
//        public async Task GetPlayerByInitialsAsync_GivenInitialsForPlayerThatDoesNotExist_ShouldBeNull()
//        {
//            var result = await _playerRepository.GetPlayerByInitialsAsync("plr");
//            result.ShouldBeNull();
//        }

//        [Test]
//        public async Task GetPlayerByInitialsAsync_GivenInvalidInitials_ShouldThrowArgumentNullException()
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _playerRepository.GetPlayerByInitialsAsync(""));
//        }

//        [Test]
//        public async Task UpdatePlayerAsync()
//        {
//            var player = new Player() { Id = 1, Initials = "plr" };

//            await _playerRepository.CreatePlayerAsync(player);
//            player.Initials = "new";
//            await _playerRepository.UpdatePlayerAsync(player);

//            var result = await _playerRepository.GetPlayerByIdAsync(1);
//            result.Initials.ShouldBe("new");
//        }

//        [Test]
//        public async Task UpdatePlayerAsync_GivenInvalidPlayerObject_ShouldThrowArgumentNullException()
//        {
//            await Should.ThrowAsync<ArgumentNullException>(async () => await _playerRepository.UpdatePlayerAsync(null));
//        }
//    }
//}
