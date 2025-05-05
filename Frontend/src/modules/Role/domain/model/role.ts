import { validator } from "@/modules/shared/domain/errors"

export type Role  =  {
    id: number;
	name: string;
    access_Roles?: any[];
    userLevels?: any[];
}

export type RoleRow  = Role & {}

export type RoleFilter = CriteriaFilter & {
    name: string
}

export const validateRole = (role: Role) => {    
    const dataValidation = [
        { validation: !role.name, error: 'ENTITY.must_specify_name', tag: 'name' },
    ];

    validator(dataValidation);
}
