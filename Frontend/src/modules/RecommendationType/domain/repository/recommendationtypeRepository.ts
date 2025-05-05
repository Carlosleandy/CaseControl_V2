import type { RecommendationType, RecommendationTypeRow } from "../model/recommendationtype"

export interface RecommendationTypeRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(recommendationtypeId: number):Promise<RecommendationType>, 
    add(recommendationtype: RecommendationType): Promise<RecommendationTypeRow>,
    edit(recommendationtype: RecommendationType): Promise<RecommendationTypeRow>,
    deleteRow(id: number): Promise<void>
}
