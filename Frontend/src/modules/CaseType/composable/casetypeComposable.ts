import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { CaseTypeRepository } from '../domain/repository/casetypeRepository'
import { 
    type CaseTypeFilter, 
    type CaseType, 
    type CaseTypeRow,
    validateCaseType 
} from '../domain/model/casetype'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<CaseTypeRow[]>([]);
const totalRecords = ref(0);

export type casetypeProps = {
    casetypeRepository : CaseTypeRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useCaseType = (props: casetypeProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const casetypeRecord = ref({} as CaseType);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setCaseTypeData = (casetype: CaseType): void => {
        casetypeRecord.value = casetype;
    }

    const getAllRecords = async () => {    
        try { debugger;
            const data = await props.casetypeRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveCaseType = async () => {
        try { debugger;
            validateCaseType(casetypeRecord.value);

            let casetype = ref({} as CaseType);

            if(casetypeRecord.value.id > 0){
                casetype = await props.casetypeRepository.edit(casetypeRecord.value);
            }
            else{
                casetype = await props.casetypeRepository.add(casetypeRecord.value);
            }
            
            if(!casetype) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };

            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return casetype;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getCaseTypeById = async (casetypeId: number) => {
        try {
            return await props.casetypeRepository.getById(casetypeId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (casetypeId: number): Promise<boolean> => {
        try {            
            await props.casetypeRepository.deleteRow(casetypeId);         
            systemMessage({ "type": 'info', "description": DELETING_RECORD_SUCCESS });
            return true;
        } catch(e) {
            const error = getErrorInformation(e as Error, DELETING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            return false;
        }
    }


    return {
        totalRecords,
        entities,
        casetypeRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setCaseTypeData,
        getAllRecords,
        saveCaseType,
        getCaseTypeById,
        deleteRecord
    }
}

