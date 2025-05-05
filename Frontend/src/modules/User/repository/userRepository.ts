import type { UserRepository } from '../domain/repository/userRepository'
import type { User, UserRow, UserFilter } from '../domain/model/user'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from "vue";

const loadRecords = async (filter: UserFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/User?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (user: User): Promise<UserRow> => {
    debugger;
    const requestData = await post(DEFAULT_API_PATH + '/User', user);
    return requestData;
}

const edit = async (user: User): Promise<UserRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/User', user);
    return requestData;
}

const getById = async (userId: number): Promise<User> => {
    const requestData = await get(DEFAULT_API_PATH + '/User/' + userId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/User/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataUsers = () => {
    const usersOnly = ref([]);
    
    async function getAllUserOnly (){
        try {
            usersOnly.value = await getOnly(DEFAULT_API_PATH + '/User/GetAllUserOnly');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        usersOnly,
        getAllUserOnly
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as UserRepository