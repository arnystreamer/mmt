namespace Jimx.MMT.API.Context
{
	public class SharedAccountToUser
	{
		public int Id { get; set; }
		public int SharedAccountId { get; set; }
		public Guid UserId { get; set; }

		public virtual SharedAccount SharedAccount { get; set; }
		public virtual User User { get; set; }
	}
}
