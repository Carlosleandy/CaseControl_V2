import { validator } from "@/modules/shared/domain/errors"

export type Linked  =  {
    id: number;
	name: string;
    lastName: string;
    identification: string;
    phone: string;
    cellPhone: string;
    birthdate: Date;
    address: string;
    code: string;
    linkTypeID: number;
    caseID: number;
    linkType?: any;
    case?: any;
}

export type LinkedRow  = Linked & {}

export type LinkedFilter = CriteriaFilter & {
    name: string
}

export const validateLinked = (linked: Linked) => {    
    const dataValidation = [
        { validation: !linked.name, error: 'ENTITY.must_specify_name', tag: 'name' },
    ];

    validator(dataValidation);
}
