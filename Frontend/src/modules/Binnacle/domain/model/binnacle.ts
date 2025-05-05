import { validator } from "@/modules/shared/domain/errors"

export type Binnacle  =  {
    id: number;
	name: string;
    dateRegistered: Date;
    caseID: number;
    userID: number;
    case?: any;
    user?: any;
}

export type BinnacleRow  = Binnacle & {}

export type BinnacleFilter = CriteriaFilter & {
    name: string
}

export const validateBinnacle = (binnacle: Binnacle) => {    
    const dataValidation = [
        { validation: !binnacle.name, error: 'Debe digitar la descripción', tag: 'name' },
    ];

    validator(dataValidation);
}
