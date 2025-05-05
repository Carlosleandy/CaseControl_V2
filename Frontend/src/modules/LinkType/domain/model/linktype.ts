import { validator } from "@/modules/shared/domain/errors"

export type LinkType  =  {
    id: number;
	name: string;
}

export type LinkTypeRow  = LinkType & {}

export type LinkTypeFilter = CriteriaFilter & {
    name: string
}

export const validateLinkType = (linktype: LinkType) => {    
    const dataValidation = [
        { validation: !linktype.name, error: 'ENTITY.must_specify_name', tag: 'name' },
    ];

    validator(dataValidation);
}
