import { validator } from "@/modules/shared/domain/errors"
// Using browser's native File interface

export type CaseInvestigation  =  {
    id: number;
    subject?: string;
	transmitter?: string;
    recipient?: string;
    communicationNumber?: string;
    dateOfCommunication?: Date;
    dateOfReceipt?: Date;
    communicationReference?: string;
    branch?: string;
    amountDetected?: number;
    amountInvestigated?: number;
    amountRecovered?: number;
    amountLost?: number;
    affectedAccount?: string;    
    userNameRegistered?: string;
    receptionMediumId?: number;
    caseTypeId?: number;
    caseStatusId?: number;
    userId?: number;
    recommendations?: any[];
    binnacles?: any[];
    recoveryHistories?: any[];
    caseAssignments?: any[];
    caseStatusChanges?: any[];
    evidences?: any[];
    linkeds?: any[];
    interviews?: any[];
    faultLinkeds?: any[];
}

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

export type CaseStatusChange  =  {
    id: number;
    userNameRegistered: string;
    dateRegistered: Date;
    caseID: number;
    caseStatusID: number;
    case?: any;
    caseStatus?: any;
}

export type RecoveryHistory  =  {
    id: number;
    amountRecovery: number;
    observations: string;
    dateRecovery: Date;
    dateRegistered: Date;
    caseID: number;
    userID: number;
    case?: any;
    user?: any;
}

export type Evidence  =  {
    id: number;
    name: string;
    description: string;
    hash: string;
    path: string;
    extension: string;
    sizeKB: number;
    dateRegistered: Date;
    caseID: number;
    evidenceClassificationID: number;
    case?: any;
    evidenceClassification?: any;
}

export type FileUploadResponse = {
    message: string;
    file: {
      originalname: string;
      filename: string;
      path: string;
    };
  }

  export type RankingCasesUserDTO = {
    casesTotal: number,
    casesUserDTOs?: any[],
  }

export type CasesUserDTO = {
    casesCount: number,
    user?: any,
  }

export type CasesByStatusResumeDTO  =  {
    id: number;
	status: string;                                                                 
    count: number;
}

export type CasesByUserNameSummaryDTO  =  {
    user: any;    
    count: number;
}

export type CasesRecoverySummaryDTO  =  {
    case?: any;
    amountInvestigated: number;
    amountRecovery: number;
    amountDifference: number;
    percentRecovery: number;
}

export type CasesByLinkedCodeDTO  =  {
    linked?: any;
    cases?: any[];
}

export type CaseInvestigationRow  = CaseInvestigation & {}

export type CaseInvestigationFilter = CriteriaFilter & {
    name: string
}

export const validateCaseInvestigation = (caseinvestigation: CaseInvestigation) => {    

    console.log('Validando caso:', {
        caseTypeId: caseinvestigation.caseTypeId,
        typeOf: typeof caseinvestigation.caseTypeId,
        isNull: caseinvestigation.caseTypeId === null,
        isUndefined: caseinvestigation.caseTypeId === undefined,
        isZero: caseinvestigation.caseTypeId === 0
    });

    const dataValidation = [
        { validation: !caseinvestigation.subject, error: 'Debe especificar el asunto de la comunicación', tag: 'subject' },
        { validation: !caseinvestigation.communicationNumber, error: 'Debe especificar el número de la comunicación de la comunicación', tag: 'communicationNumber' },
        { validation: !caseinvestigation.transmitter, error: 'Debe especificar el emisor de la comunicación de la comunicación', tag: 'transmitter' },
        { validation: !caseinvestigation.amountDetected, error: 'Debe digitar el monto investigado', tag: 'amountDetected' },
        { validation: !caseinvestigation.dateOfCommunication, error: 'Debe elegir la fecha de la comunicación', tag: 'dateOfCommunication' },
        { validation: !caseinvestigation.dateOfReceipt, error: 'Debe elegir la fecha de recepción de la comunicación', tag: 'dateOfReceipt' },
        { validation: !caseinvestigation.amountLost, error: 'Debe digital el monto de perdida', tag: 'amountLost' },
        { validation: !caseinvestigation.branch, error: 'Debe elegir la oficina del caso', tag: 'branch' },
        { validation: !caseinvestigation.affectedAccount, error: 'Debe especificar la cuenta afectada', tag: 'affectedAccount' },
        { validation: caseinvestigation.caseTypeId === null || caseinvestigation.caseTypeId === undefined || caseinvestigation.caseTypeId <= 0, error: 'Debe elegir el tipo de caso', tag: 'caseTypeId' },
        { validation: caseinvestigation.receptionMediumId === null || caseinvestigation.receptionMediumId === undefined || caseinvestigation.receptionMediumId <= 0, error: 'Debe elegir el medio de recepción del caso', tag: 'receptionMediumId' },
        { validation: caseinvestigation.caseStatusId === null || caseinvestigation.caseStatusId === undefined || caseinvestigation.caseStatusId <= 0, error: 'Debe elegir el estado del recepción del caso', tag: 'caseStatusId' },
        { validation: !caseinvestigation.communicationReference, error: 'Debe especificar la referenciade la comunicación del caso', tag: 'communicationReference' }
    ];

    validator(dataValidation);
}
