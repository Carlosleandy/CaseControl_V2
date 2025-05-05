import type { User, UserRow } from "../model/user"

export interface UserRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(userId: number):Promise<User>, 
    add(user: User): Promise<UserRow>,
    edit(user: User): Promise<UserRow>,
    deleteRow(id: number): Promise<void>
}
