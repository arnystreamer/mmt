namespace Jimx.MMT.API.Context
{
	public class Section
	{
		public int Id { get; set; }

		public int? WalletId { get; set; }
		public Guid? UserId { get; set; }
		public int? SharedAccountId { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		public virtual Wallet? Wallet { get; set; }
		public virtual SharedAccount? SharedAccount { get; set; }
		public virtual ICollection<Category> Categories { get; set; }
	}
}
