import { validator } from "@/modules/shared/domain/errors"

export type EvidenceClassification  =  {
    id: number;
	name: string;
}

export type EvidenceClassificationRow  = EvidenceClassification & {}

export type EvidenceClassificationFilter = CriteriaFilter & {
    name: string
}

export const validateEvidenceClassification = (evidenceclassification: EvidenceClassification) => {    
    const dataValidation = [
        { validation: !evidenceclassification.name, error: 'ENTITY.must_specify_name', tag: 'name' },
    ];

    validator(dataValidation);
}
