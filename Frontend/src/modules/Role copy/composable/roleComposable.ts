import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { RoleRepository } from '../domain/repository/roleRepository'
import { 
    type RoleFilter, 
    type Role, 
    type RoleRow,
    validateRole 
} from '../domain/model/role'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<RoleRow[]>([]);
const totalRecords = ref(0);

export type roleProps = {
    roleRepository : RoleRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useRole = (props: roleProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const roleRecord = ref({} as Role);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setRoleData = (role: Role): void => {
        roleRecord.value = role;
    }

    const getAllRecords = async () => {    
        try {debugger;
            const data = await props.roleRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveRole = async () => {
        try {
            validateRole(roleRecord.value);

            let role = ref({} as Role);
            
            if(roleRecord.value.id > 0){
                role = await props.roleRepository.edit(roleRecord.value);
            }
            else{
                role = await props.roleRepository.add(roleRecord.value);
            }
            
            if(!role) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return role;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getRoleById = async (roleId: number) => {
        try {
            return await props.roleRepository.getById(roleId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (roleId: number): Promise<boolean> => {
        try {            
            await props.roleRepository.deleteRow(roleId);         
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
        roleRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setRoleData,
        getAllRecords,
        saveRole,
        getRoleById,
		deleteRecord
    }
}

