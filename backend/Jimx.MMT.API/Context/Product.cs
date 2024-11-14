using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Context
{
	public class Product : IDictionaryItemWithDescriptionEdit
	{
		public Guid Id { get; set; }
		public Guid? ParentId { get; set; }
		public Guid? UserId { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		public int? CategoryId { get; set; }
		public int? SectionId { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime CreateTime { get; set; }
		public Guid CreateUserId { get; set; }

		public virtual ICollection<Product> ChildProducts { get; set; } = new List<Product>();
		public virtual Product? ParentProduct { get; set; }

		public virtual User? User { get; set; }

		public virtual Category? Category { get; set; }
		public virtual Section? Section { get; set; }

		public virtual User CreateUser { get; set; }
	}
}
