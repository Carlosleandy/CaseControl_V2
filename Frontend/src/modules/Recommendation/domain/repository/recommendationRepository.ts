import type { Recommendation, RecommendationRow } from "../model/recommendation"

export interface RecommendationRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(recommendationId: number):Promise<Recommendation>, 
    add(recommendation: Recommendation): Promise<RecommendationRow>,
    edit(recommendation: Recommendation): Promise<RecommendationRow>,
    deleteRow(id: number): Promise<void>
}
