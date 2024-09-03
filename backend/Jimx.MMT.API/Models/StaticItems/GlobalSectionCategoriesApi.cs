namespace Jimx.MMT.API.Models.StaticItems
{
	public record GlobalSectionCategoriesApi(int Id, string Name, string Description, CategoryApi[] Categories)
		: GlobalSectionApi(Id, Name, Description);
}
