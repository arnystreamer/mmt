using Jimx.Common.Models;

namespace Jimx.MMT.API.Models.StaticItems;

public class WalletEditApi(string name, string? description) : DictionaryItemWithDescriptionEdit(name, description);
