﻿using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Context
{
	public class Wallet : IDictionaryItemWithDescriptionEdit
	{
		public int Id { get; set; }
		public Guid UserId { get; set; }

		public string Name { get; set; }
		public string? Description { get; set; }

		public virtual User User { get; set; }
		public virtual ICollection<Section> Sections { get; set; }
		public virtual ICollection<Receipt> Receipts { get; set; }
	}
}
