import { getErrorInformation } from '@/modules/shared/domain/errors'
import { systemMessage } from '@/modules/system/domain/events/systemMessage'
import { LOADING_DATA_ERROR, SAVING_RECORD_ERROR, SAVING_RECORD_SUCCESS, DELETING_RECORD_SUCCESS, DELETING_RECORD_ERROR } from '@/modules/shared/domain/commonMessages'
import type { CaseInvestigationRepository } from '../domain/repository/caseinvestigationRepository'
import type { CaseInvestigation, RecoveryHistory, CaseInvestigationRow, CaseInvestigationFilter, CaseAssignment, CaseStatusChange, Evidence, CasesByLinkedCodeDTO, RankingCasesUserDTO } from '../domain/model/caseinvestigation'
import { getOnly, get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'
import { ref } from 'vue'
import jsPDF from 'jspdf'
import axios from "axios";
//import axios from 'node_modules/axios/index'

const loadRecords = async (filter: CaseInvestigationFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/Case?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (caseinvestigation: CaseInvestigation): Promise<CaseInvestigationRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/Case', caseinvestigation);
    return requestData;
}

const edit = async (caseinvestigation: CaseInvestigation): Promise<CaseInvestigationRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/Case', caseinvestigation);
    return requestData;
}

const getById = async (caseinvestigationId: number): Promise<CaseInvestigation> => {
    const requestData = await get(DEFAULT_API_PATH + '/Case/' + caseinvestigationId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/Case/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export const useDataCases = () => {
    const casesbystatussummary = ref([]);
    const casesbystatus = ref([]);

    const casesbyusernamesummary = ref([]);
    const casesbyusername = ref([]);

    const recoveryHist = ref([]);
    const changeHist = ref([]);
    const recoveryHistTotal = ref(0);

    const casesrecoverysummary = ref([]);

    const casesbycodelinked = ref({} as CasesByLinkedCodeDTO);
    const rankingcasesbyuser = ref({} as CasesByLinkedCodeDTO);

    const showDialogDetails = ref(false);  
    
    async function getCasesByStatusSummary (){
        try {
            casesbystatussummary.value = await getOnly(DEFAULT_API_PATH + '/Case/getcasesbystatussummary');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    async function getCasesByStatus (statusID: number) {
        try {
            casesbystatus.value = await getOnly(DEFAULT_API_PATH + '/Case/getcasesbystatus/' + statusID);            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function getCasesByCodeLinked (code: string) : Promise<CasesByLinkedCodeDTO> {
        try {
            casesbycodelinked.value = await getOnly(DEFAULT_API_PATH + '/Case/GetCasesByCodeLinked/' + code);    
            return casesbycodelinked;        
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function getRankingCasesByUser () : Promise<RankingCasesUserDTO> {
        try {
            rankingcasesbyuser.value = await getOnly(DEFAULT_API_PATH + '/Case/GetRankingCasesByUser');    
            return rankingcasesbyuser;        
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function getCasesByUserNameSummary (){
        try {
            casesbyusernamesummary.value = await getOnly(DEFAULT_API_PATH + '/Case/getcasesbyusernamesummary');            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    async function getCasesByUserName (username: string) {
        try {
            casesbyusername.value = await getOnly(DEFAULT_API_PATH + '/Case/getcasesbyusername?username=' + username);            
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function GeneratePDFCasesByLinkedCode (code: string) {
        try {
            const response = await fetch(DEFAULT_API_PATH + '/Case/GeneratePDFCasesByLinkedCode/' + code); 

            if(!response.ok){
                throw new Error('Error al descargar el reporte');
            }

            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download='Historial de Casos del Vinculado ' + code + '_' + today.getFullYear()+(today.getMonth()+1)+today.getDate();
            element.click();
            window.URL.revokeObjectURL(url);  
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function generatePDFCasesByStatusSummary () {
        try {
            const response = await fetch(DEFAULT_API_PATH + '/Case/generatePDFcasesbystatussummary'); 

            if(!response.ok){
                throw new Error('Error al descargar el reporte');
            }

            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download='Resúmen de Casos por Estados ' + today.getFullYear()+(today.getMonth()+1)+today.getDate();
            element.click();
            window.URL.revokeObjectURL(url);  
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function generatePDFCasesByUserNameSummary () {
        try {
            const response = await fetch(DEFAULT_API_PATH + '/Case/generatePDFcasesbyusernamesummary'); 

            if(!response.ok){
                throw new Error('Error al descargar el reporte');
            }

            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download='Resúmen de Casos por Usuarios ' + today.getFullYear()+(today.getMonth()+1)+today.getDate();
            element.click();
            window.URL.revokeObjectURL(url);  
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }


    async function generatePDFCasesByStatus (id: number) {
        try {
            const response = await fetch(DEFAULT_API_PATH + '/Case/generatePDFcasesbystatus/'+id); 

            if(!response.ok){
                throw new Error('Error al descargar el reporte');
            }

            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download='Casos en Estado ' + id + '_' + today.getFullYear()+(today.getMonth()+1)+today.getDate();
            element.click();
            window.URL.revokeObjectURL(url);  
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function generatePDFCasesByUserName (username: string) {
        try {
            const response = await fetch(DEFAULT_API_PATH + '/Case/generatePDFcasesbyusername?username=' + username);       

            if(!response.ok){
                throw new Error('Error al descargar el reporte');
            }

            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download='Casos del Empleado ' + username + '_' + today.getFullYear()+(today.getMonth()+1)+today.getDate();
            element.click();
            window.URL.revokeObjectURL(url);  
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function GeneratePDFRankingCasesByUser () {
        try {
            const response = await fetch(DEFAULT_API_PATH + '/Case/GeneratePDFRankingCasesByUser');       

            if(!response.ok){
                throw new Error('Error al descargar el reporte');
            }

            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download='Ranking de Casos por Usuario' + '_' + today.getFullYear()+(today.getMonth()+1)+today.getDate();
            element.click();
            window.URL.revokeObjectURL(url);  
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function addCaseAssignment (assignment: CaseAssignment): Promise<CaseAssignment> {
        try {
            const requestData = await post(DEFAULT_API_PATH + '/Case/AddCaseAssignment', assignment);
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            return requestData;  
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            throw new Error('Se ha presentado un eror: ' + error.message);
        }   
    }

    async function addCaseStatusChange (change: CaseStatusChange): Promise<CaseStatusChange> {
        try {
            const requestData = await post(DEFAULT_API_PATH + '/Case/AddCaseStatusChange', change);
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            return requestData;  
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            throw new Error('Se ha presentado un eror: ' + error.message);
        }   
    }

    async function getCasesAmountRecovery (caseid: number) {
        try {
            recoveryHist.value = await getOnly(DEFAULT_API_PATH + '/Case/getcasesamountrecovery/' + caseid); 
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function getCasesStatusChangeHistAsync (caseid: number) {
        try {
            changeHist.value = await getOnly(DEFAULT_API_PATH + '/Case/getcasesstatuschangehist/' + caseid); 
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function addCaseAmountRecovery (recovery: RecoveryHistory): Promise<RecoveryHistory> {
        try {
            const requestData = await post(DEFAULT_API_PATH + '/Case/addcaseamountrecovery', recovery);
            systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            return requestData;  
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            throw new Error('Se ha presentado un eror: ' + error.message);
        }   
    }

    async function getCasesRecoverySummary () {
        try {
            casesrecoverysummary.value = await getOnly(DEFAULT_API_PATH + '/Case/getcasesrecoverysummary'); 
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function addCaseEvidence (formData: FormData): Promise<Evidence> {
        try { debugger;
            const requestData = await axios.post(DEFAULT_API_PATH + '/Evidence/uploadfile', formData, {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          });
          systemMessage({ "type": 'success', "description": SAVING_RECORD_SUCCESS });
            return requestData;  
        } catch(e) {
            const error = getErrorInformation(e as Error, SAVING_RECORD_ERROR);
            systemMessage({ "type": error.type, "description": error.message });
            throw new Error('Se ha presentado un eror: ' + error.message);
        }  
    }

    async function generatePDFCasesRecoverySummary () {
        try {
            const response = await fetch(DEFAULT_API_PATH + '/Case/generatePDFcasesrecoverysummary');       

            if(!response.ok){
                throw new Error('Error al descargar el reporte');
            }

            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download='Resúmen de Recuperaciones por Casos_' + today.getFullYear()+(today.getMonth()+1)+today.getDate();
            element.click();
            window.URL.revokeObjectURL(url);  
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function generatePDFCasesStatusChangeHist (id: number) {
        try {
            const response = await fetch(DEFAULT_API_PATH + '/Case/generatePDFcasesstatuschangehist/' + id); 

            if(!response.ok){
                throw new Error('Error al descargar el reporte');
            }

            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download='Historial de Cambios de Estado del Caso - ' + id + '_' + today.getFullYear()+(today.getMonth()+1)+today.getDate();
            element.click();
            window.URL.revokeObjectURL(url);  
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function generatePDFCasesAmountRecovery (id: number) {
        try {
            const response = await fetch(DEFAULT_API_PATH + '/Case/generatePDFcasesamountrecovery/' + id); 

            if(!response.ok){
                throw new Error('Error al descargar el reporte');
            }

            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download='Recuperaciones del Caso ' + id + '_' + today.getFullYear()+(today.getMonth()+1)+today.getDate();
            element.click();
            window.URL.revokeObjectURL(url);  
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }

    async function downloadFileEvidenceAsync (evidence: Evidence) {
        try {
            const response = await fetch(DEFAULT_API_PATH + `/Evidence/downloadfile/${evidence.hash}`); 

            if(!response.ok){
                throw new Error('Error al descargar el archivo');
            }
                    
            const today = new Date();

            const blob = await response.blob();
            const url = window.URL.createObjectURL(blob);
            const element = document.createElement('a');
            element.href=url;
            element.download=evidence.name+evidence.extension;
            element.click();
            window.URL.revokeObjectURL(url); 
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
      };

      const deleteEvidence = async (hash:string): Promise<boolean>=>{
        try { debugger;
            await deleteRecord(DEFAULT_API_PATH + `/Evidence/${hash}`);
            return true;
          } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
          }
    }

    return {
        showDialogDetails,
        casesbystatussummary,
        casesbystatus,
        casesbyusernamesummary,
        casesbyusername,
        recoveryHist,
        recoveryHistTotal,
        casesrecoverysummary,
        casesbycodelinked,
        rankingcasesbyuser,
        changeHist,
        getCasesByStatusSummary,
        getCasesByStatus,
        getCasesByUserNameSummary,
        getCasesByUserName,
        getCasesByCodeLinked,
        getRankingCasesByUser,
        generatePDFCasesByStatusSummary,
        generatePDFCasesByUserNameSummary,
        generatePDFCasesByStatus,
        generatePDFCasesByUserName,
        getCasesAmountRecovery,
        addCaseAmountRecovery,
        getCasesRecoverySummary,
        addCaseEvidence,
        generatePDFCasesRecoverySummary,
        generatePDFCasesAmountRecovery,
        generatePDFCasesStatusChangeHist,
        GeneratePDFCasesByLinkedCode,
        GeneratePDFRankingCasesByUser,
        addCaseAssignment,
        addCaseStatusChange,
        getCasesStatusChangeHistAsync,
        downloadFileEvidenceAsync,
        deleteEvidence
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as CaseInvestigationRepository