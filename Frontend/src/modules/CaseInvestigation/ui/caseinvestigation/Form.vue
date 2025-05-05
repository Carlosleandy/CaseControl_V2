<script setup lang="ts">
import type { CaseInvestigationRepository } from "../../domain/repository/caseinvestigationRepository";
import type { RecommendationRepository } from "../../../Recommendation/domain/repository/recommendationRepository"
import type { BinnacleRepository } from "../../../Binnacle/domain/repository/binnacleRepository"
import { LinkedRepository } from '../../../Linked/domain/repository/linkedRepository'
import { InterviewRepository } from '../../../Interview/domain/repository/interviewRepository'
import { useCaseInvestigation } from "../../composable/caseinvestigationComposable";
import type { Recommendation } from '../../../Recommendation/domain/model/recommendation'
import type { Binnacle } from '../../../Binnacle/domain/model/binnacle'
import { useRecommendation } from "../../../Recommendation/composable/recommendationComposable";
import { useBinnacle } from "../../../Binnacle/composable/binnacleComposable";
import RecommendationForm from '../../../Recommendation/ui/recommendation/Form.vue'
import BinnacleForm from '../../../Binnacle/ui/binnacle/Form.vue'
import LinkedForm from '../../../Linked/ui/linked/Form.vue'
import InterviewForm from '../../../Interview/ui/interview/Form.vue'
import EvidenceForm from '../caseinvestigation/FormAddEvidence.vue'
import FaultLinkedForm from '../../../FaultLinked/ui/faultlinked/Form.vue'

import { useDataCaseStatus } from "../../../CaseStatus/repository/casestatusRepository";
import { useDataCaseType } from "../../../CaseType/repository/casetypeRepository";
import { useDataReceptionMedium } from "../../../ReceptionMedium/repository/receptionmediumRepository";
import { useAllOffices } from "../../../shared/utility/office"
import { useConfirm } from 'primevue/useconfirm'
import { useDataRecommendation } from "../../../Recommendation/repository/recommendationRepository";
import { useDataBinnacle } from "../../../Binnacle/repository/binnacleRepository"
import { useDataFaultLinked } from "../../../FaultLinked/repository/faultlinkedRepository"
import { useDataLinkedType } from "../../../Linked/repository/linkedRepository"
import { useDataInterview } from "../../../Interview/repository/interviewRepository"
import { useLinked } from '../../../Linked/composable/linkedComposable'
import { useInterview } from '../../../Interview/composable/interviewComposable'
import { useFaultLinked } from '../../../FaultLinked/composable/faultlinkedComposable'

import type { CaseInvestigation, CaseAssignment, RecoveryHistory, Evidence } from "../../domain/model/caseinvestigation"
import { useDataCases } from "../../repository/caseinvestigationRepository"
import { useDataUsers } from "../../../User/repository/userRepository"

import { ref } from "vue";
import { Linked } from "../../../Linked/domain/model/linked";
import { Interview } from "../../../Interview/domain/model/interview";


const formRefAssignment = ref();

const assignmentRecord = ref({} as CaseAssignment);
const recoveryRecord = ref({} as RecoveryHistory);

const { usersOnly, getAllUserOnly } = useDataUsers();
const { deleteLinked } = useDataLinkedType();
const { deleteInterview } = useDataInterview();
const { addCaseAssignment, addCaseAmountRecovery, downloadFileEvidenceAsync, deleteEvidence, deleteAssignment, deleteRecovery } = useDataCases();


const isDisabled = ref(false);

const recommendationId = ref(0);
const binnacleId = ref(0);
const evidenceId = ref(0);
const linkedId = ref(0);
const interviewId = ref(0);
const confirm = useConfirm();
const recId = ref(0);
const binId = ref(0);
const isCloningRecord = ref(false);
const formRefRecommendation = ref();
const formRefBinnacle = ref();
const formRefLinked = ref();
const formRefInterview = ref();
const formRefFaultLinked = ref();
const formRefEvidence = ref();
const showDialogAssignment = ref(false);
const showDialogRecovery = ref(false);

const { caseStatusList, getAllCaseStatus } = useDataCaseStatus();

const { caseTypeList, getAllCaseTypes } = useDataCaseType();

const { allOffices, getAllOffices } = useAllOffices();

const { receptionMediumList, getAllReceptionMediums } =
  useDataReceptionMedium();

const { deleteRecommendation } = useDataRecommendation();
const { deleteBinnacle } = useDataBinnacle();
const { deleteFaultLinked } = useDataFaultLinked();

const props = defineProps<{
  caseinvestigationRepository: CaseInvestigationRepository;
  recordId?: number;
  isnew?: boolean;
}>();

const {
  recommendationRecord,
  savingRecordRecommendation,
  showDialogRecommendation,
        setRecommendationData,
        saveRecommendation,
        getRecommendationById,
		deleteRecord
} = useRecommendation({
  recommendationRepository: props.recommendationRepository,
});

const {
  binnacleRecord,
  savingRecordBinnacle,
  showDialogBinnacle,
        setBinnacleData,
        saveBinnacle,
        getBinnacleById
} = useBinnacle({
  binnacleRepository: props.binnacleRepository,
});

const { 
        linkedRecord,		
        getLinkedById,
        setLinkedData,
	} = useLinked({
        linkedRepository: props.linkedRepository
    });	

     const { 
        interviewRecord,
        getInterviewById,
        setInterviewData,
	} = useInterview({
        interviewRepository: props.interviewRepository
    });	
   

const {
  caseinvestigationRecord,
  showDialogAddEvidence,
  showDialogAddLinkeds,
  showDialogAddInterview,
  showDialogAddFaultLinked,
  savesavingRecordLinkeds,
  savesavingRecordInterview,
  savesavingRecordFaultLinked,
  saveCaseInvestigation,
  getCaseInvestigationById,
  setCaseInvestigationData,
} = useCaseInvestigation({
  caseinvestigationRepository: props.caseinvestigationRepository,
});

const loadInitialData = async () => {
  if (props.isnew) {
    isDisabled.value = true;
  } else {
    isDisabled.value = false;
  }
  
  // Cargar los tipos de caso y otros datos de referencia primero
  await Promise.all([
    getAllCaseTypes(),
    getAllReceptionMediums(),
    getAllCaseStatus(),
    getAllOffices(),
    getAllUserOnly()
  ]);
  
  if (props?.recordId) {
    try {
      const caseinvestigation = await getCaseInvestigationById(props.recordId);
      if (caseinvestigation) {
        if (props.isnew) {
          caseinvestigation.id = 0;
        }

        // Asegurarse de que el caseTypeId esté correctamente asignado
        if (caseinvestigation.caseType && caseinvestigation.caseType.id && !caseinvestigation.caseTypeId) {
          caseinvestigation.caseTypeId = caseinvestigation.caseType.id;
        }
        
        // Asegurarse de que el receptionMediumId esté correctamente asignado
        if (caseinvestigation.receptionMedium && caseinvestigation.receptionMedium.id && !caseinvestigation.receptionMediumId) {
          caseinvestigation.receptionMediumId = caseinvestigation.receptionMedium.id;
        }
        
        // Asegurarse de que el caseStatusId esté correctamente asignado
        if (caseinvestigation.caseStatus && caseinvestigation.caseStatus.id && !caseinvestigation.caseStatusId) {
          caseinvestigation.caseStatusId = caseinvestigation.caseStatus.id;
        }
        
        // Inicializar las asignaciones si no existen
        if (!caseinvestigation.caseAssignments) {
          caseinvestigation.caseAssignments = [];
        }
        
        setCaseInvestigationData(caseinvestigation);
        console.log('Case Investigation Data:', caseinvestigationRecord.value);
        console.log('Assignments:', caseinvestigationRecord.value.caseAssignments);
      }
    } catch (error) {
      console.error('Error al cargar los datos del caso:', error);
    }
  }
};

const openFormDialogRecommendation = (recommendationRecord: Recommendation | null, isCloning = false) => {
  recId.value = recommendationRecord ? recommendationRecord.id : 0;
  showDialogRecommendation.value = true;
  isCloningRecord.value = isCloning;
  recommendationId.value = recId.value; 
}

const openFormDialogBinnacle = (binnacleRecord: Binnacle | null, isCloning = false) => {
  binId.value = binnacleRecord ? binnacleRecord.id : 0;
  showDialogBinnacle.value = true;
  isCloningRecord.value = isCloning;
  binnacleId.value = binId.value; 
}

const openFormDialogFaultLinked = () => {
  showDialogAddFaultLinked.value = true;
}

const openFormDialogEvicence = (evidenceRecord: Evidence | null) => {
  showDialogAddEvidence.value = true;
}

const openFormDialogLinkeds = (linkedRecord: Linked | null) => { 
  linkedId.value = linkedRecord ? linkedRecord.id : 0;
  showDialogAddLinkeds.value = true;
  isCloningRecord.value = false; 
}

const openFormDialogInterview = (interviewRecord: Interview | null) => {
  interviewId.value = interviewRecord ? interviewRecord.id : 0;
  showDialogAddInterview.value = true;
}

const openFormDialogAssignment = () => {
  // Reiniciar el registro de asignación
  assignmentRecord.value = {} as CaseAssignment;
  // Asignar el ID del caso actual
  assignmentRecord.value.caseID = caseinvestigationRecord.value.id;
  // Mostrar el diálogo
  showDialogAssignment.value = true;
  console.log('Abriendo diálogo de asignación', assignmentRecord.value);
}

const openFormDialogRecovery = () => {
  recoveryRecord.value = {} as RecoveryHistory;
  recoveryRecord.value.caseID = caseinvestigationRecord.value.id;
  showDialogRecovery.value = true;
}

const downloadEvicence = (evidenceRecord: Evidence | null) => {
  downloadFileEvidenceAsync(evidenceRecord);
}

async function saveRec(): Promise<void> {
  savingRecordRecommendation.value = true;        
  const result = await formRefRecommendation.value.saveRec();
  savingRecordRecommendation.value = false;   
  showDialogRecommendation.value = false;
  loadInitialData();   
}

async function saveBin(): Promise<void> {
  savingRecordBinnacle.value = true;        
  const result = await formRefBinnacle.value.saveBin();
  savingRecordBinnacle.value = false;   
  showDialogBinnacle.value = false;
  loadInitialData();   
}

async function saveLinked(): Promise<void> {
  savesavingRecordLinkeds.value = true;        
  const result = await formRefLinked.value.saveLinked();
  savesavingRecordLinkeds.value = false;   
  showDialogAddLinkeds.value = false;
  loadInitialData();   
}

async function saveInterview(): Promise<void> {
  savesavingRecordInterview.value = true;        
  const result = await formRefInterview.value.saveInterview();
  savesavingRecordInterview.value = false;   
  showDialogAddInterview.value = false;
  loadInitialData();   
}

async function saveFaultLinked(): Promise<void> {
  savesavingRecordFaultLinked.value = true;        
  const result = await formRefFaultLinked.value.saveFaultLinked();
  savesavingRecordFaultLinked.value = false;   
  showDialogAddFaultLinked.value = false;
  loadInitialData();   
}

async function saveEvidence(): Promise<void> {
  savingRecordBinnacle.value = true;        
  const result = await formRefEvidence.value.saveEvidence();
  savingRecordBinnacle.value = false;   
  showDialogAddEvidence.value = false;
  loadInitialData();
}

async function deleteRecommendationById(id: number): Promise<void> {
        const result = await deleteRecommendation(id);
        if (result) {
            loadInitialData(); 
        }
    }

async function deleteAssignmentById(id: number): Promise<void> {
    const result = await deleteAssignment(id);
    if (result) {
        loadInitialData(); 
    }
}

async function deleteRecoveryById(id: number): Promise<void> {
    const result = await deleteRecovery(id);
    if (result) {
        loadInitialData(); 
    }
}

    function confirmDeleteRecommendation(event: any, id: number) {
        confirm.require({
            target: event.currentTarget,
            message: '¿Está seguro(a) que desea eliminar este registro?',
            icon: 'pi pi-question-circle',
            accept: () => {
            deleteRecommendationById(id);
            },
        });
    }

    async function deleteBinnacleById(id: number): Promise<void> {
        const result = await deleteBinnacle(id);
        if (result) {
            loadInitialData(); 
        }
    }

    function confirmDeleteBinnacle(event: any, id: number) {
        confirm.require({
            target: event.currentTarget,
            message: '¿Está seguro(a) que desea eliminar este registro?',
            icon: 'pi pi-question-circle',
            accept: () => {
            deleteBinnacleById(id);
            },
        });
    }

    function confirmDeleteAssignment(event: any, id: number) {
        confirm.require({
            target: event.currentTarget,
            message: '¿Está seguro(a) que desea eliminar este registro?',
            icon: 'pi pi-question-circle',
            accept: () => {
            deleteAssignmentById(id);
            },
        });
    }

function confirmDeleteRecovery(event: any, id: number) {
        confirm.require({
            target: event.currentTarget,
            message: '¿Está seguro(a) que desea eliminar este registro?',
            icon: 'pi pi-question-circle',
            accept: () => {
            deleteRecoveryById(id);
            },
        });
    }

    async function deleteFaultLinkedById(id: number): Promise<void> {
        const result = await deleteFaultLinked(id);
        if (result) {
            loadInitialData(); 
        }
    }

    function confirmDeleteFaultLinked(event: any, id: number) {
        confirm.require({
            target: event.currentTarget,
            message: '¿Está seguro(a) que desea eliminar este registro?',
            icon: 'pi pi-question-circle',
            accept: () => {
            deleteFaultLinkedById(id);
            },
        });
    }

    async function deleteLinkedById(id: number): Promise<void> {
        const result = await deleteLinked(id);
        if (result) {
            loadInitialData(); 
        }
    }

    function confirmDeleteLinked(event: any, id: number) {
        confirm.require({
            target: event.currentTarget,
            message: '¿Está seguro(a) que desea eliminar este registro?',
            icon: 'pi pi-question-circle',
            accept: () => {
            deleteLinkedById(id);
            },
        });
    }

    async function deleteInterviewById(id: number): Promise<void> {
        const result = await deleteInterview(id);
        if (result) {
            loadInitialData(); 
        }
    }

    function confirmDeleteInterview(event: any, id: number) {
        confirm.require({
            target: event.currentTarget,
            message: '¿Está seguro(a) que desea eliminar este registro?',
            icon: 'pi pi-question-circle',
            accept: () => {
            deleteInterviewById(id);
            },
        });
    }

    async function deleteEvidenceByHash(hash: string): Promise<void> {
        const result = await deleteEvidence(hash);
        if (result) {
            loadInitialData(); 
        }
    }

    function confirmDeleteEvidence(event: any, hash: string) {
        confirm.require({
            target: event.currentTarget,
            message: '¿Está seguro(a) que desea eliminar esta Evidencia?',
            icon: 'pi pi-question-circle',
            accept: () => {
            deleteEvidenceByHash(hash);
            },
        });
    }

  function confirmSaveAssignment(event: any) {
  saveAssignment();
}

function confirmSaveAmountRecovery(event: any) {
    saveAmountRecovery();
  }

const saveAssignment = async () => {
  console.log('Guardando asignación:', assignmentRecord.value);
  if(assignmentRecord.value && assignmentRecord.value.userID) {
    try {
      // Asegurarse de que el ID del caso esté asignado
      assignmentRecord.value.caseID = caseinvestigationRecord.value.id;
      // Guardar la asignación
      const result = await addCaseAssignment(assignmentRecord.value);
      console.log('Resultado de guardar asignación:', result);
      // Cerrar el diálogo
      showDialogAssignment.value = false;
      // Recargar los datos para actualizar el grid
      await loadInitialData();
    } catch (error) {
      console.error('Error al guardar la asignación:', error);
    }
  } else {
    console.warn('No se puede guardar la asignación: datos incompletos', assignmentRecord.value);
  }
}

const saveAmountRecovery = async () => {
        if(recoveryRecord) {
          recoveryRecord.value.caseID = caseinvestigationRecord.value.id;
            await addCaseAmountRecovery(recoveryRecord.value);
            showDialogRecovery.value = false;
            loadInitialData();
        }
    }

loadInitialData();
defineExpose({ save: saveCaseInvestigation });

</script>

<template>
  <div>
    <form ref="formRef">
      <div class="flexbox-grid container">
        <div class="card" style="--min: 50ch">
          <!-- <h2>{{ $t("SHARED.basic_information") }}</h2> -->

          <div class="card mb-3" style="background-color: var(--surface-card); padding: 1rem;">
            <h2>Información Básica</h2>
            <div class="flexbox-grid container">
        <div class="grid">
          <span class="p-float-label">
            <InputText v-model="caseinvestigationRecord.id" disabled />
            <label for="id">{{ $t("ID") }}</label>
          </span>
        </div>
        <div class="grid">
          <span class="p-float-label">
            <InputText v-model="caseinvestigationRecord.communicationNumber" />
            <label for="communicationNumber">{{$t("No. Comunicación")}}</label>
          </span>
        </div>
      </div>

      <div class="flexbox-grid container">
        <div class="grid">
          <span class="p-float-label">
            <InputText v-model="caseinvestigationRecord.subject" />
            <label for="subject">{{ $t("Asunto") }}</label>
          </span>
        </div>
      </div>

      <div class="flexbox-grid container">
        <div class="grid">
          <span class="p-float-label">
            <InputText v-model="caseinvestigationRecord.transmitter" />
            <label for="transmitter">{{ $t("Emisor Comunicación") }}</label>
          </span>
          <span class="p-float-label">
            <InputText v-model="caseinvestigationRecord.recipient" />
            <label for="recipient">{{ $t("Destinatario Comunicación") }}</label>
          </span>
          <span class="p-float-label">
            <Calendar v-model="caseinvestigationRecord.dateOfCommunication" dateFormat="dd/mm/yy" />
            <label for="dateOfCommunication">{{$t("Fecha de Comunicación")}}</label>
          </span>
          <span class="p-float-label">
            <Calendar v-model="caseinvestigationRecord.dateOfReceipt" dateFormat="dd/mm/yy" />
            <label for="dateOfReceipt">{{ $t("Fecha Recibida") }}</label>
          </span>

          <span class="p-float-label">
            <Dropdown
              v-model="caseinvestigationRecord.branch"
              :options="allOffices.data"
              optionValue="oficina_Parsed"
              optionLabel="oficina_Completa"
              filter
            />
            <label for="caseinvestigationRecord.branch">{{$t("Oficina")}}</label>
          </span>

        </div>
        <div class="grid">
          <span class="p-float-label">
            <InputNumber :minFractionDigits="2" :maxFractionDigits="2" v-model="caseinvestigationRecord.amountDetected" />
            <label for="amountDetected">{{ $t("Monto Detectado") }}</label>
          </span>
          <span class="p-float-label">
            <InputNumber :minFractionDigits="2" :maxFractionDigits="2" v-model="caseinvestigationRecord.amountInvestigated" />
            <label for="amountInvestigated">{{$t("Monto Investigado")}}</label>
          </span>
          <span class="p-float-label">
            <InputNumber :minFractionDigits="2" :maxFractionDigits="2" v-model="caseinvestigationRecord.amountRecovered" disabled />
            <label for="amountRecovered">{{ $t("Monto Recuperado") }}</label>
          </span>
          <span class="p-float-label">
            <InputNumber :minFractionDigits="2" :maxFractionDigits="2" v-model="caseinvestigationRecord.amountLost" />
            <label for="amountLost">{{ $t("Monto Pérdida") }}</label>
          </span>
          <span class="p-float-label">
            <InputText v-model="caseinvestigationRecord.affectedAccount" />
            <label for="affectedAccount">{{ $t("Cuenta Afectada") }}</label>
          </span>
        </div>
      </div>

      <div class="flexbox-grid container">
        <div class="grid">
          <span class="p-float-label">
            <Dropdown
              v-model="caseinvestigationRecord.caseTypeId"
              :options="caseTypeList.data"
              filter
              optionValue="id"
              optionLabel="name"
            />
            <label for="caseinvestigationRecord.caseTypeId">{{$t("Tipo Caso")}}</label>
          </span>
          <span class="p-float-label">
            <Dropdown
              v-model="caseinvestigationRecord.receptionMediumId"
              :options="receptionMediumList.data"
              filter
              optionValue="id"
              optionLabel="name"
            />
            <label for="caseinvestigationRecord.receptionMediumId">{{$t("Medio Recepción")}}</label>
          </span>

          <span class="p-float-label">
            <Dropdown
              v-model="caseinvestigationRecord.caseStatusId"
              :options="caseStatusList.data"
              optionValue="id"
              optionLabel="name"
              filter
            />
            <label for="caseinvestigationRecord.caseStatusId">{{$t("Estado")}}</label>
          </span>
        </div>
      </div>

      <div class="flexbox-grid container">
        <div class="grid">
          <span class="p-float-label">
            <TextArea
              v-model="caseinvestigationRecord.communicationReference"
              variant="filled"
              rows="4"
            />
            <label for="communicationReference">{{$t("Referencia Comunicación")}}</label>
          </span>
        </div>
      </div>
            </div>
          </div>
          
          <div class="card mb-3" style="background-color: var(--surface-card); padding: 1rem;">

            <div class="flexbox-grid container">
  <DataTable :value="caseinvestigationRecord.recommendations" 

                    size="small"
                    scrollable
                    scrollHeight="calc(100vh - 300px)"
                    showGridlines 
                    tableStyle="min-width: 50rem"                     
                    ref=""                     
                    paginator 
                    :rows="5"      
                    :rowsPerPageOptions="[5, 10, 25, 50]"
                >
                    <template #header>
                        <div class="flex corn">
                            <h1>{{ $t('Recomendaciones')}}</h1> 
                    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.new')" @click="openFormDialogRecommendation(null)" icon="pi pi-plus" severity="info" aria-label="User"  />
                        </div>
                    </template>
        
                    <Column field="id" :header="$t('ID')" style="width: 10%"></Column>
                    <Column field="title" :header="$t('Título')" style="width: 50%"></Column>
                    <Column field="dateRegistered" :header="$t('Fecha de Registro')" style="width: 10%"></Column>
                    <Column field="user.userName" :header="$t('Usuario Registró')" style="width: 10%"></Column>
                    <Column field="recommendationStatus.name" :header="$t('Estado')" style="width: 10%"></Column>
                    <Column :exportable="false" style="width: 10%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="openFormDialogRecommendation(data)" class="grid-button-text" icon="pi pi-pencil" v-tooltip.top="$t('COMMON_BUTTONS.editStatus')" text rounded raised outlined />
                                <PButton @click="($event: Event)=>{ confirmDeleteRecommendation($event, data.id) }" class="grid-button-text" icon="pi pi-trash" v-tooltip.top="$t('COMMON_BUTTONS.delete')" severity="danger" text rounded raised outlined />
                            </div>
                        </template>
                    </Column>                    
                </DataTable>       
            </div>
          </div>
          
          <div class="card mb-3" style="background-color: var(--surface-card); padding: 1rem;">

            <div class="flexbox-grid container">
  <DataTable :value="caseinvestigationRecord.binnacles" 
                    size="small"
                    scrollable
                    scrollHeight="calc(100vh - 300px)"
                    showGridlines 
                    tableStyle="min-width: 50rem"                     
                    ref=""                     
                    paginator 
                    :rows="5"      
                    :rowsPerPageOptions="[5, 10, 25, 50]"
                >
                    <template #header>
                        <div class="flex corn">
                            <h1>{{ $t('Bitácora')}}</h1>
    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.new')" @click="openFormDialogBinnacle(null)" icon="pi pi-plus" severity="info" aria-label="User"  />
                        </div>
                    </template>
        
                    <Column field="id" :header="$t('ID')" style="width: 10%"></Column>
                    <Column field="name" :header="$t('Descripción')" style="width: 60%"></Column>
                    <Column field="dateRegistered" :header="$t('Fecha de Registro')" style="width: 10%"></Column>
                    <Column field="user.userName" :header="$t('Usuario Registró')" style="width: 10%"></Column>
                    <Column :exportable="false" style="width: 10%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="($event: Event)=>{ confirmDeleteBinnacle($event, data.id) }" class="grid-button-text" icon="pi pi-trash" v-tooltip.top="$t('COMMON_BUTTONS.delete')" severity="danger" text rounded raised outlined />
                            </div>
                        </template>
                    </Column>                    
                </DataTable>
            </div>
          </div>
          
          <div class="card mb-3" style="background-color: var(--surface-card); padding: 1rem; width: 100%;">
            <div style="width: 100%;">
              <div class="grid">
                <h2>{{ $t("Asignaciones") }}</h2>

                <div class="flexbox-grid container">
                  <div class="grid">
                    <DataTable :value="caseinvestigationRecord.caseAssignments" 
                      size="small"
                      scrollable
                      scrollHeight="calc(100vh - 300px)"
                      showGridlines 
                      tableStyle="min-width: 50rem"                     
                      ref=""                     
                      paginator 
                      :rows="5"      
                      :rowsPerPageOptions="[5, 10, 25, 50]"
                      v-tooltip.right="caseinvestigationRecord.caseAssignments && caseinvestigationRecord.caseAssignments.length === 0 ? 'No hay asignaciones registradas' : null"
                    >
                      <template #header>
                        <div class="flex corn">
                          <h1>{{ $t('Asignaciones')}}</h1>
                          <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.new')" @click="openFormDialogAssignment()" icon="pi pi-plus" severity="info" aria-label="User" />
                        </div>
                      </template>
                      <template #empty>
                        <div class="p-4 text-center">
                          No hay asignaciones registradas para este caso.
                        </div>
                      </template>
                      <Column field="id" :header="$t('ID')" style="width: 5%"></Column>
                      <Column :header="$t('Usuario Asignado')" style="width: 25%">
                        <template #body="{ data }">
                          {{ data.user && data.user.employee ? data.user.employee.nombre_Completo : 'No disponible' }}
                        </template>
                      </Column>
                      <Column field="observations" :header="$t('Observaciones')" style="width: 40%"></Column>
                      <Column field="dateRegistered" :header="$t('Fecha de Registro')" style="width: 10%">
                        <template #body="{ data }">
                          {{ new Date(data.dateRegistered).toLocaleDateString() }}
                        </template>
                      </Column>
                      <Column field="userNameRegistered" :header="$t('Usuario Registró')" style="width: 10%"></Column>
                      <Column :exportable="false" style="width: 10%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="($event: Event)=>{ confirmDeleteAssignment($event, data.id) }" class="grid-button-text" icon="pi pi-trash" v-tooltip.top="$t('COMMON_BUTTONS.delete')" severity="danger" text rounded raised outlined />
                            </div>
                        </template>
                      </Column>
                    </DataTable>
                  </div>
                </div>


              </div>
            </div>
          </div>



          <div class="card mb-3" style="background-color: var(--surface-card); padding: 1rem; width: 100%;">
            <div style="width: 100%;">
              <div class="grid">
                <div class="flexbox-grid container">
                  <div class="grid">
                    <DataTable :value="caseinvestigationRecord.recoveryHistories" 
                      size="small"
                      scrollable
                      scrollHeight="calc(100vh - 300px)"
                      showGridlines 
                      tableStyle="min-width: 50rem"                     
                      ref=""                     
                      paginator 
                      :rows="5"      
                      :rowsPerPageOptions="[5, 10, 25, 50]"
                    >
                      <template #header>
                        <div class="flex corn">
                          <h1>{{ $t('Recuperaciones')}}</h1>
                          <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.new')" @click="openFormDialogRecovery()" icon="pi pi-plus" severity="info" aria-label="User" />
                        </div>
                      </template>
                      <Column field="id" :header="$t('ID')" style="width: 5%"></Column>
                      <Column field="dateRecovery" :header="$t('Fecha de Recuperación')" style="width: 15%"></Column>
                      <Column field="amountRecovery" :header="$t('Monto Recuperado')" style="width: 15%"></Column>
                      <Column field="observations" :header="$t('Observaciones')" style="width: 30%"></Column>
                      <Column field="dateRegistered" :header="$t('Fecha de Registro')" style="width: 10%"></Column>
                      <Column field="userNameRegistered" :header="$t('Usuario Registró')" style="width: 10%"></Column>
                      <Column :exportable="false" style="width: 10%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="($event: Event)=>{ confirmDeleteRecovery($event, data.id) }" class="grid-button-text" icon="pi pi-trash" v-tooltip.top="$t('COMMON_BUTTONS.delete')" severity="danger" text rounded raised outlined />
                            </div>
                        </template>
                      </Column>
                    </DataTable>
                  </div>
                </div>
              </div>
            </div>
          </div>
          
          <div class="card mb-3" style="background-color: var(--surface-card); padding: 1rem;">

            <div class="flexbox-grid container">
           
  <DataTable :value="caseinvestigationRecord.linkeds" 
                    size="small"
                    scrollable
                    scrollHeight="calc(100vh - 300px)"
                    showGridlines 
                    tableStyle="min-width: 50rem"                     
                    ref=""                     
                    paginator 
                    :rows="5"      
                    :rowsPerPageOptions="[5, 10, 25, 50]"
                >
                    <template #header>
                        <div class="flex corn">
                            <h1>{{ $t('Vinculados')}}</h1>
    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.new')" @click="openFormDialogLinkeds(null)" icon="pi pi-plus" severity="info" aria-label="User"  />
                        </div>
                    </template>
        
                    <Column field="id" :header="$t('ID')" style="width: 10%"></Column>
                    <Column field="linkType.name" :header="$t('Tipo Vinculado')" style="width: 10%"></Column>
                    <Column field="fullName" :header="$t('Vinculado')" style="width: 60%"></Column>
                    <Column field="identification" :header="$t('Identificación')" style="width: 10%"></Column>
                    <Column :exportable="false" style="width: 10%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="($event: Event)=>{ confirmDeleteLinked($event, data.id) }" class="grid-button-text" icon="pi pi-trash" v-tooltip.top="$t('COMMON_BUTTONS.delete')" severity="danger" text rounded raised outlined />
                            </div>
                        </template>
                    </Column>                    
                </DataTable>
            </div>
          </div>
          
          <div class="card mb-3" style="background-color: var(--surface-card); padding: 1rem;">
            
            <div class="flexbox-grid container">
           
  <DataTable :value="caseinvestigationRecord.interviews" 
                    size="small"
                    scrollable
                    scrollHeight="calc(100vh - 300px)"
                    showGridlines 
                    tableStyle="min-width: 50rem"                     
                    ref=""                     
                    paginator 
                    :rows="5"      
                    :rowsPerPageOptions="[5, 10, 25, 50]"
                >
                    <template #header>
                        <div class="flex corn">
                            <h1>{{ $t('Entrevistas')}}</h1>
    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.new')" @click="openFormDialogInterview(null)" icon="pi pi-plus" severity="info" aria-label="User"  />
                        </div>
                    </template>
        
                    <Column field="id" :header="$t('ID')" style="width: 10%"></Column>
                    <Column field="intervieweeType.name" :header="$t('Tipo Entrevistado')" style="width: 10%"></Column>
                    <Column field="linked.fullName" :header="$t('Vinculado')" style="width: 20%"></Column>
                    <Column field="dateInterview" :header="$t('Fecha')" style="width: 10%"></Column>
                    <Column field="description" :header="$t('Descripción')" style="width: 40%"></Column>
                    <Column :exportable="false" style="width: 10%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="($event: Event)=>{ confirmDeleteInterview($event, data.id) }" class="grid-button-text" icon="pi pi-trash" v-tooltip.top="$t('COMMON_BUTTONS.delete')" severity="danger" text rounded raised outlined />
                            </div>
                        </template>
                    </Column>                    
                </DataTable>
            </div>
          </div>
          
          <div class="card mb-3" style="background-color: var(--surface-card); padding: 1rem;">

            <div class="flexbox-grid container">
  <DataTable :value="caseinvestigationRecord.faultLinkeds" 
                    size="small"
                    scrollable
                    scrollHeight="calc(100vh - 300px)"
                    showGridlines 
                    tableStyle="min-width: 50rem"                     
                    ref=""                     
                    paginator 
                    :rows="5"      
                    :rowsPerPageOptions="[5, 10, 25, 50]"
                >
                    <template #header>
                        <div class="flex corn">
                            <h1>{{ $t('Faltas de los Vinculados')}}</h1>
    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.new')" @click="openFormDialogFaultLinked(null)" icon="pi pi-plus" severity="info" aria-label="User"  />
                        </div>
                    </template>
      
                    <Column field="id" :header="$t('ID')" style="width: 5%"></Column>
                    <Column field="linked.fullName" :header="$t('Vinculado')" style="width: 20%"></Column>
                    <Column field="fault.name" :header="$t('Falta')" style="width: 60%"></Column>
                    <Column field="dateRegistered" :header="$t('Fecha de Registro')" style="width: 10%"></Column>
                    <Column :exportable="false" style="width: 5%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="($event: Event)=>{ confirmDeleteFaultLinked($event, data.id) }" class="grid-button-text" icon="pi pi-trash" v-tooltip.top="$t('COMMON_BUTTONS.delete')" severity="danger" text rounded raised outlined />
                            </div>
                        </template>
                    </Column>                    
                </DataTable>
            </div>
          </div>
          
          <div class="card mb-3" style="background-color: var(--surface-card); padding: 1rem;">

            <div class="flexbox-grid container">

  <DataTable :value="caseinvestigationRecord.evidences" 
                    size="small"
                    scrollable
                    scrollHeight="calc(100vh - 300px)"
                    showGridlines 
                    tableStyle="min-width: 50rem"                     
                    ref=""                     
                    paginator 
                    :rows="5"      
                    :rowsPerPageOptions="[5, 10, 25, 50]"
                >
                    <template #header>
                        <div class="flex corn">
                            <h1>{{ $t('')}}</h1>
    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.new')" @click="openFormDialogEvicence(null)" icon="pi pi-plus" severity="info" aria-label="User"  />
                        </div>
                    </template>
        
                    <Column field="id" :header="$t('ID')" style="width: 5%"></Column>
                    <Column field="evidenceClassification.name" :header="$t('Clasificación')" style="width: 15%"></Column>
                    <Column field="name" :header="$t('Nombre')" style="width: 25%"></Column>
                    <Column field="description" :header="$t('Descripción')" style="width: 40%"></Column>
                    <Column field="dateRegistered" :header="$t('Fecha Registro')" style="width: 10%"></Column>
                    <Column :exportable="false" style="width: 5%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="downloadEvicence(data)" class="grid-button-text" icon="pi pi-download" v-tooltip.top="$t('COMMON_BUTTONS.download')" severity="warning" text rounded raised outlined />
                                <PButton @click="($event: Event)=>{ confirmDeleteEvidence($event, data.hash) }" class="grid-button-text" icon="pi pi-trash" v-tooltip.top="$t('COMMON_BUTTONS.delete')" severity="danger" text rounded raised outlined />
                            </div>
                        </template>
                    </Column>                    
                </DataTable>
            </div>
        </div>
      </div>
    </form>

    <PDialog v-model:visible="showDialogRecommendation" :closeOnEscape="false" modal :header="$t('' + (recommendationId === 0 ? $t('COMMON_TITLES.new_record'): $t('COMMON_TITLES.edit_record')) )" :style="{ width: '88vw' }">
        <div>
            <RecommendationForm 
                :recommendationRepository="recommendationRepository" 
                :cloning="isCloningRecord" 
                :recId="recommendationId"
                :caseId="caseinvestigationRecord.id"
                ref="formRefRecommendation" 
            />
        </div>
        <template #footer>            
            <PButton @click="showDialogRecommendation = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="saveRec" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

    <PDialog v-model:visible="showDialogBinnacle" :closeOnEscape="false" modal :header="$t('' + (binnacleId === 0 ? $t('COMMON_TITLES.new_record'): $t('COMMON_TITLES.edit_record')) )" :style="{ width: '88vw' }">
        <div>
            <BinnacleForm 
                :binnacleRepository="binnacleRepository" 
                :cloning="isCloningRecord" 
                :binId="binnacleId"
                :caseId="caseinvestigationRecord.id"
                ref="formRefBinnacle" 
            />
        </div>
        <template #footer>            
            <PButton @click="showDialogBinnacle = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="saveBin" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

    <PDialog v-model:visible="showDialogAddEvidence" :closeOnEscape="false" modal :header="$t('' + (evidenceId === 0 ? $t('COMMON_TITLES.new_record'): $t('COMMON_TITLES.edit_record')) )" :style="{ width: '88vw' }">
        <div>
            <EvidenceForm 
                :caseInvestigation="caseinvestigationRecord"
                ref="formRefEvidence" 
            />
        </div>
        <template #footer>            
            <PButton @click="showDialogAddEvidence = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="saveEvidence" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

    <PDialog v-model:visible="showDialogAddLinkeds" :closeOnEscape="false" modal :header="$t('' + (linkedId === 0 ? $t('COMMON_TITLES.new_record'): $t('COMMON_TITLES.edit_record')) )" :style="{ width: '88vw' }">
        <div>
            <LinkedForm 
              :linkedRepository="linkedRepository"
              :caseInvestigation="caseinvestigationRecord"
              :recordId="linkedId"
              ref="formRefLinked" 
            />
        </div>
        <template #footer>            
            <PButton @click="showDialogAddLinkeds = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="saveLinked" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

    <PDialog v-model:visible="showDialogAddInterview" :closeOnEscape="false" modal :header="$t($t('COMMON_TITLES.new_record'))" :style="{ width: '88vw' }">
        <div>
            <InterviewForm 
              :interviewRepository="interviewRepository"
              :caseInvestigation="caseinvestigationRecord"
              :recordId="interviewId"
              ref="formRefInterview" 
            />
        </div>
        <template #footer>            
            <PButton @click="showDialogAddInterview = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="saveInterview" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

    <PDialog v-model:visible="showDialogAddFaultLinked" :closeOnEscape="false" modal :header="$t($t('COMMON_TITLES.new_record'))" :style="{ width: '88vw' }">
        <div>
            <FaultLinkedForm 
              :caseInvestigation="caseinvestigationRecord"
              ref="formRefFaultLinked" 
            />
        </div>
        <template #footer>            
            <PButton @click="showDialogAddFaultLinked = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="saveFaultLinked" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

    <PDialog v-model:visible="showDialogAssignment" :closeOnEscape="false" modal :header="$t('COMMON_TITLES.new_record')" :style="{ width: '88vw' }">
        <div>
            <div class="flexbox-grid container">
                <div class="grid">
                    <span class="p-float-label">
                        <Dropdown
                            v-model="caseinvestigationRecord.userId"
                            :options="usersOnly.data"
                            filter
                            optionValue="id"
                            optionLabel="employee.nombre_Completo"
                            readonly disabled
                        />
                        <label for="caseinvestigationRecord.userId">{{$t("Usuario Actual")}}</label>
                    </span>

                    <span class="p-float-label">
                        <Dropdown
                            v-model="assignmentRecord.userID"
                            :options="usersOnly.data"
                            filter
                            optionValue="id"
                            optionLabel="employee.nombre_Completo"
                            class="w-full"
                        />
                        <label for="assignmentRecord.userID">{{$t("Usuario a Asignar")}}</label>
                    </span>

                    <span class="p-float-label">
                        <TextArea v-model="assignmentRecord.observations" class="w-full" />
                        <label for="observations">{{ $t("Observaciones") }}</label>
                    </span>
                </div>
            </div>
        </div>
        <template #footer>            
            <PButton @click="showDialogAssignment = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="confirmSaveAssignment($event)" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

    <PDialog v-model:visible="showDialogRecovery" :closeOnEscape="false" modal :header="$t('COMMON_TITLES.new_record')" :style="{ width: '88vw' }">
        <div>
            <div class="flexbox-grid container">
                <div class="grid">
                    <div class="flexbox-grid container">
                        <div class="grid">
                            <span class="p-float-label">
                                <Calendar v-model="recoveryRecord.dateRecovery" dateFormat="dd/mm/yy" />
                                <label for="dateRecovery">{{ $t("Fecha de Recuperación") }}</label>
                            </span>
                        </div>
                        <div class="grid">
                            <span class="p-float-label">
                                <InputNumber :minFractionDigits="2" :maxFractionDigits="2" v-model="recoveryRecord.amountRecovery" />
                                <label for="amountRecovery">{{$t("Monto Recuperado")}}</label>
                            </span>
                        </div>
                    </div>

                    <div class="flexbox-grid container">
                        <div class="grid">
                            <span class="p-float-label">
                                <TextArea v-model="recoveryRecord.observations" />
                                <label for="observations">{{ $t("Observaciones") }}</label>
                            </span>

                            <span class="p-float-label">
                                <InputText v-model="recoveryRecord.caseID" type="hidden" />
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <template #footer>            
            <PButton @click="showDialogRecovery = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="confirmSaveAmountRecovery($event)" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>
  </div>
</template>

<style scoped>
</style>