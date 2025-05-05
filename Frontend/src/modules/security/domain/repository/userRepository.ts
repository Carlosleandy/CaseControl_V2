import type { ModulePrivilegeAdapter } from "../dto/userPrivileges"
import type { User, UserForm } from "../entity/user"

export interface UserRepository {
    saveUser(user: UserForm): Promise<User>
    loadUsers(filter: CriteriaFilter | null): Promise<GridRecord>,
    getUserById(userId: string):Promise<UserForm>,   
    getUserPrivileges(userId: string):Promise<ModulePrivilegeAdapter[]>,      
    changeUserStatus(userId: string, status: boolean): Promise<boolean>   
    userExists(userName: string): Promise<boolean>   
}
