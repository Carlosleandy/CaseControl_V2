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
debugger;
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
        try {
            roleList.value = await getOnly(DEFAULT_API_PATH + '/Roles');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        roleList,
        getAllRoles
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as RoleRepository