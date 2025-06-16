using Jimx.Common.Models;

namespace Jimx.MMT.API.Models.StaticItems;

public class CategoryEditApi(string name, string description) : DictionaryItemWithDescriptionEdit(name, description);
