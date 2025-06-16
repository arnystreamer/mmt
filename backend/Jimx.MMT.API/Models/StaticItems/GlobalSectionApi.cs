using Jimx.Common.Models;

namespace Jimx.MMT.API.Models.StaticItems;

public class GlobalSectionApi(int id, string name, string? description)
    : DictionaryItemWithDescription(id, name, description);

