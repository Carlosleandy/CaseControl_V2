import { getDate } from "@/modules/shared/utility/date";
import type { User } from "../../domain/entity/user";

export const getUserRecords = (response: RequestResponse): GridRecord => {
    const usersRecords = response.data.map((data: any) => {
        data.createdAt = getDate(data.createdAt);
        return data;
    });

    return {
        records: usersRecords,
        totalRecordsQty: Number(response.headers['x-totalrecords'])
    }
}

export const getUserSingleRecords = (response: RequestResponse): User => {
    const user = response.data;
    user.createdAt = getDate(user.createdAt);
    return user;
}