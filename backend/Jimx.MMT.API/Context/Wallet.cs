namespace Jimx.MMT.API.Context
{
	public class Wallet
	{
		public int Id { get; set; }
		public Guid UserId { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		public virtual ICollection<Section> Sections { get; set; }
	}
}
