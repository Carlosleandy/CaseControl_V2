import { getDate } from "@/modules/shared/utility/date";
import type { CaseManagerRow, CaseManagerRecord } from "../../domain/model/caseManager";

export const getCaseManagerRecords = (response: RequestResponse): GridRecord => {
    const caseManagerRecords = response.data.map((data: any) => {
        data.hiredDate = data.hiredDate ? getDate(data.hiredDate) : '';
        return data;
    });

    return {
        records: caseManagerRecords,
        totalRecordsQty: Number(response.headers['x-totalrecords'])
    }
}

export const getCaseManagerRow = (response: RequestResponse): CaseManagerRow => {
    const user = response.data;
    user.hiredDate = getDate(user.hiredDate);
    return user;
}

export const getCaseManagerRecord = (response: RequestResponse): CaseManagerRecord => {
    const caseManager = response.data as CaseManagerRecord;
    caseManager.employee.hiredDate = new Date(getDate(caseManager.employee.hiredDate));
    return caseManager;
}