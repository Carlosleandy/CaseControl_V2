import type { CaseTypeRepository } from '../domain/repository/casetypeRepository'
import type { CaseType, CaseTypeRow, CaseTypeFilter } from '../domain/model/casetype'
import { get, getOnly, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from 'vue'

const loadRecords = async (filter: CaseTypeFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/CaseType?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (caseManager: CaseType): Promise<CaseTypeRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/CaseType', caseManager);
    return requestData;
}

const edit = async (caseManager: CaseType): Promise<CaseTypeRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/CaseType', caseManager);
    return requestData;
}

const getById = async (caseManagerId: number): Promise<CaseType> => {
    const requestData = await get(DEFAULT_API_PATH + '/CaseType/' + caseManagerId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/CaseType/' + id);
    } catch (error: any) {
      throw new Error('Error al eliminar el tipo de caso: ' + error.message);
    }
}

export const useDataCaseType = () => {
    const caseTypeList = ref([]);
    
    async function getAllCaseTypes (){
        try {
            caseTypeList.value = await getOnly(DEFAULT_API_PATH + '/CaseType');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        caseTypeList,
        getAllCaseTypes
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as CaseTypeRepository