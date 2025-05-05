const key: string = import.meta.env.VITE_SYSTEM_KEY;

const xor = (txt: string, pass: string) => {
    const ord: any = []
    let buf = ""
    let z = 0;

    for (z = 1; z <= 255; z++) {
        ord[String.fromCharCode(z)] = z
    }
 
    for (let j = z = 0; z < txt.length; z++) {
        buf += String.fromCharCode(ord[txt.substr(z, 1)] ^ ord[pass.substr(j, 1)])
        j = (j < pass.length) ? j + 1 : 0
    }
 
    return buf
}

export const encrypt = (value: any) => {
    return xor(JSON.stringify(value), key);
}

export const decrypt = (value: any) => {
    return JSON.parse(xor(value, key));
}
