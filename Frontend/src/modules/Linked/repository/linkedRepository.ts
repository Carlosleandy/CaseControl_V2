import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { DELETING_RECORD_ERROR, DELETING_RECORD_SUCCESS, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS } from '@/modules/shared/domain/commonMessages'
import type { LinkedRepository } from '../domain/repository/linkedRepository'
import type { Linked, LinkedRow, LinkedFilter } from '../domain/model/linked'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from 'vue'

const loadRecords = async (filter: LinkedFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/Linkeds?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (linked: Linked): Promise<LinkedRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/Linkeds', linked);
    return requestData;
}

const edit = async (linked: Linked): Promise<LinkedRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/Linkeds', linked);
    return requestData;
}

const getById = async (linkedId: number): Promise<Linked> => {
    const requestData = await get(DEFAULT_API_PATH + '/Linkeds/' + linkedId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/Linkeds/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataLinkedType = () => {
    const linkedList = ref([]);
    const linkedTypeList = ref([]);
    
    async function getAllLinkedType (){
        try {
            linkedTypeList.value = await getOnly(DEFAULT_API_PATH + '/Linkeds/linktypes');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function getAllLinkeds (){
        try {
            linkedList.value = await getOnly(DEFAULT_API_PATH + '/Linkeds');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function addLinked (linked: Linked): Promise<Linked> {
        try {
            const requestData = await post(DEFAULT_API_PATH + '/Linkeds', linked);
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            return requestData;  
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            throw new Error('Se ha presentado un eror: ' + error.message);
        }   
    }

    const deleteLinked = async (id:number): Promise<boolean>=>{
        try {
            const deleted=await deleteRow(id);
            systemMessage({ "type": 'info', "description": DELETING_RECORD_SUCCESS });
            return true; 
        } catch(e) {
            const error = getErrorInformation(e as Error, DELETING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            throw new Error('Se ha presentado un eror: ' + error.message);
        }         
    }

    return {
        linkedTypeList,
        linkedList,
        getAllLinkedType,
        getAllLinkeds,
        addLinked,
        deleteLinked
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as LinkedRepository