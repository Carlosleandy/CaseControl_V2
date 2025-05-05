import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { RecommendationRepository } from '../domain/repository/recommendationRepository'
import { 
    type RecommendationFilter, 
    type Recommendation, 
    type RecommendationRow,
    validateRecommendation 
} from '../domain/model/recommendation'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<RecommendationRow[]>([]);
const totalRecords = ref(0);

export type recommendationProps = {
    recommendationRepository : RecommendationRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useRecommendation = (props: recommendationProps) => {
    const lastFilterHashRecommendation = ref(0);
    const loadingRecordsRecommendation = ref(false);
    const savingRecordRecommendation = ref(false);
    const showFilterRecommendation = ref(false);
    const showDialogRecommendation = ref(false);  
    const recommendationRecord = ref({} as Recommendation);
    const filterIsAppliedRecommendation = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setRecommendationData = (recommendation: Recommendation): void => {
        recommendationRecord.value = recommendation;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.recommendationRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecordsRecommendation.value = false;
    }

    const saveRecommendation = async () => {
        try {
            validateRecommendation(recommendationRecord.value);

            let recommendation = ref({} as Recommendation);
            
            if(recommendationRecord.value.id > 0){
                recommendation = await props.recommendationRepository.edit(recommendationRecord.value);
            }
            else{
                recommendation = await props.recommendationRepository.add(recommendationRecord.value);
            }
            
            if(!recommendation) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return recommendation;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getRecommendationById = async (recommendationId: number) => {
        try {
            return await props.recommendationRepository.getById(recommendationId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (recommendationId: number): Promise<boolean> => {
        try {      
            await props.recommendationRepository.deleteRow(recommendationId);         
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
        recommendationRecord,
        loadingRecordsRecommendation,
        filter,
        savingRecordRecommendation,
        showFilterRecommendation,
        showDialogRecommendation,
        filterIsAppliedRecommendation,
        setRecommendationData,
        getAllRecords,
        saveRecommendation,
        getRecommendationById,
		deleteRecord
    }
}

