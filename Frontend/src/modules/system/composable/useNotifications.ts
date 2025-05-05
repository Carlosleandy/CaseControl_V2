
import { useToast } from "primevue/usetoast";
import { useI18n } from 'vue-i18n';
type MessageType = 'success' | 'error' | 'info' | 'warn';

export interface NotificationParams {
    type: MessageType;
    title?: string;
    description: string;
    duration?: number;
    descriptionData?: Record<string, unknown>
}

export const useNotifications = () => {
    const toast = useToast();
    const i18n = useI18n();

    const show = ({ type, title, duration, description, descriptionData = {} }: NotificationParams ) => {
        
        const defaultTitles: Record<MessageType, string> = {
            success: 'SYSTEM.system_info',
            error: 'SYSTEM.system_error',
            info: 'SYSTEM.system_info',
            warn: 'SYSTEM.warning'
        };

        const messageTitle = title || defaultTitles[type];

        toast.add({ severity: type, summary: i18n.t(messageTitle), detail: i18n.t(description, descriptionData), life: (duration || 3000) });
    }

    return {
        show
    }
}
