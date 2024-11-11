namespace Jimx.MMT.API.Context
{
	public class ReceiptEntry
	{
		public Guid Id { get; set; }
		public Guid ReceiptId { get; set; }
		public Guid ProductId { get; set; }

		public decimal Quantity { get; set; }
		public decimal Price { get; set; }

		public DateTime CreateTime { get; set; }
		public Guid CreateUserId { get; set; }

		public virtual Receipt Receipt { get; set; }
		public virtual Product Product { get; set; }

		public virtual User CreateUser { get; set; }
	}
}
