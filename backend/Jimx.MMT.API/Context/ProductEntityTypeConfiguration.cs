using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jimx.MMT.API.Context
{
	public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasOne(p => p.ParentProduct).WithMany(p => p.ChildProducts).HasForeignKey(p => p.ParentId);

			builder.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);

			builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
			builder.HasOne(p => p.Section).WithMany().HasForeignKey(p => p.SectionId);

			builder.HasOne(p => p.CreateUser).WithMany().HasForeignKey(p => p.CreateUserId);
		}
	}
}
