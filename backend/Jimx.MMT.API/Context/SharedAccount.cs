using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Context
{
	public class SharedAccount : IDictionaryItemWithDescriptionEdit
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		public virtual ICollection<Section> Sections { get; set; }
		public virtual ICollection<SharedAccountToUser> SharedAccountToUsers { get; set; }
	}
}
