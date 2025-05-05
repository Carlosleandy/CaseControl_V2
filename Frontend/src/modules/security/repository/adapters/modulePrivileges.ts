import type { ModulePrivilege, ModulePrivilegeAdapter } from '../../domain/dto/userPrivileges';

export const getModulePrivileges = (privileges: ModulePrivilege[]): ModulePrivilegeAdapter[] => {
    const modules: ModulePrivilegeAdapter[] = [];
    const indexes: Record<number, number> = {};
    let index = undefined;
    let counter = 0;

    for(const privilege of privileges) {
        index = indexes[privilege.systemModuleId];

        if(index == undefined) {
            indexes[privilege.systemModuleId] = counter;
            modules.push({
                id: privilege.systemModuleId,
                name: privilege.systemModuleName,
                privileges: []
            });
            index = counter;
            counter++;
        }
    
        modules[index].privileges.push({
            id: privilege.id,
            name: privilege.name
        });
    }

    return modules;
}