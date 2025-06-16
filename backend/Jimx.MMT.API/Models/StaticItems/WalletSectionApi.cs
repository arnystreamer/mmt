using Jimx.Common.Models;

namespace Jimx.MMT.API.Models.StaticItems;

public class WalletSectionApi(int id, int walletId, string name, string? description) : DictionaryItemWithDescription(id, name, description)
{
    public int WalletId { get; init; } = walletId;
}
