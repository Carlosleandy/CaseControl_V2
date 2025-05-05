import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { CaseStatusRepository } from '../domain/repository/casestatusRepository'
import { 
    type CaseStatusFilter, 
    type CaseStatus, 
    type CaseStatusRow,
    validateCaseStatus 
} from '../domain/model/casestatus'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<CaseStatusRow[]>([]);
const totalRecords = ref(0);

export type casestatusProps = {
    casestatusRepository : CaseStatusRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useData = () => {
    const caseses = ref([]);
    
    function loadData (){
        setTimeout(() => {
            caseses.value = [1,2,3,4,5]
        }, 3000);
    }
    return {
        caseses,
        loadData
    }
}

export const useCaseStatus = (props: casestatusProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const casestatusRecord = ref({} as CaseStatus);
    const casestatusList = ref({} as CaseStatus);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setCaseStatusData = (casestatus: CaseStatus): void => {
        casestatusRecord.value = casestatus;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.casestatusRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;

            casestatusList.value=data;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveCaseStatus = async () => {
        try {
            validateCaseStatus(casestatusRecord.value);

            let casestatus = ref({} as CaseStatus);
            
            if(casestatusRecord.value.id > 0){
                casestatus = await props.casestatusRepository.edit(casestatusRecord.value);
            }
            else{
                casestatus = await props.casestatusRepository.add(casestatusRecord.value);
            }
            
            if(!casestatus) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return casestatus;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getCaseStatusById = async (casestatusId: number) => {
        try {
            return await props.casestatusRepository.getById(casestatusId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (casestatusId: number): Promise<boolean> => {
        try {            
            await props.casestatusRepository.deleteRow(casestatusId);         
            systemMessage({ "type": 'info', "description": DELETING_RECORD_SUCCESS });
            return true;
        } catch(e) {
            const error = getErrorInformation(e as Error, DELETING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            return false;
        }
    }

    return {
        casestatusList,
        totalRecords,
        entities,
        casestatusRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setCaseStatusData,
        getAllRecords,
        saveCaseStatus,
        getCaseStatusById,
		deleteRecord
    }
}

