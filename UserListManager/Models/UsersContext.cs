using Microsoft.EntityFrameworkCore;

namespace UserListManager.Models;

public class UsersContext : DbContext
{
	public DbSet<User> Users { get; set; }

	public UsersContext(DbContextOptions<UsersContext> options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlite(@"Data Source=database.db");
}
