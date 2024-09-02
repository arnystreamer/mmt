namespace Jimx.MMT.API.Models.StaticItems
{
	public record GlobalSectionCategoriesApi(int Id, string Name, string Description, CategoryApi[] Sections)
		: GlobalSectionApi(Id, Name, Description);
}
