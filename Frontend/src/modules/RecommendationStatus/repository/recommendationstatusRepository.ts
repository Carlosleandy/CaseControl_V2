import type { RecommendationStatusRepository } from '../domain/repository/recommendationstatusRepository'
import type { RecommendationStatus, RecommendationStatusRow, RecommendationStatusFilter } from '../domain/model/recommendationstatus'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from "vue";

const loadRecords = async (filter: RecommendationStatusFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/RecommendationStatus?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (recommendationstatus: RecommendationStatus): Promise<RecommendationStatusRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/RecommendationStatus', recommendationstatus);
    return requestData;
}

const edit = async (recommendationstatus: RecommendationStatus): Promise<RecommendationStatusRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/RecommendationStatus', recommendationstatus);
    return requestData;
}

const getById = async (recommendationstatusId: number): Promise<RecommendationStatus> => {
    const requestData = await get(DEFAULT_API_PATH + '/RecommendationStatus/' + recommendationstatusId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/RecommendationStatus/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataRecommendationStatus = () => {
    const recommendationStatusList = ref([]);
    
    async function getAllRecommendationStatus (){
        try {
            recommendationStatusList.value = await getOnly(DEFAULT_API_PATH + '/RecommendationStatus');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        recommendationStatusList,
        getAllRecommendationStatus
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as RecommendationStatusRepository