import type { FaultLinked, FaultLinkedRow } from "../model/faultlinked"

export interface FaultLinkedRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(faultlinkedId: number):Promise<FaultLinked>, 
    add(faultlinked: FaultLinked): Promise<FaultLinkedRow>,
    edit(faultlinked: FaultLinked): Promise<FaultLinkedRow>,
    deleteRow(id: number): Promise<void>
}
