using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimx.MMT.API.Context
{
	public class SectionEntityTypeConfiguration : IEntityTypeConfiguration<Section>
	{
		public void Configure(EntityTypeBuilder<Section> builder)
		{
			builder.HasOne(c => c.Wallet).WithMany(c => c.Sections)
				.HasForeignKey(c => c.WalletId);

			builder.HasOne(c => c.SharedAccount).WithMany(c => c.Sections)
				.HasForeignKey(c => c.SharedAccountId);

			builder.HasOne(c => c.User).WithMany(u => u.Sections)
				.HasForeignKey(c => c.UserId);
		}
	}
}
