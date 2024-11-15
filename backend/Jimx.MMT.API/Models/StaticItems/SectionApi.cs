using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Models.StaticItems;

public record SectionApi(int Id, SectionType Type, string Name, string? Description) : DictionaryItemWithDescription(Id, Name, Description);
