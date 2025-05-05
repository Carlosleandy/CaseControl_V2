import type { RecommendationStatus, RecommendationStatusRow } from "../model/recommendationstatus"

export interface RecommendationStatusRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(recommendationstatusId: number):Promise<RecommendationStatus>, 
    add(recommendationstatus: RecommendationStatus): Promise<RecommendationStatusRow>,
    edit(recommendationstatus: RecommendationStatus): Promise<RecommendationStatusRow>,
    deleteRow(id: number): Promise<void>
}
