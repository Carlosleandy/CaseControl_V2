import type { FaultRepository } from '../domain/repository/faultRepository'
import type { Fault, FaultRow, FaultFilter } from '../domain/model/fault'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from 'vue'

const loadRecords = async (filter: FaultFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/Fault?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (fault: Fault): Promise<FaultRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/Fault', fault);
    return requestData;
}

const edit = async (fault: Fault): Promise<FaultRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/Fault', fault);
    return requestData;
}

const getById = async (faultId: number): Promise<Fault> => {
    const requestData = await get(DEFAULT_API_PATH + '/Fault/' + faultId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/Fault/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataFaultType = () => {
    const faultTypeList = ref([]);
    const faultsList = ref([]);
    
    async function getAllFaultTypes (){
        try {
            faultTypeList.value = await getOnly(DEFAULT_API_PATH + '/Fault/getallfaulttypes');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    async function getAllFaults (){
        try {
            faultsList.value = await getOnly(DEFAULT_API_PATH + '/Fault');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        faultTypeList,
        faultsList,
        getAllFaultTypes,
        getAllFaults
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as FaultRepository