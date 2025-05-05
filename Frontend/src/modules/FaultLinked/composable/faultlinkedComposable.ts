import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { FaultLinkedRepository } from '../domain/repository/faultlinkedRepository'
import { 
    type FaultLinkedFilter, 
    type FaultLinked, 
    type FaultLinkedRow,
    validateFaultLinked 
} from '../domain/model/faultlinked'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<FaultLinkedRow[]>([]);
const totalRecords = ref(0);

export type faultlinkedProps = {
    faultlinkedRepository : FaultLinkedRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useFaultLinked = (props: faultlinkedProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const faultlinkedRecord = ref({} as FaultLinked);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setFaultLinkedData = (faultlinked: FaultLinked): void => {
        faultlinkedRecord.value = faultlinked;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.faultlinkedRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveFaultLinked = async () => {
        try {
            validateFaultLinked(faultlinkedRecord.value);

            let faultlinked = ref({} as FaultLinked);
            
            if(faultlinkedRecord.value.id > 0){
                faultlinked = await props.faultlinkedRepository.edit(faultlinkedRecord.value);
            }
            else{
                faultlinked = await props.faultlinkedRepository.add(faultlinkedRecord.value);
            }
            
            if(!faultlinked) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return faultlinked;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getFaultLinkedById = async (faultlinkedId: number) => {
        try {
            return await props.faultlinkedRepository.getById(faultlinkedId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (faultlinkedId: number): Promise<boolean> => {
        try {            
            await props.faultlinkedRepository.deleteRow(faultlinkedId);         
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
        faultlinkedRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setFaultLinkedData,
        getAllRecords,
        saveFaultLinked,
        getFaultLinkedById,
		deleteRecord
    }
}

