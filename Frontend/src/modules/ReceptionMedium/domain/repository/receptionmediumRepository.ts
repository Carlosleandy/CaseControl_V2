import type { ReceptionMedium, ReceptionMediumRow } from "../model/receptionmedium"

export interface ReceptionMediumRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(receptionmediumId: number):Promise<ReceptionMedium>, 
    add(receptionmedium: ReceptionMedium): Promise<ReceptionMediumRow>,
    edit(receptionmedium: ReceptionMedium): Promise<ReceptionMediumRow>,
    deleteRow(id: number): Promise<void>
}
