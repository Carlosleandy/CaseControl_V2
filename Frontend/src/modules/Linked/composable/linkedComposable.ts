import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { LinkedRepository } from '../domain/repository/linkedRepository'
import { 
    type LinkedFilter, 
    type Linked, 
    type LinkedRow,
    validateLinked 
} from '../domain/model/linked'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<LinkedRow[]>([]);
const totalRecords = ref(0);

export type linkedProps = {
    linkedRepository : LinkedRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useLinked = (props: linkedProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const linkedRecord = ref({} as Linked);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setLinkedData = (linked: Linked): void => {
        linkedRecord.value = linked;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.linkedRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveLinked = async () => {
        try { debugger;
            validateLinked(linkedRecord.value);

            let linked = ref({} as Linked);
            
            if(linkedRecord.value.id > 0){
                linked = await props.linkedRepository.edit(linkedRecord.value);
            }
            else{
                linked = await props.linkedRepository.add(linkedRecord.value);
            }
            
            if(!linked) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return linked;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getLinkedById = async (linkedId: number) => {
        try {
            return await props.linkedRepository.getById(linkedId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (linkedId: number): Promise<boolean> => {
        try {            
            await props.linkedRepository.deleteRow(linkedId);         
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
        linkedRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setLinkedData,
        getAllRecords,
        saveLinked,
        getLinkedById,
		deleteRecord
    }
}

