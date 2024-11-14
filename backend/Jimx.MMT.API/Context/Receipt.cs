namespace Jimx.MMT.API.Context
{
	public class Receipt
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public DateTime Date { get; set; }
		public int LocationId { get; set; }
		public int CurrencyId { get; set; }
		public string? Comment { get; set; }

		public DateTime CreateTime { get; set; }
		public Guid CreateUserId { get; set; }

		public virtual User User { get; set; }
		public virtual Location Location { get; set; }
		public virtual Currency Currency { get; set; }
		public virtual ICollection<ReceiptEntry> ReceiptEntries { get; set; } = new List<ReceiptEntry>();

		public virtual User CreateUser { get; set; }
	}
}
