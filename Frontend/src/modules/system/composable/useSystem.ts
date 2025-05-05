import type { SystemRepository } from '@/modules/system/domain/repository/systemRepository'
import { systemMessage } from '../domain/events/systemMessage'
import { LOADING_DATA_ERROR } from '@/modules/shared/domain/commonMessages'

export type systemProps = {
    systemRepo : SystemRepository
}

export const useSystem = (props: systemProps) => {

    const loadCities = async () => {        
        try {
            return await props.systemRepo.loadCities();            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }        
    }
    
    return {
        loadCities
    }
}

