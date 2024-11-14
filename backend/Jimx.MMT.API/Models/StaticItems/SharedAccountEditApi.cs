using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Models.StaticItems;

public record SharedAccountEditApi(string Name, string Description) : DictionaryItemWithDescriptionEdit(Name, Description);
