import type { Auth } from "../entity/auth"

export interface AuthRepository {
    authenticate(userName: string, password: string): Promise<Auth>
    refreshToken(): Promise<string>
}
