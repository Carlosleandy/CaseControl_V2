import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { UserRepository } from '../domain/repository/userRepository'
import { 
    type UserFilter, 
    type User, 
    type UserRow,
    validateUser 
} from '../domain/model/user'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<UserRow[]>([]);
const totalRecords = ref(0);

export type userProps = {
    userRepository : UserRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useUser = (props: userProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const userRecord = ref({} as User);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setUserData = (user: User): void => {
        userRecord.value = user;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.userRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveUser = async () => {
        try {
            validateUser(userRecord.value);
debugger;
            let user = ref({} as User);
            
            if(userRecord.value.id > 0){
                user = await props.userRepository.edit(userRecord.value);
            }
            else{
                user = await props.userRepository.add(userRecord.value);
            }
            
            if(!user) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return user;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getUserById = async (userId: number) => {
        try {
            return await props.userRepository.getById(userId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (userId: number): Promise<boolean> => {
        try {            
            await props.userRepository.deleteRow(userId);         
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
        userRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setUserData,
        getAllRecords,
        saveUser,
        getUserById,
		deleteRecord
    }
}

