import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { WorkingGroupRepository } from '../domain/repository/workinggroupRepository'
import { 
    type WorkingGroupFilter, 
    type WorkingGroup, 
    type WorkingGroupRow,
    validateWorkingGroup 
} from '../domain/model/workinggroup'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<WorkingGroupRow[]>([]);
const totalRecords = ref(0);

export type workinggroupProps = {
    workinggroupRepository : WorkingGroupRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useWorkingGroup = (props: workinggroupProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const workinggroupRecord = ref({} as WorkingGroup);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setWorkingGroupData = (workinggroup: WorkingGroup): void => {
        workinggroupRecord.value = workinggroup;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.workinggroupRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveWorkingGroup = async () => {
        try {
            validateWorkingGroup(workinggroupRecord.value);

            let workinggroup = ref({} as WorkingGroup);
            
            if(workinggroupRecord.value.id > 0){
                workinggroup = await props.workinggroupRepository.edit(workinggroupRecord.value);
            }
            else{
                workinggroup = await props.workinggroupRepository.add(workinggroupRecord.value);
            }
            
            if(!workinggroup) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return workinggroup;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getWorkingGroupById = async (workinggroupId: number) => {
        try {
            return await props.workinggroupRepository.getById(workinggroupId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (workinggroupId: number): Promise<boolean> => {
        try {            
            await props.workinggroupRepository.deleteRow(workinggroupId);         
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
        workinggroupRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setWorkingGroupData,
        getAllRecords,
        saveWorkingGroup,
        getWorkingGroupById,
		deleteRecord
    }
}

