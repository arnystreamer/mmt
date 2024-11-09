import { ItemWithDescription } from "../item-with-description";

export interface SectionCategory extends ItemWithDescription {
  sectionId: number | undefined;
}
