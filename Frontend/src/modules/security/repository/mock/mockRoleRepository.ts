import { get, post, patch, DEFAULT_API_PATH } from '@/modules/shared/http_handler'

import type { RoleRepository } from '../../domain/repository/roleRepository';
import type { RoleForm } from '../../domain/entity/role';
import { getRecords } from '@/modules/shared/utility/records';

const getUserRoles = async (userId: string): Promise<Role[]> =>{
    return [
        {
            "id": 1,
            "name": "Role 1",
            "isActive": true
        },
        {
            "id": 2,
            "name": "Role 2",
            "isActive": true
        }
    ];
}

const loadRoles = async (filter: RoleFilter | null): Promise<GridRecord> => {    
    const response = {
        data: [
            {
                "id": 1,
                "name": "Role 1",
                "isActive": true
            },
            {
                "id": 2,
                "name": "Role 2",
                "isActive": true
            }
        ],
        headers: {
            'x-totalrecords': 2
        }
    }
    return getRecords(response);
}

const saveRole = async (user: RoleForm): Promise<Role> => {
    return {
        "id": 1,
        "name": "Role 1",
        "isActive": true
    };
}

const getRoleById = async (roleId: number): Promise<Role> => {
    return {
        "id": 1,
        "name": "Role 1",
        "isActive": true
    };
}

const changeRoleStatus = async (roleId: number, status: boolean): Promise<boolean> => {
    return true;
}

export default {
    getUserRoles,
    loadRoles,
    saveRole,
    getRoleById,
    changeRoleStatus
} as RoleRepository