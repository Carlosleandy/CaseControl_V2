import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { FaultRepository } from '../domain/repository/faultRepository'
import { 
    type FaultFilter, 
    type Fault, 
    type FaultRow,
    validateFault 
} from '../domain/model/fault'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<FaultRow[]>([]);
const totalRecords = ref(0);

export type faultProps = {
    faultRepository : FaultRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useFault = (props: faultProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const faultRecord = ref({} as Fault);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setFaultData = (fault: Fault): void => {
        faultRecord.value = fault;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.faultRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveFault = async () => {
        try {
            validateFault(faultRecord.value);

            let fault = ref({} as Fault);
            
            if(faultRecord.value.id > 0){
                fault = await props.faultRepository.edit(faultRecord.value);
            }
            else{
                fault = await props.faultRepository.add(faultRecord.value);
            }
            
            if(!fault) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return fault;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getFaultById = async (faultId: number) => {
        try {
            return await props.faultRepository.getById(faultId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (faultId: number): Promise<boolean> => {
        try {            
            await props.faultRepository.deleteRow(faultId);         
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
        faultRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setFaultData,
        getAllRecords,
        saveFault,
        getFaultById,
		deleteRecord
    }
}

