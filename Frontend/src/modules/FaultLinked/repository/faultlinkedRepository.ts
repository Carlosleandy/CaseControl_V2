import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { FaultLinkedRepository } from '../domain/repository/faultlinkedRepository'
import type { FaultLinked, FaultLinkedRow, FaultLinkedFilter } from '../domain/model/faultlinked'
import type { Fault } from '../../Fault/domain/model/fault'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const loadRecords = async (filter: FaultLinkedFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/FaultLinked?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (faultlinked: FaultLinked): Promise<FaultLinkedRow> => { debugger;
    const requestData = await post(DEFAULT_API_PATH + '/FaultLinked', faultlinked);
    return requestData;
}

const edit = async (faultlinked: FaultLinked): Promise<FaultLinkedRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/FaultLinked', faultlinked);
    return requestData;
}

const getById = async (faultlinkedId: number): Promise<FaultLinked> => {
    const requestData = await get(DEFAULT_API_PATH + '/FaultLinked/' + faultlinkedId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/FaultLinked/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataFaultLinked = () => {
    const faultsbycodelinked = ref([]);

    async function getFaultsByCodeLinked (code: string) : Promise<FaultLinked[]> {
        try {
            faultsbycodelinked.value = await getOnly(DEFAULT_API_PATH + '/FaultLinked/GetFaultLinkedsByLinkedCode/' + code);    
            return faultsbycodelinked;        
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    const saveFaultLinked = async (faultLinked: FaultLinked): Promise<boolean> => { debugger;
        try {
            const requestData = await post(DEFAULT_API_PATH + '/FaultLinked', faultLinked);
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            return requestData;  
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            throw new Error('Se ha presentado un eror: ' + error.message);
        } 
    };

    const deleteFaultLinked = async (id:number): Promise<boolean>=>{
        const deleted=await deleteRow(id);
        return true;
    }

    async function generatePDFFaultsByLinkedCodeAsync (code: string) {
        try {
            const response = await fetch(DEFAULT_API_PATH + '/FaultLinked/generatePDFFaultsByLinkedCode/' + code); 

            if(!response.ok){
                throw new Error('Error al descargar el reporte');
            }

            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download='Faltas del Vinculado ' + code + '_' + today.getFullYear()+(today.getMonth()+1)+today.getDate();
            element.click();
            window.URL.revokeObjectURL(url);  
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    return {
        faultsbycodelinked,
        getFaultsByCodeLinked,
        saveFaultLinked,
        deleteFaultLinked,
        generatePDFFaultsByLinkedCodeAsync
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as FaultLinkedRepository