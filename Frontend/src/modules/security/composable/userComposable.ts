import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { UserRepository as UserRepo } from '../domain/repository/userRepository'
// import type { RoleRepository as RoleRepo } from '../domain/repository/roleRepository'
import { type User, type UserForm, type UserFilter, validateUser } from '../domain/entity/user'
import type { ModulePrivilegeAdapter } from '../domain/dto/userPrivileges'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
// import type { Role } from '../domain/entity/role'
import { computed, ref } from 'vue'

const users = ref<User[]>([]);
const totalRecords = ref(0);

const filter = ref({
    filters: {} as UserFilter,
    limit: 50,
    skip: 0
} as CriteriaFilter);

export type userProps = {
    userRepository : UserRepo,
    // roleRepository?: RoleRepo,
    userId?: string
}

export const useUser = (props: userProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const userId = ref('');
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);
    const loggedUserPriviles = ref<ModulePrivilegeAdapter[]>([]);
    const loggedUserRoles = ref<Role[]>([]);
    const userForm = ref({} as UserForm);
    const userAlreadyExists = ref(false);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);

    const setUserData = (user: UserForm): void => {
        userForm.value = user;
    }

    const getAllUsers = async () => {    
        const hasChanges = evaluateFilter(filter, lastFilterHash);
        
        if(totalRecords.value > 0 && totalRecords.value <= (filter.value.skip || 0)) {
            return users.value;
        }
        
        loadingRecords.value = true;

        try {
            const data = await props.userRepository.loadUsers(filter.value);
            if(hasChanges) {
                resetPageScroll();
                users.value = [...(data.records || [])];
            } else {
                users.value = [...users.value, ...(data.records || [])];
            }
            totalRecords.value = data.totalRecordsQty;
            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveUser = async () => {
        try {
            validateUser(userForm.value);
            const user = await props.userRepository.saveUser(userForm.value);
            if(!user) return null;

            if(userForm.value.id) { // Updating record if is an update
                const index = users.value.findIndex(x => x.id === userForm.value.id);
                users.value[index] = user;
            } else {                
                users.value = [user, ...users.value];
                totalRecords.value += 1;
            }
            
            resetPageScroll();
            return user;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getUserById = async (userId: string) => {
        try {
            return await props.userRepository.getUserById(userId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const getUserPrivileges = async (userId: string) => {
        return await props.userRepository.getUserPrivileges(userId);
    }

    const userExists = async (userName: string) => {
        try {
            userAlreadyExists.value = await props.userRepository.userExists(userName);
            if(userAlreadyExists.value) {
                systemMessage({ "type": "warn", "description": 'SECURITY.user_already_exists' });            
            }
        } catch (error) {
            userAlreadyExists.value = false;                        
        }
    }

    const toggleUserStatus = async (user: User) => {        
        user.isActive = !user.isActive;
        const result = await props.userRepository.changeUserStatus(user.id, user.isActive);
        
        if(!result) {
            user.isActive = !user.isActive;
        }
    }
    
    return {
        totalRecords,
        users,
        userForm,
        loadingRecords,
        filter,
        userId,
        savingRecord,
        showFilter,
        showDialog,
        loggedUserPriviles,       
        loggedUserRoles, 
        filterIsApplied,
        userAlreadyExists,
        setUserData,
        getAllUsers,
        saveUser,
        getUserById,
        getUserPrivileges,
        toggleUserStatus,
        userExists
    }
}

