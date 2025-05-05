import type { WorkingGroup, WorkingGroupRow } from "../model/workinggroup"

export interface WorkingGroupRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(workinggroupId: number):Promise<WorkingGroup>, 
    add(workinggroup: WorkingGroup): Promise<WorkingGroupRow>,
    edit(workinggroup: WorkingGroup): Promise<WorkingGroupRow>,
    deleteRow(id: number): Promise<void>
}
