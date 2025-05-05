import type { WorkingGroupRepository } from '../domain/repository/workinggroupRepository'
import type { WorkingGroup, WorkingGroupRow, WorkingGroupFilter } from '../domain/model/workinggroup'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from "vue";

const loadRecords = async (filter: WorkingGroupFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/WorkingGroup?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (workinggroup: WorkingGroup): Promise<WorkingGroupRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/WorkingGroup', workinggroup);
    return requestData;
}

const edit = async (workinggroup: WorkingGroup): Promise<WorkingGroupRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/WorkingGroup', workinggroup);
    return requestData;
}

const getById = async (workinggroupId: number): Promise<WorkingGroup> => {
    const requestData = await get(DEFAULT_API_PATH + '/WorkingGroup/' + workinggroupId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/WorkingGroup/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataWorkingGroup = () => {
    const workingGroupList = ref([]);
    
    async function getAllWorkingGroups (){
        try {
            workingGroupList.value = await getOnly(DEFAULT_API_PATH + '/WorkingGroup');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        workingGroupList,
        getAllWorkingGroups
    }}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as WorkingGroupRepository