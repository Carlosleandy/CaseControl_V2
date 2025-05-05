import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { RecommendationTypeRepository } from '../domain/repository/recommendationtypeRepository'
import { 
    type RecommendationTypeFilter, 
    type RecommendationType, 
    type RecommendationTypeRow,
    validateRecommendationType 
} from '../domain/model/recommendationtype'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<RecommendationTypeRow[]>([]);
const totalRecords = ref(0);

export type recommendationtypeProps = {
    recommendationtypeRepository : RecommendationTypeRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useRecommendationType = (props: recommendationtypeProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const recommendationtypeRecord = ref({} as RecommendationType);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setRecommendationTypeData = (recommendationtype: RecommendationType): void => {
        recommendationtypeRecord.value = recommendationtype;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.recommendationtypeRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveRecommendationType = async () => {
        try {
            validateRecommendationType(recommendationtypeRecord.value);

            let recommendationtype = ref({} as RecommendationType);
            
            if(recommendationtypeRecord.value.id > 0){
                recommendationtype = await props.recommendationtypeRepository.edit(recommendationtypeRecord.value);
            }
            else{
                recommendationtype = await props.recommendationtypeRepository.add(recommendationtypeRecord.value);
            }
            
            if(!recommendationtype) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return recommendationtype;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getRecommendationTypeById = async (recommendationtypeId: number) => {
        try {
            return await props.recommendationtypeRepository.getById(recommendationtypeId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (recommendationtypeId: number): Promise<boolean> => {
        try {            
            await props.recommendationtypeRepository.deleteRow(recommendationtypeId);         
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
        recommendationtypeRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setRecommendationTypeData,
        getAllRecords,
        saveRecommendationType,
        getRecommendationTypeById,
		deleteRecord
    }
}

