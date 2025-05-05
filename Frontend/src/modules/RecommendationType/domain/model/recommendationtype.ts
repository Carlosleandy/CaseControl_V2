import { validator } from "@/modules/shared/domain/errors"

export type RecommendationType  =  {
    id: number;
	name: string;
    recommendations?: any[];
}

export type RecommendationTypeRow  = RecommendationType & {}

export type RecommendationTypeFilter = CriteriaFilter & {
    name: string
}

export const validateRecommendationType = (recommendationtype: RecommendationType) => {    
    const dataValidation = [
        { validation: !recommendationtype.name, error: 'Debe digitar la descripción', tag: 'name' },
    ];

    validator(dataValidation);
}
