import type { ReceptionMediumRepository } from '../domain/repository/receptionmediumRepository'
import type { ReceptionMedium, ReceptionMediumRow, ReceptionMediumFilter } from '../domain/model/receptionmedium'
import { get, getOnly, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from 'vue'

const loadRecords = async (filter: ReceptionMediumFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/ReceptionMedium?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (receptionmedium: ReceptionMedium): Promise<ReceptionMediumRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/ReceptionMedium', receptionmedium);
    return requestData;
}

const edit = async (receptionmedium: ReceptionMedium): Promise<ReceptionMediumRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/ReceptionMedium', receptionmedium);
    return requestData;
}

const getById = async (receptionmediumId: number): Promise<ReceptionMedium> => {
    const requestData = await get(DEFAULT_API_PATH + '/ReceptionMedium/' + receptionmediumId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/ReceptionMedium/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataReceptionMedium = () => {
    const receptionMediumList = ref([]);
    
    async function getAllReceptionMediums (){
        try {
            receptionMediumList.value = await getOnly(DEFAULT_API_PATH + '/ReceptionMedium');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        receptionMediumList,
        getAllReceptionMediums
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as ReceptionMediumRepository