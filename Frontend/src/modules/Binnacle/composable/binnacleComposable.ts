import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { BinnacleRepository } from '../domain/repository/binnacleRepository'
import type { 
    BinnacleFilter, 
    Binnacle, 
    BinnacleRow,
    validateBinnacle 
} from '../domain/model/binnacle'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<BinnacleRow[]>([]);
const totalRecords = ref(0);

export type binnacleProps = {
    binnacleRepository : BinnacleRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useBinnacle = (props: binnacleProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecordBinnacle = ref(false);
    const showFilterBinnacle = ref(false);
    const showDialogBinnacle = ref(false);  
    const binnacleRecord = ref({} as Binnacle);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setBinnacleData = (binnacle: Binnacle): void => {
        binnacleRecord.value = binnacle;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.binnacleRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveBinnacle = async () => {
        try {
            validateBinnacle(binnacleRecord.value);

            let binnacle = ref({} as Binnacle);
            
            if(binnacleRecord.value.id > 0){
                binnacle = await props.binnacleRepository.edit(binnacleRecord.value);
            }
            else{
                binnacle = await props.binnacleRepository.add(binnacleRecord.value);
            }
            
            if(!binnacle) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return binnacle;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getBinnacleById = async (binnacleId: number) => {
        try {
            return await props.binnacleRepository.getById(binnacleId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (binnacleId: number): Promise<boolean> => {
        try {            
            await props.binnacleRepository.deleteRow(binnacleId);         
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
        binnacleRecord,
        loadingRecords,
        filter,
        savingRecordBinnacle,
        showFilterBinnacle,
        showDialogBinnacle,
        filterIsApplied,
        setBinnacleData,
        getAllRecords,
        saveBinnacle,
        getBinnacleById,
		deleteRecord
    }
}

