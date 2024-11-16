import { ItemIdentity } from "../item-identity";
import { CategoryEdit } from "./category-edit.model";
import { Section } from "./section.model";


export interface Category extends CategoryEdit, ItemIdentity {
  sectionId: number;
  section: Section;
}
