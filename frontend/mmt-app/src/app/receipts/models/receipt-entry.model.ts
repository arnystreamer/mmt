import { ItemGuid } from "src/app/models/item-guid";
import { User } from "src/app/models/static-data/user.model";
import { ReceiptEntryEdit } from "./receipt-entry-edit.model";
import { Product } from "src/app/models/static-data/product.model";

export interface ReceiptEntry extends ReceiptEntryEdit, ItemGuid {
  product: Product;
  createUserId: string;
  createUser: User;
}
