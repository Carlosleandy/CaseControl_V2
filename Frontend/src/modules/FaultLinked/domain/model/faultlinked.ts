import { validator } from "@/modules/shared/domain/errors"

export type FaultLinked  =  {
    id: number;
	caseID: number;
    linkedID: number;
    faultID: number;
    dateRegistered: Date;
    case?: any;
    linked?: any;
    fault?: any;
}

export type FaultLinkedRow  = FaultLinked & {}

export type FaultLinkedFilter = CriteriaFilter & {
    name: string
}

export const validateFaultLinked = (faultlinked: FaultLinked) => {    
    const dataValidation = [
        { validation: !faultlinked.linkedID, error: 'ENTITY.must_specify_name', tag: 'linkedID' },
        { validation: !faultlinked.faultID, error: 'ENTITY.must_specify_name', tag: 'faultID' },
    ];

    validator(dataValidation);
}
