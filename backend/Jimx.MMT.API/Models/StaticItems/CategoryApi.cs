using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Models.StaticItems;

public record CategoryApi(int Id, int SectionId, string Name, string? Description) : DictionaryItemWithDescription(Id, Name, Description);
