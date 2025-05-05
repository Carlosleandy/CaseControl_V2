import type { Binnacle, BinnacleRow } from "../model/binnacle"

export interface BinnacleRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(binnacleId: number):Promise<Binnacle>, 
    add(binnacle: Binnacle): Promise<BinnacleRow>,
    edit(binnacle: Binnacle): Promise<BinnacleRow>,
    deleteRow(id: number): Promise<void>
}
