import { useAuthStore } from '../store/auth'
import { computed } from 'vue'

export const useSecurity = () => {
    const system = useAuthStore();
    const userIsLogged = computed(() => (system?.token !== undefined && system?.token !== null));
    const user = computed(() => system.user);

    const hasPrivilege = (privileges: string[]): boolean => {
		if (!userIsLogged.value) {
			return false;
		}

		for (const privilege of privileges){
			if (system.privileges?.indexOf(privilege) > -1) {
				return true;
			}
		}

		return false;
    }

    return {
        userIsLogged,
		user,
        hasPrivilege
    }
}
