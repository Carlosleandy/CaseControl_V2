import { ref } from 'vue'
import { defineStore } from 'pinia'
import { getEncrypt } from '@/modules/shared/utility/localStorage'
import type { Auth } from '../domain/entity/auth';
import type { User } from '../domain/entity/user';

const MAIN_ADMIN_KEY = 'ADMIN_MAIN';

export const useAuthStore = defineStore('auth', () => {
    const session = getEncrypt('session', true) as Auth;

    const token = ref(session?.token);
    const systemMenu = ref( (session?.menu || []) as Record<string, any>[])
    const privileges = ref(session?.privileges || []);
    const user = ref(session?.user || {});
    const isSuperUser = ref(privileges.value.indexOf(MAIN_ADMIN_KEY) > -1);

    function setMenu(menu: Record<string, any>[]) {
        systemMenu.value = menu;
    }

    function setToken(authToken: string) {
        token.value = authToken;
    }

    function setPrivileges(userPrivileges: string[]) {
        privileges.value = userPrivileges;
    }

    function setUser(logguedUser: User) {
        user.value = logguedUser;
    }

    function setIsSuperUser(privileges: string[]) {
        isSuperUser.value = privileges.indexOf(MAIN_ADMIN_KEY) > -1;
    }

    return { 
        systemMenu, token, privileges, user, isSuperUser,
        setMenu, setToken, setPrivileges, setUser, setIsSuperUser
    }
})
