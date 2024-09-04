using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Models.StaticItems;

public record SharedAccountSectionEditApi(int Id, string Name, string Description) : DictionaryItemWithDescription(Id, Name, Description);
