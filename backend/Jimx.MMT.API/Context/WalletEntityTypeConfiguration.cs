using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimx.MMT.API.Context
{
	public class WalletEntityTypeConfiguration : IEntityTypeConfiguration<Wallet>
	{
		public void Configure(EntityTypeBuilder<Wallet> builder)
		{
			builder.HasOne(c => c.User).WithMany(u => u.Wallets)
				.HasForeignKey(c => c.UserId);
		}
	}
}
