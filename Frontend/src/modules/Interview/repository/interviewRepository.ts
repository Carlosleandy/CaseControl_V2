import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { DELETING_RECORD_ERROR, DELETING_RECORD_SUCCESS, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS } from '@/modules/shared/domain/commonMessages'
import type { InterviewRepository } from '../domain/repository/interviewRepository'
import type { Interview, InterviewRow, InterviewFilter } from '../domain/model/interview'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from 'vue'

const loadRecords = async (filter: InterviewFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/Interview?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (interview: Interview): Promise<InterviewRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/Interview', interview);
    return requestData;
}

const edit = async (interview: Interview): Promise<InterviewRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/Interview', interview);
    return requestData;
}

const getById = async (interviewId: number): Promise<Interview> => {
    const requestData = await get(DEFAULT_API_PATH + '/Interview/' + interviewId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/Interview/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataInterview = () => {
    
    const intervieweeTypeList = ref([]);

    async function getIntervieweeTypes (){
        try {
            intervieweeTypeList.value = await getOnly(DEFAULT_API_PATH + '/Interview/getallintervieweetype');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function addInterview (interview: Interview): Promise<Interview> {
        try {
            const requestData = await post(DEFAULT_API_PATH + '/Interview', interview);
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            return requestData;  
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            throw new Error('Se ha presentado un eror: ' + error.message);
        }   
    }

    const deleteInterview = async (id:number): Promise<boolean>=>{
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
        intervieweeTypeList,
        getIntervieweeTypes,
        addInterview,
        deleteInterview
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as InterviewRepository