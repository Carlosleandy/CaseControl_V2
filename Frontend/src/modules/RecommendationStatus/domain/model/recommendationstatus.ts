import { validator } from "@/modules/shared/domain/errors"

export type RecommendationStatus  =  {
    id: number;
	name: string;
    recommendations?: any[];
}

export type RecommendationStatusRow  = RecommendationStatus & {}

export type RecommendationStatusFilter = CriteriaFilter & {
    name: string
}

export const validateRecommendationStatus = (recommendationstatus: RecommendationStatus) => {    
    const dataValidation = [
        { validation: !recommendationstatus.name, error: 'Debe digitar la descripción', tag: 'name' },
    ];

    validator(dataValidation);
}
