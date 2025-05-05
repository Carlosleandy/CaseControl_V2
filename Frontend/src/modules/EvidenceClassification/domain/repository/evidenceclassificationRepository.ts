import type { EvidenceClassification, EvidenceClassificationRow } from "../model/evidenceclassification"

export interface EvidenceClassificationRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(evidenceclassificationId: number):Promise<EvidenceClassification>, 
    add(evidenceclassification: EvidenceClassification): Promise<EvidenceClassificationRow>,
    edit(evidenceclassification: EvidenceClassification): Promise<EvidenceClassificationRow>,
    deleteRow(id: number): Promise<void>
}
