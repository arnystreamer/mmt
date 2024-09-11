import { Observable } from "rxjs";
import { ItemWithDescription } from "src/app/models/item-with-description";

export type ItemWithDescriptionCreator = (x: ItemWithDescription) => Observable<ItemWithDescription>;

export interface GlobalSectionDialogData {
  creator: ItemWithDescriptionCreator;
  name: string;
  description?: string;
}
