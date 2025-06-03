import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { CaseAssignmentRepository } from '../domain/repository/caseassignmentRepository'
import { 
    type CaseAssignmentFilter, 
    type CaseAssignment, 
    type CaseAssignmentRow,
    validateCaseAssignment 
} from '../domain/model/caseassignment'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<CaseAssignmentRow[]>([]);
const totalRecords = ref(0);

export type caseassignmentProps = {
    caseassignmentRepository : CaseAssignmentRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useCaseAssignment = (props: caseassignmentProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const caseassignmentRecord = ref({} as CaseAssignment);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setCaseAssignmentData = (caseassignment: CaseAssignment): void => {
        caseassignmentRecord.value = caseassignment;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.caseassignmentRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveCaseAssignment = async () => {
        try {
            validateCaseAssignment(caseassignmentRecord.value);

            let savedRecord;
            
            if(caseassignmentRecord.value.id > 0){
                savedRecord = await props.caseassignmentRepository.edit(caseassignmentRecord.value);
            }
            else{
                savedRecord = await props.caseassignmentRepository.add(caseassignmentRecord.value);
            }
            
            // Only show success message
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return savedRecord;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            return null;
        }
    }

    const getCaseAssignmentById = async (caseassignmentId: number) => {
        try {
            return await props.caseassignmentRepository.getById(caseassignmentId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (caseassignmentId: number): Promise<boolean> => {
        try {            
            await props.caseassignmentRepository.deleteRow(caseassignmentId);         
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
        caseassignmentRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setCaseAssignmentData,
        getAllRecords,
        saveCaseAssignment,
        getCaseAssignmentById,
		deleteRecord
    }
}

