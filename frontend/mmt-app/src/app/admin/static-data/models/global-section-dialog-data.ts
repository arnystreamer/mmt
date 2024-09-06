import { ItemWithDescription } from "src/app/models/item-with-description";

export interface GlobalSectionDialogData {
  submitAsync: (item: ItemWithDescription) => Promise<boolean>;
  name: string;
  description?: string;
}
