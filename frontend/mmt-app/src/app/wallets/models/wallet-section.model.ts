import { ItemWithDescriptionEdit } from "src/app/models/item-with-description-edit";
import { ItemIdentity } from "src/app/models/item-identity";

export interface WalletSection extends WalletSectionEdit, ItemIdentity {
  walletId: number;
}

export interface WalletSectionEdit extends ItemWithDescriptionEdit {

}
