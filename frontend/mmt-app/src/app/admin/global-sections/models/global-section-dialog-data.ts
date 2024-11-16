import { Observable } from "rxjs";
import { SectionEdit } from "src/app/models/static-data/section-edit.model";
import { Section } from "src/app/models/static-data/section.model";

export type GlobalSectionCreator = (x: SectionEdit) => Observable<Section>;

export interface GlobalSectionDialogData {
  creator: GlobalSectionCreator;
  name: string;
  description?: string;
}
