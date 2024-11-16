import { ItemIdentity } from "../item-identity";
import { WalletEdit } from "./wallet-edit.model";


export interface Wallet extends WalletEdit, ItemIdentity {
  userId: string;
}
