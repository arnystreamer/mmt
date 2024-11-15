export interface Receipt extends ReceiptEdit {
  id: number;

}

export interface ReceiptEdit {
  date: Date;
  walletId?: number;
  sharedAccountId?: number;
  locationId: number;
  currencyId: number;
  comment?: string;
}

