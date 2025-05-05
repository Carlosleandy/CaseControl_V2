import {CaseInvestigation } from '../domain/model/caseinvestigation'


//Validar la entrada de los datos
export function validateCaseInvestigation(Case:CaseInvestigation) {

    const inputToValidate = [
        {
            element: !Case.communicationNumber,
            error: 'El número de comunicación no debe estar vacio.'
        },

        {
            element: !Case.subject,
            error: 'Debe especificar el asunto'
        },

        {
            element: !Case.transmitter,
            error: 'Es necesario completar el emisor'
        },
        {
            element: !Case.amountDetected,
            error: 'Debe especificar el monto detectado'
        },
        {
            element: !Case.dateOfCommunication,
            error: 'Debe especificar la fecha de la comunicación'
        },
        {
            element: !Case.dateOfReceipt,
            error: 'Debe especificar la fecha de recepcion'
        }
    ];

    for(const element of inputToValidate){
        if(element.element){
            throw new Error(element.error);
        }
    }
       
}

