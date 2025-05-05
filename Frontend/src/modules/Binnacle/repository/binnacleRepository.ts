import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { BinnacleRepository } from '../domain/repository/binnacleRepository'
import type { Binnacle, BinnacleRow, BinnacleFilter } from '../domain/model/binnacle'
import { get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'

const loadRecords = async (filter: BinnacleFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/Binnacle?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (binnacle: Binnacle): Promise<BinnacleRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/Binnacle', binnacle);
    return requestData;
}

const edit = async (binnacle: Binnacle): Promise<BinnacleRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/Binnacle', binnacle);
    return requestData;
}

const getById = async (binnacleId: number): Promise<Binnacle> => {
    const requestData = await get(DEFAULT_API_PATH + '/Binnacle/' + binnacleId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
        await deleteRecord(DEFAULT_API_PATH + '/Binnacle/' + id);
        systemMessage({ "type": 'info', "description": DELETING_RECORD_SUCCESS });
      } catch (e) {
          const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
          systemMessage({ "type": error.type, "description": error.message });
      }
}

export const useDataBinnacle = () => {
    const binnacleSave = async (binnacle: Binnacle): Promise<BinnacleRow> => {
        try {
            const savedRecord = await add(binnacle);
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            return savedRecord;      
        } catch (e: any) {
           const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const deleteBinnacle = async (id:number): Promise<boolean>=>{
        const deleted=await deleteRow(id);
        return true;
    }
    return {
        binnacleSave,
        deleteBinnacle
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as BinnacleRepository