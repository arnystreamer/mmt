namespace Jimx.MMT.API.Models.StaticItems
{
	public record WalletSectionsApi(int Id, Guid UserId, string Name, string Description, SectionApi[] Sections)
		: WalletApi(Id, UserId, Name, Description);
}
