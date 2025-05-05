import type { CaseType, CaseTypeRow } from "../model/casetype"

export interface CaseTypeRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(casetypeId: number):Promise<CaseType>,   
    add(casetype: CaseType): Promise<CaseTypeRow>
    edit(casetype: CaseType): Promise<CaseTypeRow>
    deleteRow(id: number): Promise<void>
}
