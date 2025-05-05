import type { Role, RoleRow } from "../model/role"

export interface RoleRepository {
    loadRecords (filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(roleId: number):Promise<Role>, 
    add(role: Role): Promise<RoleRow>,
    edit(role: Role): Promise<RoleRow>,
    deleteRow(id: number): Promise<void>
}
