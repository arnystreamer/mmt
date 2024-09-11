﻿using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Models.StaticItems;

public record CategoryEditApi(int Id, string Name, string Description) : DictionaryItemWithDescription(Id, Name, Description);