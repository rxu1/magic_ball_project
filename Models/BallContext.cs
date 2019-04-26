using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MagicBall.Models
{
  public class BallContext : DbContext
  {
    public BallContext(DbContextOptions<BallContext> options): base(options) { }
    public DbSet<MagicBall> Questions { get; set; }
  }
}