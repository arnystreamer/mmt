import { ItemGuid } from "../item-guid";
import { Category } from "./category.model";
import { ProductEdit } from "./product-edit.model";
import { Section } from "./section.model";

export interface Product extends ProductEdit, ItemGuid
{
  section: Section;
  category?: Category
}
