using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimx.MMT.API.Context
{
	public class ReceiptEntryEntityTypeConfiguration : IEntityTypeConfiguration<ReceiptEntry>
	{
		public void Configure(EntityTypeBuilder<ReceiptEntry> builder)
		{
			builder.HasOne(re => re.Receipt).WithMany(r => r.ReceiptEntries).HasForeignKey(r => r.ReceiptId);
			builder.HasOne(re => re.Product).WithMany().HasForeignKey(r => r.ProductId);
			builder.HasOne(re => re.CreateUser).WithMany().HasForeignKey(r => r.CreateUserId);
		}
	}
}
