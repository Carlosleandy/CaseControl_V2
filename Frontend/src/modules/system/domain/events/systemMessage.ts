
import type { NotificationParams } from '@/modules/system/composable/useNotifications'
import { dispatchEvent } from '@/modules/shared/utility/eventHandler';

export const systemMessage = (message: NotificationParams) => {
    dispatchEvent('SYSTEM_MESSAGE', message);
}
