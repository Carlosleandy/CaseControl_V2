import { post, DEFAULT_API_PATH } from "@/modules/shared/http_handler"
import type { AuthRepository } from "../domain/repository/authRepository"
import Menu from './static_data/menu.json'

// const authenticate = async (userName: string, password: string): Promise<any> => {
//     //const data = await post(DEFAULT_API_PATH + '/auth/authenticate', {
//     const data = await post(DEFAULT_API_PATH + '/account/authenticate', {
//         userName, password
//     });

//     const loginInfo = {
//         ...data.data,
//         menu: Menu,
//     }
//     return loginInfo;
// }


const authenticate = async (key: string, userName: string): Promise<any> => {
    //const data = await post(DEFAULT_API_PATH + '/auth/authenticate', {
    const data = await post(DEFAULT_API_PATH + '/account/authenticate', {
        key, userName
    });

    const loginInfo = {
        ...data.data,
        menu: Menu,
    }
    return loginInfo;
}

const refreshToken = async (): Promise<string> => {
    const response = await post(DEFAULT_API_PATH + '/Account/refresh', { });
    return response.data.token;
}

export default {
    authenticate,
    refreshToken
} as AuthRepository
