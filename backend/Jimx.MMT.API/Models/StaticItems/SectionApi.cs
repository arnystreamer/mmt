using Jimx.Common.Models;

namespace Jimx.MMT.API.Models.StaticItems;

public class SectionApi(int id, SectionType type, string name, string? description) : DictionaryItemWithDescription(id, name, description)
{
    public SectionType Type { get; init; } = type;
}
