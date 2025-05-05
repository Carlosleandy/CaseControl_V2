import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { RecommendationStatusRepository } from '../domain/repository/recommendationstatusRepository'
import { 
    type RecommendationStatusFilter, 
    type RecommendationStatus, 
    type RecommendationStatusRow,
    validateRecommendationStatus 
} from '../domain/model/recommendationstatus'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<RecommendationStatusRow[]>([]);
const totalRecords = ref(0);

export type recommendationstatusProps = {
    recommendationstatusRepository : RecommendationStatusRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useRecommendationStatus = (props: recommendationstatusProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const recommendationstatusRecord = ref({} as RecommendationStatus);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setRecommendationStatusData = (recommendationstatus: RecommendationStatus): void => {
        recommendationstatusRecord.value = recommendationstatus;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.recommendationstatusRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveRecommendationStatus = async () => {
        try {
            validateRecommendationStatus(recommendationstatusRecord.value);

            let recommendationstatus = ref({} as RecommendationStatus);
            
            if(recommendationstatusRecord.value.id > 0){
                recommendationstatus = await props.recommendationstatusRepository.edit(recommendationstatusRecord.value);
            }
            else{
                recommendationstatus = await props.recommendationstatusRepository.add(recommendationstatusRecord.value);
            }
            
            if(!recommendationstatus) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return recommendationstatus;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getRecommendationStatusById = async (recommendationstatusId: number) => {
        try {
            return await props.recommendationstatusRepository.getById(recommendationstatusId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (recommendationstatusId: number): Promise<boolean> => {
        try {            
            await props.recommendationstatusRepository.deleteRow(recommendationstatusId);         
            systemMessage({ "type": 'info', "description": DELETING_RECORD_SUCCESS });
            return true;
        } catch(e) {
            const error = getErrorInformation(e as Error, DELETING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            return false;
        }
    }

    return {
        totalRecords,
        entities,
        recommendationstatusRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setRecommendationStatusData,
        getAllRecords,
        saveRecommendationStatus,
        getRecommendationStatusById,
		deleteRecord
    }
}

