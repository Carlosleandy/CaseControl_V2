import type { EvidenceClassificationRepository } from '../domain/repository/evidenceclassificationRepository'
import type { EvidenceClassification, EvidenceClassificationRow, EvidenceClassificationFilter } from '../domain/model/evidenceclassification'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from "vue";

const loadRecords = async (filter: EvidenceClassificationFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/EvidenceClassification?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (evidenceclassification: EvidenceClassification): Promise<EvidenceClassificationRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/EvidenceClassification', evidenceclassification);
    return requestData;
}

const edit = async (evidenceclassification: EvidenceClassification): Promise<EvidenceClassificationRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/EvidenceClassification', evidenceclassification);
    return requestData;
}

const getById = async (evidenceclassificationId: number): Promise<EvidenceClassification> => {
    const requestData = await get(DEFAULT_API_PATH + '/EvidenceClassification/' + evidenceclassificationId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/EvidenceClassification/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataEvidenceClassification = () => {
    const classificationList = ref([]);
    
    async function getAllClassifications (){
        try {
            classificationList.value = await getOnly(DEFAULT_API_PATH + '/EvidenceClassification');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        classificationList,
        getAllClassifications
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as EvidenceClassificationRepository