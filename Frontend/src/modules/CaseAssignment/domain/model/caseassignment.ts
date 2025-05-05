import { validator } from "@/modules/shared/domain/errors"

export type CaseAssignment  =  {
    id: number;
    userNameRegistered: string;
    observations: string;
    dateRegistered: Date;
    caseID: number;
    userID: number;
    case?: any;
    user?: any;
}

export type CaseAssignmentRow  = CaseAssignment & {}

export type CaseAssignmentFilter = CriteriaFilter & {
    name: string
}

export const validateCaseAssignment = (caseassignment: CaseAssignment) => {    
    const dataValidation = [
        { validation: !caseassignment.caseID, error: 'Debe especificar el Caso', tag: 'caseID' },
    ];

    validator(dataValidation);
}
