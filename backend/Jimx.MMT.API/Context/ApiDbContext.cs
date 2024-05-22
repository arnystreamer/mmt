using Microsoft.EntityFrameworkCore;

namespace Jimx.MMT.API.Context
{
	public class ApiDbContext : DbContext
	{
		public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
		{

		}

		public DbSet<Wallet> Wallets { get; set; }
		public DbSet<Section> Sections { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<SharedAccount> SharedAccounts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryEntityTypeConfiguration).Assembly);
		}
	}
}
