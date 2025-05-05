import { useSecurity } from "@/modules/auth/composable/useSecurity";

const claim = (el: any, privileges: any) => {
    const security = useSecurity();
    el.style.visibility = 'hidden';
    
    setTimeout(() => {
        if (security.hasPrivilege(privileges.value))
            el.style.visibility = 'visible';
        else
            el.remove();
    }, 10);

}

export default claim;