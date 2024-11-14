namespace Jimx.MMT.API.Models.Common
{
	public interface IDictionaryItemWithDescription
	{
		int Id { get; }
		string Name { get; }
		string? Description { get; }
	}
}
