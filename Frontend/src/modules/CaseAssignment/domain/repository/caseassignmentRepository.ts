import type { CaseAssignment, CaseAssignmentRow } from "../model/caseassignment"

export interface CaseAssignmentRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(caseassignmentId: number):Promise<CaseAssignment>, 
    add(caseassignment: CaseAssignment): Promise<CaseAssignmentRow>,
    edit(caseassignment: CaseAssignment): Promise<CaseAssignmentRow>,
    deleteRow(id: number): Promise<void>
}
