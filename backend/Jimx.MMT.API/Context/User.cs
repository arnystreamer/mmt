namespace Jimx.MMT.API.Context
{
	public class User
	{
		public Guid Id { get; set; }
		public string Login { get; set; }
		public string Name { get; set; }

		public ICollection<Wallet> Wallets { get; set; }
		public ICollection<Section> Sections { get; set; }
		public ICollection<SharedAccountToUser> SharedAccountToUsers { get; set; }
	}
}
