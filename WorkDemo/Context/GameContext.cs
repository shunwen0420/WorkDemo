using Microsoft.EntityFrameworkCore;
using WorkDemo.Models;

namespace WorkDemo.Context
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {

        }
        public DbSet<GameViewModel>Games { get; set; }
    }
}
