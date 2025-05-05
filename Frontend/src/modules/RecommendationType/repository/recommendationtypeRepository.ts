import type { RecommendationTypeRepository } from '../domain/repository/recommendationtypeRepository'
import type { RecommendationType, RecommendationTypeRow, RecommendationTypeFilter } from '../domain/model/recommendationtype'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from "vue";

const loadRecords = async (filter: RecommendationTypeFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/RecommendationType?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (recommendationtype: RecommendationType): Promise<RecommendationTypeRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/RecommendationType', recommendationtype);
    return requestData;
}

const edit = async (recommendationtype: RecommendationType): Promise<RecommendationTypeRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/RecommendationType', recommendationtype);
    return requestData;
}

const getById = async (recommendationtypeId: number): Promise<RecommendationType> => {
    const requestData = await get(DEFAULT_API_PATH + '/RecommendationType/' + recommendationtypeId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/RecommendationType/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataRecommendationType = () => {
    const recommendationTypeList = ref([]);
    
    async function getAllRecommendationTypes (){
        try {
            recommendationTypeList.value = await getOnly(DEFAULT_API_PATH + '/RecommendationType');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        recommendationTypeList,
        getAllRecommendationTypes
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as RecommendationTypeRepository