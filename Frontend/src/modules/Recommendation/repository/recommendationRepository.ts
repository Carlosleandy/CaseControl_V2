import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { RecommendationRepository } from '../domain/repository/recommendationRepository'
import type { Recommendation, RecommendationRow, RecommendationFilter } from '../domain/model/recommendation'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from 'vue';

const loadRecords = async (filter: RecommendationFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/Recommendation?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (recommendation: Recommendation): Promise<RecommendationRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/Recommendation', recommendation);
    return requestData;
}

const edit = async (recommendation: Recommendation): Promise<RecommendationRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/Recommendation', recommendation);
    return requestData;
}

const getById = async (recommendationId: number): Promise<Recommendation> => {
    const requestData = await get(DEFAULT_API_PATH + '/Recommendation/' + recommendationId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/Recommendation/' + id);
      systemMessage({ "type": 'info', "description": DELETING_RECORD_SUCCESS });
    } catch (e) {
        const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
        systemMessage({ "type": error.type, "description": error.message });
    }
}

export const useDataRecommendation = () => {
    const recommendationSave = async (recommendation: Recommendation): Promise<RecommendationRow> => {
        try {
            if(recommendation.id>0){
                const savedRecord = await edit(recommendation);
                systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
                return savedRecord;   
            } else {
                const savedRecord = await add(recommendation);
                systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
                return savedRecord; 
            }        
        } catch (e: any) {
           const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getByID = async (id:number): Promise<Recommendation> => {
        const recom=await getById(id);
        return recom;
    }

    const deleteRecommendation = async (id:number): Promise<boolean>=>{
        const deleted=await deleteRow(id);
        return true;
    }

    const casesbyrecommendationtypesummary = ref([]);
    const casesbyrecommendationtype = ref([]);
    const showDialogDetails = ref(false);  
    
    async function getCasesByRecommendationTypeSummary (){
        try {
            casesbyrecommendationtypesummary.value = await getOnly(DEFAULT_API_PATH + '/Recommendation/getcasesbyrecommendationtypesummary');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    async function getCasesByRecommendationType (statusID: number) {
        try {
            casesbyrecommendationtype.value = await getOnly(DEFAULT_API_PATH + '/Recommendation/getcasesbyrecommendationtype/' + statusID);            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }


    async function generatePDFCasesByRecommendationTypeSummary () {
        try {
            const response = await fetch(DEFAULT_API_PATH + '/Recommendation/generatePDFcasesbyrecommendationtypesummary'); 

            if(!response.ok){
                throw new Error('Error al descargar el reporte');
            }

            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download='Resúmen de Casos por Tipo de Recomendación _' + today.getFullYear()+(today.getMonth()+1)+today.getDate();
            element.click();
            window.URL.revokeObjectURL(url);  
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function generatePDFCasesByRecommendationType (id: number) {
        try {
            const response = await fetch(DEFAULT_API_PATH + '/Recommendation/generatePDFcasesbyrecommendationtype/'+id); 

            if(!response.ok){
                throw new Error('Error al descargar el reporte');
            }

            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download='Casos con Tipo de Recomendación ' + id + '_' + today.getFullYear()+(today.getMonth()+1)+today.getDate();
            element.click();
            window.URL.revokeObjectURL(url);  
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    return {
        casesbyrecommendationtypesummary,
        casesbyrecommendationtype,
        showDialogDetails,
        getByID,
        recommendationSave,
        deleteRecommendation,
        getCasesByRecommendationTypeSummary,
        getCasesByRecommendationType,
        generatePDFCasesByRecommendationTypeSummary,
        generatePDFCasesByRecommendationType
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as RecommendationRepository

