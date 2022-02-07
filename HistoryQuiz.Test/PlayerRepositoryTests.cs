using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryQuiz.Data;
using HistoryQuiz.Models;
using HistoryQuiz.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace HistoryQuiz.Test
{
    public class PlayerRepositoryTests
    {
        private static DbContextOptions<AppDbContext> _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "PlayerTest")
            .Options;
        private AppDbContext _context;
        private IRepository<Player> _playerRepository;

        [SetUp]
        public void Setup()
        {
            _context = new AppDbContext(_dbContextOptions);
            _context.Database.EnsureCreated();
            _playerRepository = new PlayerRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task CreatePlayerAsync()
        {
            var player = new Player() { Id = 1, Initials = "plr" };

            await _playerRepository.AddAsync(player);

            var result = await _playerRepository.GetByIdAsync(1);
            result.Initials.ShouldBe("plr");
        }

        [Test]
        public async Task CreatePlayerAsync_GivenInvalidPlayerObject_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _playerRepository.AddAsync(null));
        }

        [Test]
        public async Task DeletePlayerAsync()
        {
            var player = new Player() { Id = 1, Initials = "plr" };

            await _playerRepository.AddAsync(player);
            await _playerRepository.DeleteAsync(player);

            var result = await _playerRepository.GetByIdAsync(1);
            result.ShouldBeNull();
        }

        [Test]
        public async Task DeletePlayerAsync_GivenInvalidPlayerObject_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _playerRepository.DeleteAsync(null));
        }

        [Test]
        public async Task GetAllPlayersAsync()
        {
            List<Player> players = new List<Player>()
            {
                new Player() { Id = 1, Initials = "pl1" },
                new Player() { Id = 2, Initials = "pl2" },
                new Player() { Id = 3, Initials = "pl3" }
            };

            await _context.Players.AddRangeAsync(players);
            await _context.SaveChangesAsync();
            var result = await _playerRepository.GetAllAsync();

            result.Count().ShouldBe(3);
        }

        [Test]
        public async Task GetPlayerByIdAsync()
        {
            var player = new Player() { Id = 1, Initials = "plr" };

            await _playerRepository.AddAsync(player);
            var result = await _playerRepository.GetByIdAsync(1);

            result.Initials.ShouldBe("plr");
        }

        [Test]
        public async Task GetPlayerByIdAsync_GivenIdForPlayerThatDoesNotExist_ShouldBeNull()
        {
            var result = await _playerRepository.GetByIdAsync(1);

            result.ShouldBeNull();
        }

        [Test]
        public async Task GetPlayerByIdAsync_GivenInvalidId_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _playerRepository.GetByIdAsync(0));
        }

        [Test]
        public async Task UpdatePlayerAsync()
        {
            var player = new Player() { Id = 1, Initials = "plr" };

            await _playerRepository.AddAsync(player);
            player.Initials = "new";
            await _playerRepository.UpdateAsync(player);
            var result = await _playerRepository.GetByIdAsync(1);

            result.Initials.ShouldBe("new");
        }

        [Test]
        public async Task UpdatePlayerAsync_GivenInvalidPlayerObject_ShouldThrowArgumentNullException()
        {
            await Should.ThrowAsync<ArgumentNullException>(async () => await _playerRepository.UpdateAsync(null));
        }
    }
}
