using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimx.MMT.API.Context
{
	public class SharedAccountEntityTypeConfiguration : IEntityTypeConfiguration<SharedAccount>
	{
		public void Configure(EntityTypeBuilder<SharedAccount> builder)
		{
			builder.HasMany(s => s.SharedAccountToUsers).WithOne(su => su.SharedAccount)
				.HasForeignKey(s => s.SharedAccountId);

			builder.HasMany(s => s.Sections).WithOne(s => s.SharedAccount)
				.HasForeignKey(s => s.SharedAccountId);
		}
	}
}
