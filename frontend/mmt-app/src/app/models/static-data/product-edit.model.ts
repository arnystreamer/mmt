
export interface ProductEdit {
  parentId: string | null;
  isExclusiveForCurrentUser: boolean;
  name: string;
  description?: string;
  sectionId: number;
  categoryId?: number;
  createTime: Date;
}
