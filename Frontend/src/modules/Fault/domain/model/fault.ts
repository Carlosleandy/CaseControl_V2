import { validator } from "@/modules/shared/domain/errors"

export type Fault  =  {
    id: number;
	name: string;
    faultTypeID: number;
}

export type FaultRow  = Fault & {}

export type FaultFilter = CriteriaFilter & {
    name: string
}

export const validateFault = (fault: Fault) => {    
    const dataValidation = [
        { validation: !fault.name, error: 'ENTITY.must_specify_name', tag: 'name' },
    ];

    validator(dataValidation);
}
