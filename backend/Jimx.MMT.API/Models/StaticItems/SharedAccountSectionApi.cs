using Jimx.Common.Models;

namespace Jimx.MMT.API.Models.StaticItems;

public class SharedAccountSectionApi(int id, int sharedAccountId, string name, string? description) : DictionaryItemWithDescription(id, name, description)
{
    public int SharedAccountId { get; init; } = sharedAccountId;
}
