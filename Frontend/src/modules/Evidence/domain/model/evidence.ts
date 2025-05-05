import { validator } from "@/modules/shared/domain/errors"

export type Evidence  =  {
    id: number;
	name: string;
}

export type EvidenceRow  = Evidence & {}

export type EvidenceFilter = CriteriaFilter & {
    name: string
}

export const validateEvidence = (evidence: Evidence) => {    
    const dataValidation = [
        { validation: !evidence.name, error: 'ENTITY.must_specify_name', tag: 'name' },
    ];

    validator(dataValidation);
}
