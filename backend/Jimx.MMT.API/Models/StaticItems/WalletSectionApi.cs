﻿using Jimx.MMT.API.Models.Common;

namespace Jimx.MMT.API.Models.StaticItems;

public record WalletSectionApi(int Id, int WalletId, string Name, string? Description) : DictionaryItemWithDescription(Id, Name, Description);
