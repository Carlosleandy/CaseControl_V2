import type { Fault, FaultRow } from "../model/fault"

export interface FaultRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(faultId: number):Promise<Fault>, 
    add(fault: Fault): Promise<FaultRow>,
    edit(fault: Fault): Promise<FaultRow>,
    deleteRow(id: number): Promise<void>
}
