import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { InterviewRepository } from '../domain/repository/interviewRepository'
import { 
    type InterviewFilter, 
    type Interview, 
    type InterviewRow,
    validateInterview 
} from '../domain/model/interview'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<InterviewRow[]>([]);
const totalRecords = ref(0);

export type interviewProps = {
    interviewRepository : InterviewRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useInterview = (props: interviewProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const interviewRecord = ref({} as Interview);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setInterviewData = (interview: Interview): void => {
        interviewRecord.value = interview;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.interviewRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveInterview = async () => {
        try {
            validateInterview(interviewRecord.value);

            let interview = ref({} as Interview);
            
            if(interviewRecord.value.id > 0){
                interview = await props.interviewRepository.edit(interviewRecord.value);
            }
            else{
                interview = await props.interviewRepository.add(interviewRecord.value);
            }
            
            if(!interview) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return interview;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getInterviewById = async (interviewId: number) => {
        try {
            return await props.interviewRepository.getById(interviewId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (interviewId: number): Promise<boolean> => {
        try {            
            await props.interviewRepository.deleteRow(interviewId);         
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
        interviewRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setInterviewData,
        getAllRecords,
        saveInterview,
        getInterviewById,
		deleteRecord
    }
}

