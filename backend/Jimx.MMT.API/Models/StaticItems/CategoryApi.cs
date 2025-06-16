using Jimx.Common.Models;

namespace Jimx.MMT.API.Models.StaticItems;

public class CategoryApi(int id, int sectionId, SectionApi? section, string name, string? description) : DictionaryItemWithDescription(id, name, description)
{
    public int SectionId { get; init; } = sectionId;
    public SectionApi? Section { get; init; } = section;

}
