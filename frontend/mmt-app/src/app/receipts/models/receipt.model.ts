import { Currency } from "src/app/models/static-data/currency.model";
import { Location } from "src/app/models/static-data/location.model";
import { SharedAccount } from "src/app/models/static-data/shared-account.model";
import { User } from "src/app/models/static-data/user.model";
import { Wallet } from "src/app/models/static-data/wallet.model";
import { ReceiptEdit } from "./receipt-edit.model";
import { ItemGuid } from "src/app/models/item-guid";
import { ReceiptEntry } from "./receipt-entry.model";

export interface Receipt extends ReceiptEdit, ItemGuid {
  wallet? : Wallet;
  sharedAccount?: SharedAccount;
  location: Location;
  currency: Currency;
  createTime: Date;
  createUserId: string;
  createUser: User;
  entries: ReceiptEntry[];
}
