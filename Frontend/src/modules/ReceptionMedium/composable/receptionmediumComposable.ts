import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { ReceptionMediumRepository } from '../domain/repository/receptionmediumRepository'
import { 
    type ReceptionMediumFilter, 
    type ReceptionMedium, 
    type ReceptionMediumRow,
    validateReceptionMedium 
} from '../domain/model/receptionmedium'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<ReceptionMediumRow[]>([]);
const totalRecords = ref(0);

export type receptionmediumProps = {
    receptionmediumRepository : ReceptionMediumRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useReceptionMedium = (props: receptionmediumProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const receptionmediumRecord = ref({} as ReceptionMedium);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setReceptionMediumData = (receptionmedium: ReceptionMedium): void => {
        receptionmediumRecord.value = receptionmedium;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.receptionmediumRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveReceptionMedium = async () => {
        try {
            validateReceptionMedium(receptionmediumRecord.value);

            let receptionmedium = ref({} as ReceptionMedium);
            
            if(receptionmediumRecord.value.id > 0){
                receptionmedium = await props.receptionmediumRepository.edit(receptionmediumRecord.value);
            }
            else{
                receptionmedium = await props.receptionmediumRepository.add(receptionmediumRecord.value);
            }
            
            if(!receptionmedium) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return receptionmedium;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getReceptionMediumById = async (receptionmediumId: number) => {
        try {
            return await props.receptionmediumRepository.getById(receptionmediumId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (receptionmediumId: number): Promise<boolean> => {
        try {            
            await props.receptionmediumRepository.deleteRow(receptionmediumId);         
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
        receptionmediumRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setReceptionMediumData,
        getAllRecords,
        saveReceptionMedium,
        getReceptionMediumById,
		deleteRecord
    }
}

