import { validator } from "@/modules/shared/domain/errors"

export type UserLevel  =  {
    id: number;
	name: string;
    roleID: number;
    role?: any;
    users?: any[];
}

export type UserLevelRow  = UserLevel & {}

export type UserLevelFilter = CriteriaFilter & {
    name: string
}

export const validateUserLevel = (userlevel: UserLevel) => {    
    const dataValidation = [
        { validation: !userlevel.name, error: 'Debe digitar la descripción', tag: 'name' },
    ];

    validator(dataValidation);
}
