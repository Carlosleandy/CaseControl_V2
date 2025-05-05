import { validator } from "@/modules/shared/domain/errors"

export type User  =  {
    id: number;
	userName: string;
    isActive: boolean;
    isAdmin: boolean;
    workingGroupID: number;
    userLevelID: number;
    binnacles?: any[];
    recommendations?: any[];
    cases?: any[];
    recoveryHistories?: any[];
    caseAssignments?: any[];
    employee?: any;

}

export type UserRow  = User & {}

export type UserFilter = CriteriaFilter & {
    userName: string
}

export const validateUser = (user: User) => {    
    const dataValidation = [
        { validation: !user.userName, error: 'Debe digitar el UserName', tag: 'userName' },
    ];

    validator(dataValidation);
}
