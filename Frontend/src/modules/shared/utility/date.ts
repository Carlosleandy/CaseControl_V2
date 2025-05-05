import { format as tFormat, tzDate } from "@formkit/tempo"

export const getDate = (date: Date, format='YYYY-MM-DD', timeZone = 'America/New_York') => {
    return tFormat(tzDate(date, timeZone),format);
}