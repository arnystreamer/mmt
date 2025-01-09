import { ReportByLocationItem } from "./report-by-location-item.model";

export interface ReportByLocation {
  items: ReportByLocationCopuntry[];
}

export interface ReportByLocationCopuntry
{
  countryCode: string;
  amount: number;
  items: ReportByLocationItem[];
}
