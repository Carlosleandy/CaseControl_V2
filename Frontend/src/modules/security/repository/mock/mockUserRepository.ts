import type { UserRepository as UserRepo } from '../../domain/repository/userRepository'
import type { User, UserForm, UserFilter } from '../../domain/entity/user'
import { post, get, patch, DEFAULT_API_PATH } from '@/modules/shared/http_handler'
import type { ModulePrivilegeAdapter } from '../../domain/dto/userPrivileges'
import { getModulePrivileges } from '../adapters/modulePrivileges'
import { getDate } from '@/modules/shared/utility/date'
import { getUserRecords, getUserSingleRecords } from '../adapters/userRecords'

const loadUsers = async (filter: UserFilter | null): Promise<GridRecord> => {    
    const data = [
        {
            "CreatedAt": "0001-01-01T00:00:00Z",
            "UpdatedAt": "2024-05-01T10:18:28.74572-04:00",
            "DeletedAt": null,
            "id": "6a41f6fc-545c-463e-8fb8-e602f2b856c4",
            "name": "José",
            "lastName": "Gabriel",
            "userName": "jgabriel",
            "password": "202cb962ac59075b964b07152d234b70",
            "phone": "",
            "isActive": false,
            "email": "",
            "createdAt": "2024-05-01T10:18:28.74572-04:00",
            "Birthday": "0000-12-31T19:03:58-04:56"
        },
        {
            "CreatedAt": "0001-01-01T00:00:00Z",
            "UpdatedAt": "2024-05-01T10:17:02.688954-04:00",
            "DeletedAt": null,
            "id": "0670ebba-c5a6-4408-bd0b-4b4c440a0cab",
            "name": "Andreina",
            "lastName": "Durán",
            "userName": "ldilone25",
            "password": "202cb962ac59075b964b07152d234b70",
            "phone": "",
            "isActive": false,
            "email": "",
            "createdAt": "2024-05-01T10:17:02.688954-04:00",
            "Birthday": "0000-12-31T19:03:58-04:56"
        }
    ];

    const response = {
        data,
        headers: {
            'x-totalrecords': 2
        }
    }

    return getUserRecords(response);
}

const saveUser = async (user: UserForm): Promise<User> => {
    const requestData = {
        "CreatedAt": "0001-01-01T00:00:00Z",
        "UpdatedAt": "2024-05-01T10:17:02.688954-04:00",
        "DeletedAt": null,
        "id": "0670ebba-c5a6-4408-bd0b-4b4c440a0cab",
        "name": "Andreina",
        "lastName": "Durán",
        "userName": "ldilone25",
        "password": "202cb962ac59075b964b07152d234b70",
        "phone": "",
        "isActive": false,
        "email": "",
        "createdAt": "2024-05-01T10:17:02.688954-04:00",
        "Birthday": "0000-12-31T19:03:58-04:56"
    };
    return getUserSingleRecords(requestData);
}

const getUserPrivileges = async (userId: string): Promise<ModulePrivilegeAdapter[]> => {
    const requestData = [
        {
          "id": 3,
          "name": "ADMIN_MAIN",
          "systemModuleId": 2,
          "systemModuleName": "admin"
        },
        {
          "id": 1,
          "name": "USERS_CREATE",
          "systemModuleId": 1,
          "systemModuleName": "users"
        }
      ];

    return getModulePrivileges(requestData);
}

const getUserById = async (userId: string): Promise<User> => {
    return {
        "CreatedAt": "0001-01-01T00:00:00Z",
        "UpdatedAt": "2024-05-01T10:17:02.688954-04:00",
        "DeletedAt": null,
        "id": "0670ebba-c5a6-4408-bd0b-4b4c440a0cab",
        "name": "Andreina",
        "lastName": "Durán",
        "userName": "ldilone25",
        "password": "202cb962ac59075b964b07152d234b70",
        "phone": "",
        "isActive": false,
        "email": "",
        "createdAt": "2024-05-01T10:17:02.688954-04:00",
        "Birthday": "0000-12-31T19:03:58-04:56"
    };
}

const changeUserStatus = async (userId: string, status: boolean): Promise<boolean> => {
    return true;
}

const userExists = async (userName: string): Promise<boolean> => {
    return false;
}

export default {
    loadUsers,
    saveUser,
    getUserById,
    getUserPrivileges,
    changeUserStatus,
    userExists
} as UserRepo