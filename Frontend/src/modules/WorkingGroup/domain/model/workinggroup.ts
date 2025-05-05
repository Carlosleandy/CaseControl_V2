import { validator } from "@/modules/shared/domain/errors"

export type WorkingGroup  =  {
    id: number;
	name: string;
    users?: any[];
}

export type WorkingGroupRow  = WorkingGroup & {}

export type WorkingGroupFilter = CriteriaFilter & {
    name: string
}

export const validateWorkingGroup = (workinggroup: WorkingGroup) => {    
    const dataValidation = [
        { validation: !workinggroup.name, error: 'Debe digitar la descripción', tag: 'name' },
    ];

    validator(dataValidation);
}
