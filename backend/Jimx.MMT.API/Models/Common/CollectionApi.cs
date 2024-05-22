namespace Jimx.MMT.API.Models.Common
{
	public record CollectionApi<T>(int Total, int Skip, int Take, int Count, T[] Items);
}
