import type { Linked, LinkedRow } from "../model/linked"

export interface LinkedRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(linkedId: number):Promise<Linked>, 
    add(linked: Linked): Promise<LinkedRow>,
    edit(linked: Linked): Promise<LinkedRow>,
    deleteRow(id: number): Promise<void>
}
