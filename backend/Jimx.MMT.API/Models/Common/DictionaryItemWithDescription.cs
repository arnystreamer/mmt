using Jimx.MMT.API.Services.DbWrapper;

namespace Jimx.MMT.API.Models.Common
{
	public record DictionaryItemWithDescription(int Id, string Name, string? Description): IDictionaryItemWithDescription;
}
