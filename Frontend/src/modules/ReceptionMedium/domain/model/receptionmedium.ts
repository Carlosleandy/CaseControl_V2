import { validator } from "@/modules/shared/domain/errors"

export type ReceptionMedium  =  {
    id: number;
	name: string;
    cases?: any[];
}

export type ReceptionMediumRow  = ReceptionMedium & {}

export type ReceptionMediumFilter = CriteriaFilter & {
    name: string
}

export const validateReceptionMedium = (receptionmedium: ReceptionMedium) => {    
    const dataValidation = [
        { validation: !receptionmedium.name, error: 'Debe digitar la descripción', tag: 'name' },
    ];

    validator(dataValidation);
}
