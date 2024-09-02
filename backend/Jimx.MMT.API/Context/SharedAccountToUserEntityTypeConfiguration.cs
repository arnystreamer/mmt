using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimx.MMT.API.Context
{
	public class SharedAccountToUserEntityTypeConfiguration : IEntityTypeConfiguration<SharedAccountToUser>
	{
		public void Configure(EntityTypeBuilder<SharedAccountToUser> builder)
		{
			builder.HasOne(c => c.User).WithMany(u => u.SharedAccountToUsers)
				.HasForeignKey(c => c.UserId);
		}
	}
}
