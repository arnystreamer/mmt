using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Models.StaticItems;

public record SharedAccountSectionApi(int Id, int SharedAccountId, string Name, string? Description) : DictionaryItemWithDescription(Id, Name, Description);
