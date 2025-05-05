import { get, getOnly, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { ref } from 'vue'


export const useAllCostCenters = () => {
    const allCostCenters = ref([]);
    
    async function getAllCostCenters (){
        try {
            allCostCenters.value = await getOnly(DEFAULT_API_PATH + '/Util/GetAllCostCenters');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        allCostCenters,
        getAllCostCenters
    }
}


