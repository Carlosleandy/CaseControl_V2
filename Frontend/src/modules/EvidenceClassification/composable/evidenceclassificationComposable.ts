import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { EvidenceClassificationRepository } from '../domain/repository/evidenceclassificationRepository'
import { 
    type EvidenceClassificationFilter, 
    type EvidenceClassification, 
    type EvidenceClassificationRow,
    validateEvidenceClassification 
} from '../domain/model/evidenceclassification'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<EvidenceClassificationRow[]>([]);
const totalRecords = ref(0);

export type evidenceclassificationProps = {
    evidenceclassificationRepository : EvidenceClassificationRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useEvidenceClassification = (props: evidenceclassificationProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const evidenceclassificationRecord = ref({} as EvidenceClassification);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setEvidenceClassificationData = (evidenceclassification: EvidenceClassification): void => {
        evidenceclassificationRecord.value = evidenceclassification;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.evidenceclassificationRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveEvidenceClassification = async () => {
        try {
            validateEvidenceClassification(evidenceclassificationRecord.value);

            let evidenceclassification = ref({} as EvidenceClassification);
            
            if(evidenceclassificationRecord.value.id > 0){
                evidenceclassification = await props.evidenceclassificationRepository.edit(evidenceclassificationRecord.value);
            }
            else{
                evidenceclassification = await props.evidenceclassificationRepository.add(evidenceclassificationRecord.value);
            }
            
            if(!evidenceclassification) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return evidenceclassification;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getEvidenceClassificationById = async (evidenceclassificationId: number) => {
        try {
            return await props.evidenceclassificationRepository.getById(evidenceclassificationId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (evidenceclassificationId: number): Promise<boolean> => {
        try {            
            await props.evidenceclassificationRepository.deleteRow(evidenceclassificationId);         
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
        evidenceclassificationRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setEvidenceClassificationData,
        getAllRecords,
        saveEvidenceClassification,
        getEvidenceClassificationById,
		deleteRecord
    }
}

