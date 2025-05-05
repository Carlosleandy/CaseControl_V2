import type { UserLevel, UserLevelRow } from "../model/userlevel"

export interface UserLevelRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(userlevelId: number):Promise<UserLevel>, 
    add(userlevel: UserLevel): Promise<UserLevelRow>,
    edit(userlevel: UserLevel): Promise<UserLevelRow>,
    deleteRow(id: number): Promise<void>
}
