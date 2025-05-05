import type { UserRepository as UserRepo } from '../domain/repository/userRepository'
import type { User, UserForm, UserFilter } from '../domain/entity/user'
import { post, get, patch, DEFAULT_API_PATH } from '@/modules/shared/http_handler'
import type { ModulePrivilegeAdapter } from '../domain/dto/userPrivileges'
import { getModulePrivileges } from './adapters/modulePrivileges'
import { getDate } from '@/modules/shared/utility/date'
import { getUserRecords, getUserSingleRecords } from './adapters/userRecords'

const loadUsers = async (filter: UserFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.creationDateFrom) {
        filter.filters.creationDateFrom = getDate(filter.filters?.creationDateFrom);
    }

    if(filter && filter.filters?.creationDateTo) {
        filter.filters.creationDateTo = getDate(filter.filters?.creationDateTo);
    }

    const response = await post(DEFAULT_API_PATH + '/auth/users/records', { ...filter });
    return getUserRecords(response);
}

const saveUser = async (user: UserForm): Promise<User> => {
    const requestData = await post(DEFAULT_API_PATH + '/auth/register', user);
    return getUserSingleRecords(requestData);
}

const getUserPrivileges = async (userId: string): Promise<ModulePrivilegeAdapter[]> => {
    userId = userId || '';
    const requestData = await get(DEFAULT_API_PATH + '/auth/privileges' + userId);
    return getModulePrivileges(requestData.data);
}

const getUserById = async (userId: string): Promise<User> => {
    const requestData = await get(DEFAULT_API_PATH + '/auth/users/' + userId);
    return requestData.data;
}

const changeUserStatus = async (userId: string, status: boolean): Promise<boolean> => {
    const requestData = await patch(DEFAULT_API_PATH + `/auth/users/${userId}/${status}`, {} );
    return requestData.status === 200;
}

const userExists = async (userName: string): Promise<boolean> => {
    const requestData = await get(DEFAULT_API_PATH + `/auth/users/${userName}/exists`, {} );
    return requestData.data.exists;
}

export default {
    loadUsers,
    saveUser,
    getUserById,
    getUserPrivileges,
    changeUserStatus,
    userExists
} as UserRepo