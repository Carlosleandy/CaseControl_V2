import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { RoleRepository } from '../domain/repository/roleRepository'
import type { Role, RoleRow, RoleFilter } from '../domain/model/role'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from "vue";

const loadRecords = async (filter: RoleFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/Roles?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (role: Role): Promise<RoleRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/Roles', role);
    return requestData;
}

const edit = async (role: Role): Promise<RoleRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/Roles', role);
    return requestData;
}

const getById = async (roleId: number): Promise<Role> => {
    const requestData = await get(DEFAULT_API_PATH + '/Roles/' + roleId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/Roles/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataRole = () => {
    const roleList = ref([]);
    
    async function getAllRoles (){
        try {debugger;
            roleList.value = await getOnly(DEFAULT_API_PATH + '/Roles');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function getRolByID (id: number){
        try {
            return await getById(id);                       
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function saveRole (roleRecord: Role): Promise<Role> {
        try {debugger;
            let role = ref({} as Role);
            
            if(roleRecord.id > 0){
                // role = await put(DEFAULT_API_PATH + '/Roles', roleRecord);
                role = await edit(roleRecord);
            }
            else{
                role = await add(roleRecord);
            }
            
            if(!role) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            // resetPageScroll();
            return role; 
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            throw new Error('Se ha presentado un eror: ' + error.message);
        }              
    }
    
    async function RemoveRow (id: number) : Promise<void> {
        try {
            await deleteRow(id);      
            systemMessage({ "type": 'info', "description": DELETING_RECORD_SUCCESS });                 
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    return {
        roleList,
        getAllRoles,
        getRolByID,
        saveRole,
        RemoveRow
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as RoleRepository