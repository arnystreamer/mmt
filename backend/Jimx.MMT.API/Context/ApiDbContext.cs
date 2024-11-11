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
		public DbSet<SharedAccountToUser> SharedAccountToUsers { get; set; }
		public DbSet<User> Users { get; set; }

		public DbSet<Currency> Currencies { get; set; }
		public DbSet<Location> Location { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Receipt> Receipts { get; set; }
		public DbSet<ReceiptEntry> ReceiptEntries { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryEntityTypeConfiguration).Assembly);
		}
	}
}
