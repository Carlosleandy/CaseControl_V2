type validation = {
    validation: boolean;
    error: string;
}

export class CustomError extends Error {
    constructor(message: string) {
        super(message);

        // Set the prototype explicitly
        Object.setPrototypeOf(this, CustomError.prototype);

        // Set the name of the custom error
        this.name = this.constructor.name;
    }
}

export const getErrorInformation = (e: Error, defaultMessage = '') => {
    let errorType: 'error' | 'warn' = 'error';
    let errorMessage = defaultMessage;

    if(e instanceof CustomError) {
        errorType = 'warn';
        errorMessage = (e as Error).message;
    }

    return {
        type: errorType,
        message: errorMessage
    }
}

export const validator = (elements: validation[]) => {
    for(const element of elements) {
        if(element.validation) {
            throw new CustomError(element.error)
        }
    }
}