import type { CaseStatusRepository } from '../domain/repository/casestatusRepository'
import type { CaseStatus, CaseStatusRow, CaseStatusFilter, PaginationParams } from '../domain/model/casestatus'
import { get, getOnly, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from 'vue';

const loadRecords = async (filter: CaseStatusFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }
    
    const response = await get(DEFAULT_API_PATH + '/CaseStatus?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (casestatus: CaseStatus): Promise<CaseStatusRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/CaseStatus', casestatus);
    return requestData;
}

const edit = async (casestatus: CaseStatus): Promise<CaseStatusRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/CaseStatus', casestatus);
    return requestData;
}

const getById = async (casestatusId: number): Promise<CaseStatus> => {
    const requestData = await get(DEFAULT_API_PATH + '/CaseStatus/' + casestatusId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/CaseStatus/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataCaseStatus = () => {
    const caseStatusList = ref([]);
    
    async function getAllCaseStatus (){
        try {
            caseStatusList.value = await getOnly(DEFAULT_API_PATH + '/CaseStatus');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        caseStatusList,
        getAllCaseStatus
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as CaseStatusRepository