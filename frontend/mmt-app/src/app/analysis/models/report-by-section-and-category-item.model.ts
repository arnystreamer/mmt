import { Section } from "src/app/models/static-data/section.model";
import { ReportBySectionAndCategoryCatitem } from "./report-by-section-and-category-catitem.model";

export interface ReportBySectionAndCategoryItem {
  section: Section;
  categories: ReportBySectionAndCategoryCatitem[];
  amount: number;
}
