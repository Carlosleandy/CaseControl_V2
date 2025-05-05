import { get, getOnly, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { ref } from 'vue'


export const useAllOffices = () => {
    const allOffices = ref([]);
    
    async function getAllOffices (){
        try {
            allOffices.value = await getOnly(DEFAULT_API_PATH + '/Util/GetAllOffices');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        allOffices,
        getAllOffices
    }
}


