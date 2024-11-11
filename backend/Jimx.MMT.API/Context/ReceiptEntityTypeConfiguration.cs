using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimx.MMT.API.Context
{
	public class ReceiptEntityTypeConfiguration : IEntityTypeConfiguration<Receipt>
	{
		public void Configure(EntityTypeBuilder<Receipt> builder)
		{
			builder.HasOne(r => r.Currency).WithMany().HasForeignKey(r => r.CurrencyId);
			builder.HasOne(r => r.Location).WithMany().HasForeignKey(r => r.LocationId);
			builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
			builder.HasOne(r => r.CreateUser).WithMany().HasForeignKey(r => r.CreateUserId);
		}
	}
}
