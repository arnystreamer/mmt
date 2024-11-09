import { ItemWithDescription } from "../../models/item-with-description";

export interface WalletSection extends ItemWithDescription {
  walletId: number | undefined;
}
