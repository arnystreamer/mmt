using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Models.StaticItems;

public record WalletEditApi(string Name, string? Description) : DictionaryItemWithDescriptionEdit(Name, Description);
