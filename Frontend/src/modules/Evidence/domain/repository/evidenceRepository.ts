import type { Evidence, EvidenceRow } from "../model/evidence"

export interface EvidenceRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(evidenceId: number):Promise<Evidence>, 
    add(evidence: Evidence): Promise<EvidenceRow>,
    edit(evidence: Evidence): Promise<EvidenceRow>,
    deleteRow(id: number): Promise<void>
}
