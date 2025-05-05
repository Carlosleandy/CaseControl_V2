import { get, DEFAULT_API_PATH } from '@/modules/shared/http_handler'
import type { SystemRepository } from '../domain/repository/systemRepository'
import type { City } from '../domain/model/city'

const loadCities = async (): Promise<City[]> => {
    const requestData = await get(DEFAULT_API_PATH + '/system/city');
    return requestData.data;
}

export default {
    loadCities
} as SystemRepository