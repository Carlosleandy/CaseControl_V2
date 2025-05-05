export type ModulePrivilege  = {
    id: number; 
    name: string;
    systemModuleId: number;
    systemModuleName: string;
}

export type ModulePrivilegeAdapter  = {
    id: number; 
    name: string;
    privileges: GenericKeyValue[]
}