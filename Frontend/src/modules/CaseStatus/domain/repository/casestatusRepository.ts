import type { CaseStatus, CaseStatusRow } from "../model/casestatus"

export interface CaseStatusRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(casestatusId: number):Promise<CaseStatus>, 
    add(casestatus: CaseStatus): Promise<CaseStatusRow>,
    edit(casestatus: CaseStatus): Promise<CaseStatusRow>,
    deleteRow(id: number): Promise<void>
}
