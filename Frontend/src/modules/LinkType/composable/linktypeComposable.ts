import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { LinkTypeRepository } from '../domain/repository/linktypeRepository'
import { 
    type LinkTypeFilter, 
    type LinkType, 
    type LinkTypeRow,
    validateLinkType 
} from '../domain/model/linktype'
import { evaluateFilter, resetPageScroll } from '@/modules/shared/utility/records'
import { computed, ref } from 'vue'

const entities = ref<LinkTypeRow[]>([]);
const totalRecords = ref(0);

export type linktypeProps = {
    linktypeRepository : LinkTypeRepository,
}

const filter = ref<PaginationDTO>({
    id: 0,
    page: 1,
    recordsNumber: 10,
    filter: ''
});

export const useLinkType = (props: linktypeProps) => {
    const lastFilterHash = ref(0);
    const loadingRecords = ref(false);
    const savingRecord = ref(false);
    const showFilter = ref(false);
    const showDialog = ref(false);  
    const linktypeRecord = ref({} as LinkType);
    const filterIsApplied = computed(() => Object.keys(filter.value.filters).length > 0);
    
    const setLinkTypeData = (linktype: LinkType): void => {
        linktypeRecord.value = linktype;
    }

    const getAllRecords = async () => {    
        try {
            const data = await props.linktypeRepository.loadRecords(filter.value);
            entities.value = [...(data.records || [])];
            totalRecords.value = data.totalRecordsQty;
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
        
        loadingRecords.value = false;
    }

    const saveLinkType = async () => {
        try {
            validateLinkType(linktypeRecord.value);

            let linktype = ref({} as LinkType);
            
            if(linktypeRecord.value.id > 0){
                linktype = await props.linktypeRepository.edit(linktypeRecord.value);
            }
            else{
                linktype = await props.linktypeRepository.add(linktypeRecord.value);
            }
            
            if(!linktype) {
                systemMessage({ "type": 'error', "description": SAVING_RECORD_ERROR });
            };
            
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            resetPageScroll();
            return linktype;
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
        }
    }

    const getLinkTypeById = async (linktypeId: number) => {
        try {
            return await props.linktypeRepository.getById(linktypeId);            
        } catch(e) {
            systemMessage({ "type": "error", "description": LOADING_DATA_ERROR });
        }
    }

    const deleteRecord = async (linktypeId: number): Promise<boolean> => {
        try {            
            await props.linktypeRepository.deleteRow(linktypeId);         
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
        linktypeRecord,
        loadingRecords,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        setLinkTypeData,
        getAllRecords,
        saveLinkType,
        getLinkTypeById,
		deleteRecord
    }
}

