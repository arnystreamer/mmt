namespace Jimx.MMT.API.Models.StaticItems
{
	public record SectionCategoriesApi(int Id, int WalletId, string Name, string Description, CategoryApi[] Sections) 
		: SectionApi(Id, WalletId, Name, Description);
}
