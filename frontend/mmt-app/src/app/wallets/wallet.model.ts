import { ItemWithDescription } from "../models/item-with-description";

export interface Wallet extends ItemWithDescription {
  userId: string;
}
