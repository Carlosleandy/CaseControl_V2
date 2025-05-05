import { getDate } from "@/modules/shared/utility/date";

const useDatePipe = (app: any) => {
    app.config.globalProperties.$formatDate = function(date: Date) {
        return getDate(new Date(date));
    };
}

export default useDatePipe;