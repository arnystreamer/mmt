
export interface ProductEdit {
  parentId: string;
  isExclusiveForCurrentUser: boolean;
  name: string;
  description?: string;
  sectionId: number;
  categoryId?: number;
  createTime: Date;
}
