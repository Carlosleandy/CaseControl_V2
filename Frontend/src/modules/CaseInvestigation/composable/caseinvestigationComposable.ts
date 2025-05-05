import { getErrorInformation,CustomError } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { CaseInvestigationRepository } from '../domain/repository/caseinvestigationRepository'
import { 
    type CaseInvestigationFilter, 
    type CaseInvestigation, 
    type CaseInvestigationRow,
    validateCaseInvestigation 
} from '../domain/model/caseinvestigation'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<CaseInvestigationRow[]>([]);
const totalRecords = ref(0);
const statuses = ref([]);

export type caseinvestigationProps = {
    caseinvestigationRepository : CaseInvestigationRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useCaseInvestigation = (props: caseinvestigationProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const showDialogDetails = ref(false);  
    const showDialogStatusChange = ref(false);  
    const showDialogAddRecovery = ref(false);  
    const showDialogChangeHist = ref(false); 
    const showDialogLinked = ref(false);  
    const showDialogAddAssignment = ref(false);  
    const showDialogRecoveryHist = ref(false);  
    const showDialogAddEvidence = ref(false);  
    const showDialogAddLinkeds = ref(false);  
    const showDialogAddInterview = ref(false); 
    const showDialogAddFaultLinked = ref(false);
    const savesavingRecordLinkeds = ref(false); 
    const savesavingRecordInterview = ref(false); 
    const savesavingRecordFaultLinked = ref(false); 
    const caseinvestigationRecord = ref({} as CaseInvestigation);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setCaseInvestigationData = (caseinvestigation: CaseInvestigation): void => { debugger;
        caseinvestigationRecord.value = caseinvestigation;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.caseinvestigationRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveCaseInvestigation = async () => {
        try {
            validateCaseInvestigation(caseinvestigationRecord.value);

                  let caseinvestigation = ref({} as CaseInvestigation);
            
            if(caseinvestigationRecord.value.id > 0){ 
                caseinvestigation = await props.caseinvestigationRepository.edit(caseinvestigationRecord.value);
            }
            else{
                caseinvestigation = await props.caseinvestigationRepository.add(caseinvestigationRecord.value);
            }
            
            if(!caseinvestigation) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return caseinvestigation;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });

            // Si es un error de validación, enfocamos el campo con error
           if (e instanceof CustomError && (e as any).tag) {
                   const field = document.querySelector(`[data-field="${(e as any).tag}"]`);
                 if (field) {
                    (field as HTMLElement).focus();
                } 
            }
            throw e;

        }
    }

    const getCaseInvestigationById = async (caseinvestigationId: number) => {
        try { debugger;
            return await props.caseinvestigationRepository.getById(caseinvestigationId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (caseinvestigationId: number): Promise<boolean> => {
        try {            
            await props.caseinvestigationRepository.deleteRow(caseinvestigationId);         
            systemMessage({ "type": 'info', "description": DELETING_RECORD_SUCCESS });
            return true;
        } catch(e) {
            const error = getErrorInformation(e as Error, DELETING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            return false;
        }
    }

    return {
        statuses,
        totalRecords,
        entities,
        caseinvestigationRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        showDialogDetails,
        showDialogStatusChange,
        showDialogChangeHist,
        showDialogAddEvidence,
        showDialogAddLinkeds,
        showDialogAddInterview,
        showDialogLinked,
        showDialogAddRecovery,
        showDialogAddFaultLinked,
        showDialogAddAssignment,
        showDialogRecoveryHist,
        savesavingRecordLinkeds,
        savesavingRecordInterview,
        savesavingRecordFaultLinked,
        filterIsApplied,
        setCaseInvestigationData,
        getAllRecords,
        saveCaseInvestigation,
        getCaseInvestigationById,
		deleteRecord
    }
}

