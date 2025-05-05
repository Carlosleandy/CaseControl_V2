import { validator } from "@/modules/shared/domain/errors"

export type Recommendation  =  {
    id: number;
    title: string;
    unitToWhichItIsAddressed: string;
    contact: string;
    response: string;
    dateRegistered: Date;
    recommendationStatusID: number;
    caseID: number;
    userID: number;
    recommendationTypeID: number;
}

export type RecommendationRow  = Recommendation & {}

export type RecommendationFilter = CriteriaFilter & {
    name: string
}

export type CasesByRecommendationTypeSummaryDTO  =  {
    id: number;
	recommendationType: string;    
    count: number;
}

export const validateRecommendation = (recommendation: Recommendation) => {    
    const dataValidation = [
        { validation: !recommendation.title, error: 'Debe especificar el Título', tag: 'title' },
    ];

    validator(dataValidation);
}
