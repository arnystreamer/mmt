import { ItemIdentity } from "../item-identity";
import { SectionCategoryEdit } from "./section-category-edit.model";

export interface SectionCategory extends SectionCategoryEdit, ItemIdentity {
  sectionId: number;
}
