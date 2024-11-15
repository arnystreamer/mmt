using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Models.StaticItems;

public record SectionEditApi(string Name, string? Description) : DictionaryItemWithDescriptionEdit(Name, Description);
