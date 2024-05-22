namespace Jimx.MMT.API.Models.StaticItems
{
	public record WalletSectionsCategoriesApi(int Id, Guid UserId, string Name, string Description, SectionCategoriesApi[] SectionCategories)
		: WalletApi(Id, UserId, Name, Description);
}
