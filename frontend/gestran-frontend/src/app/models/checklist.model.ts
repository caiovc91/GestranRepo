export interface CheckListItem {
  itemTypeId: string;
  itemTypeName: string;
  isChecked: boolean;
  comments?: string;
}

export interface CheckList {
  id?: string;
  checkListName: string;
  executedById?: string;
  collectionId?: string;
  currentStatus?: string;
  inProgress?: boolean;
  isApproved?: boolean;
  createdAt?: string;
  updatedAt?: string;
  approvedAt?: string;
  checkListItems?: CheckListItem[];
}