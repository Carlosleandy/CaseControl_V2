import type { City } from "../model/city"

export interface SystemRepository {
    loadCities(): Promise<City[]>,
}
