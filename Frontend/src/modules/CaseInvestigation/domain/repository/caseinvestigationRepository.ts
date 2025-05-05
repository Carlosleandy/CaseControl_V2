import type { CaseInvestigation, CaseInvestigationRow } from "../model/caseinvestigation"

export interface CaseInvestigationRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(caseinvestigationId: number):Promise<CaseInvestigation>, 
    add(caseinvestigation: CaseInvestigation): Promise<CaseInvestigationRow>,
    edit(caseinvestigation: CaseInvestigation): Promise<CaseInvestigationRow>,
    deleteRow(id: number): Promise<void>
}
