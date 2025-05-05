import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { UserLevelRepository } from '../domain/repository/userlevelRepository'
import { 
    type UserLevelFilter, 
    type UserLevel, 
    type UserLevelRow,
    validateUserLevel 
} from '../domain/model/userlevel'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<UserLevelRow[]>([]);
const totalRecords = ref(0);

export type userlevelProps = {
    userlevelRepository : UserLevelRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useUserLevel = (props: userlevelProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const userlevelRecord = ref({} as UserLevel);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setUserLevelData = (userlevel: UserLevel): void => {
        userlevelRecord.value = userlevel;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.userlevelRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveUserLevel = async () => {
        try {
            validateUserLevel(userlevelRecord.value);

            let userlevel = ref({} as UserLevel);
            
            if(userlevelRecord.value.id > 0){
                userlevel = await props.userlevelRepository.edit(userlevelRecord.value);
            }
            else{
                userlevel = await props.userlevelRepository.add(userlevelRecord.value);
            }
            
            if(!userlevel) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return userlevel;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getUserLevelById = async (userlevelId: number) => {
        try {
            return await props.userlevelRepository.getById(userlevelId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (userlevelId: number): Promise<boolean> => {
        try {            
            await props.userlevelRepository.deleteRow(userlevelId);         
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
        userlevelRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setUserLevelData,
        getAllRecords,
        saveUserLevel,
        getUserLevelById,
		deleteRecord
    }
}

