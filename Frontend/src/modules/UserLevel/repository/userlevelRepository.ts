import type { UserLevelRepository } from '../domain/repository/userlevelRepository'
import type { UserLevel, UserLevelRow, UserLevelFilter } from '../domain/model/userlevel'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from "vue";

const loadRecords = async (filter: UserLevelFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/UserLevel?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (userlevel: UserLevel): Promise<UserLevelRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/UserLevel', userlevel);
    return requestData;
}

const edit = async (userlevel: UserLevel): Promise<UserLevelRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/UserLevel', userlevel);
    return requestData;
}

const getById = async (userlevelId: number): Promise<UserLevel> => {
    const requestData = await get(DEFAULT_API_PATH + '/UserLevel/' + userlevelId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/UserLevel/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataUserLevel = () => {
    const userLevelList = ref([]);
    
    async function getAllUserLevels (){
        try {
            userLevelList.value = await getOnly(DEFAULT_API_PATH + '/UserLevel');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        userLevelList,
        getAllUserLevels
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as UserLevelRepository