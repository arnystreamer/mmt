import { Observable } from "rxjs";
import { GlobalSection } from "./global-section.model";

export type GlobalSectionCreator = (x: GlobalSection) => Observable<GlobalSection>;

export interface GlobalSectionDialogData {
  creator: GlobalSectionCreator;
  name: string;
  description?: string;
}
