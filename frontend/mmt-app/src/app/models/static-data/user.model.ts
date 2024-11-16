import { ItemGuid } from "../item-guid";
import { UserEdit } from "./user-edit.model";


export interface User extends UserEdit, ItemGuid {
}
