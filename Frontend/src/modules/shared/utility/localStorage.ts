import { encrypt, decrypt } from "./encryptor";

export const set = (sessionKey: string, data: any): void => {
    localStorage.setItem(sessionKey, data);
}

export const get = (sessionKey: string, jsonParsed: boolean = false): any => {
    try {
        const data = localStorage.getItem(sessionKey);

        if (jsonParsed && data) {
            return JSON.parse(data);
        }

        return data;
    }
    catch {
        return null;
    }
}

export const setEncrypt = (key: string, data: any, parseJson = false): void => {    
    if (parseJson) {
        data = JSON.stringify(data);
    }

    const encryptedData = encrypt(data);
    localStorage.setItem(key, encryptedData);
}

export const getEncrypt = (key: string, jsonParsed: boolean = false): any => {
    try {
        const encryptedSession = localStorage.getItem(key) || '';
        const decryptedSession = decrypt(encryptedSession) || {};

        if (jsonParsed) {
            return JSON.parse(decryptedSession);
        }

        return decryptedSession[key];
    }
    catch {
        return null;
    }
}

export const unset = (key: string) => {
    localStorage.removeItem(key);
}

export const clearStore = () => {
    localStorage.clear();
}
