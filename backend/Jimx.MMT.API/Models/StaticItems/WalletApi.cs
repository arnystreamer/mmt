using Jimx.Common.Models;

namespace Jimx.MMT.API.Models.StaticItems;

public class WalletApi(int id, Guid userId, string name, string? description) : DictionaryItemWithDescription(id, name, description)
{
    public Guid UserId { get; init; } = userId;
}
