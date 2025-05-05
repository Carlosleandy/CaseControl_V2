import { post, DEFAULT_API_PATH } from "@/modules/shared/http_handler"
import type { AuthRepository } from "../../domain/repository/authRepository"
import Menu from '../static_data/menu.json'

const authenticate = async (userName: string, password: string): Promise<any> => {

    const data = {
        "user": {
            "ID": 0,
            "CreatedAt": "0001-01-01T00:00:00Z",
            "UpdatedAt": "2024-04-02T18:14:09.244902-04:00",
            "DeletedAt": null,
            "id": "6ed40f80-b8b3-476b-a1ad-b896020a5bee",
            "name": "Luis",
            "lastName": "Dilone",
            "userName": "ldilone",
            "password": "202cb962ac59075b964b07152d234b70",
            "phone": "aalda",
            "isActive": true,
            "email": "ald;asld",
            "createdAt": "2024-04-02T18:14:09.244902-04:00",
            "Birthday": "0000-12-31T19:03:58-04:56"
        },
        "privileges": [
            "ADMIN_MAIN",
            "USERS_CREATE"
        ],
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJLaW5nTG90dGVyeUlkZW50aXR5VXNlciIsImV4cCI6MTcxNjIxNDg3MCwiaXNzIjoiS2luZ0xvdHRlcnlJZGVudGl0eVVzZXIiLCJ1aWQiOiI2ZWQ0MGY4MC1iOGIzLTQ3NmItYTFhZC1iODk2MDIwYTViZWUifQ.UbzahoNcneQjb549gb3bOtkHsZuUNVfLEUHARMbwi60"
    }

    const loginInfo = {
        ...data,
        menu: Menu,
    }

    return loginInfo;
}

const refreshToken = async (): Promise<string> => {
    return "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJLaW5nTG90dGVyeUlkZW50aXR5VXNlciIsImV4cCI6MTcxNjIxNDg3MCwiaXNzIjoiS2luZ0xvdHRlcnlJZGVudGl0eVVzZXIiLCJ1aWQiOiI2ZWQ0MGY4MC1iOGIzLTQ3NmItYTFhZC1iODk2MDIwYTViZWUifQ.UbzahoNcneQjb549gb3bOtkHsZuUNVfLEUHARMbwi60";
}

export default {
    authenticate,
    refreshToken
} as AuthRepository
