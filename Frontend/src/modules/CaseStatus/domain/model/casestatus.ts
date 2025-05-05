import { validator } from "@/modules/shared/domain/errors"

export type CaseStatus  =  {
    id: number;
	name: string;
    percent: number;
    cases?: any[];
    caseStatusChanges?: any[];
}

export type CaseStatusRow  = CaseStatus & {}

export type CaseStatusFilter = CriteriaFilter & {
    name: string
}

export const validateCaseStatus = (casestatus: CaseStatus) => {    
    const dataValidation = [
        { validation: !casestatus.name, error: 'Debe digitar la descripción', tag: 'name' },
        { validation: !casestatus.percent, error: 'Debe especificar el porciento', tag: 'percent' },
    ];

    validator(dataValidation);
}
