import type { User } from './user'

export interface Auth {
    user: User;
    token: string;
    privileges: string[],
    menu: Record<string, unknown>[],
}
