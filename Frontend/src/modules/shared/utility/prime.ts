
export const showConfirm = ({
    confirm, event, callback, message,
    confirmText, cancelText
}: {
    confirm: any, event: any, 
    callback: any, message: string,
    confirmText: string,
    cancelText: string
}) => {
    confirm.require({
        target: event.currentTarget,
        message: message,
        icon: 'pi pi-exclamation-circle',
        acceptIcon: 'pi pi-check',
        rejectIcon: 'pi pi-times',
        acceptLabel: confirmText,
        rejectLabel: cancelText,
        rejectClass: 'p-button-outlined p-button-sm',
        acceptClass: 'p-button-sm',
        accept: callback
    });
}