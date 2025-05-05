import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { EvidenceRepository } from '../domain/repository/evidenceRepository'
import { 
    type EvidenceFilter, 
    type Evidence, 
    type EvidenceRow,
    validateEvidence 
} from '../domain/model/evidence'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<EvidenceRow[]>([]);
const totalRecords = ref(0);

export type evidenceProps = {
    evidenceRepository : EvidenceRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useEvidence = (props: evidenceProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const evidenceRecord = ref({} as Evidence);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setEvidenceData = (evidence: Evidence): void => {
        evidenceRecord.value = evidence;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.evidenceRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveEvidence = async () => {
        try {
            validateEvidence(evidenceRecord.value);

            let evidence = ref({} as Evidence);
            
            if(evidenceRecord.value.id > 0){
                evidence = await props.evidenceRepository.edit(evidenceRecord.value);
            }
            else{
                evidence = await props.evidenceRepository.add(evidenceRecord.value);
            }
            
            if(!evidence) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return evidence;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getEvidenceById = async (evidenceId: number) => {
        try {
            return await props.evidenceRepository.getById(evidenceId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (evidenceId: number): Promise<boolean> => {
        try {            
            await props.evidenceRepository.deleteRow(evidenceId);         
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
        evidenceRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setEvidenceData,
        getAllRecords,
        saveEvidence,
        getEvidenceById,
		deleteRecord
    }
}

