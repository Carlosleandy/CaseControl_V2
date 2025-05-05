import { validator } from "@/modules/shared/domain/errors"

export type CaseType  =  {
    id: number;
	name: string;
    cases?: any[];
}

export type CaseTypeRow  = CaseType & {}

export type CaseTypeFilter = CriteriaFilter & {
    name: string
}

export const validateCaseType = (casetype: CaseType) => {    
    const dataValidation = [
        { validation: !casetype.name, error: 'Debe ingresar la descripción', tag: 'name' },
    ];

    validator(dataValidation);
}
