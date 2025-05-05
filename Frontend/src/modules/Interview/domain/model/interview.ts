import { validator } from "@/modules/shared/domain/errors"

export type Interview  =  {
    id: number;
	description: string;
    dateInterview: Date;
    dateRegistered: Date;
    intervieweeTypeID: number;
    caseID: number;
    linkedID: number;
    intervieweeType?: any;
    case?: any;
    linked?: any;
}

export type InterviewRow  = Interview & {}

export type InterviewFilter = CriteriaFilter & {
    name: string
}

export const validateInterview = (interview: Interview) => {    
    const dataValidation = [
        { validation: !interview.description, error: 'ENTITY.must_specify_name', tag: 'name' },
    ];

    validator(dataValidation);
}
