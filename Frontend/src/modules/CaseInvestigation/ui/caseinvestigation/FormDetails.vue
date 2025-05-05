<script setup lang="ts">
import type { CaseInvestigationRepository } from "../../domain/repository/caseinvestigationRepository";
import type { RecommendationRepository } from "../../../Recommendation/domain/repository/recommendationRepository"
import type { BinnacleRepository } from "../../../Binnacle/domain/repository/binnacleRepository"
import { useCaseInvestigation } from "../../composable/caseinvestigationComposable";

import { useDataCaseStatus } from "../../../CaseStatus/repository/casestatusRepository";
import { useDataCaseType } from "../../../CaseType/repository/casetypeRepository";
import { useDataReceptionMedium } from "../../../ReceptionMedium/repository/receptionmediumRepository";
import { ref } from "vue";
import RecommendationForm from '../../../Recommendation/ui/recommendation/Form.vue'
import BinnacleForm from '../../../Binnacle/ui/binnacle/Form.vue'
import type { Recommendation } from '../../../Recommendation/domain/model/recommendation'
import type { Binnacle } from '../../../Binnacle/domain/model/binnacle'
import { useRecommendation } from "../../../Recommendation/composable/recommendationComposable";
import { useBinnacle } from "../../../Binnacle/composable/binnacleComposable";
import { useConfirm } from 'primevue/useconfirm'
import { useDataRecommendation } from "../../../Recommendation/repository/recommendationRepository";
import { useDataBinnacle } from "../../../Binnacle/repository/binnacleRepository"
import { useAllOffices } from "../../../shared/utility/office"

const confirm = useConfirm();

const recId = ref(0);
const binId = ref(0);
const isCloningRecord = ref(false);
const formRefRecommendation = ref();
const formRefBinnacle = ref();

const { caseStatusList, getAllCaseStatus } = useDataCaseStatus();

const { caseTypeList, getAllCaseTypes } = useDataCaseType();

const { allOffices, getAllOffices } = useAllOffices();

const { receptionMediumList, getAllReceptionMediums } =
  useDataReceptionMedium();

const { deleteRecommendation } = useDataRecommendation();
const { deleteBinnacle } = useDataBinnacle();

let recommendationId = ref(0);
let binnacleId = ref(0);

const props = defineProps<{
  caseinvestigationRepository: CaseInvestigationRepository;
  recommendationRepository: RecommendationRepository;
  binnacleRepository: BinnacleRepository;
  recordId?: number;
  cloning?: boolean;
}>();

const {
  caseinvestigationRecord,
  getCaseInvestigationById,
  setCaseInvestigationData,
} = useCaseInvestigation({
  caseinvestigationRepository: props.caseinvestigationRepository,
});

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

const loadInitialData = () => {
  if (props?.recordId) {
    getCaseInvestigationById(props.recordId).then((caseinvestigation) => {
      if (caseinvestigation) {
        if (props.cloning) {
          caseinvestigation.id = 0;
        }

        setCaseInvestigationData(caseinvestigation);
      }
    });
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

async function saveRec(): Promise<void> {
  savingRecordRecommendation.value = true;        
  const result = await formRefRecommendation.value.saveRec();
  savingRecordRecommendation.value = false;   
  loadInitialData();   
  showDialogRecommendation.value = false;
}

async function saveBin(): Promise<void> {
  savingRecordBinnacle.value = true;        
  const result = await formRefBinnacle.value.saveBin();
  savingRecordBinnacle.value = false;   
  loadInitialData();   
  showDialogBinnacle.value = false;
}

async function deleteRecommendationById(id: number): Promise<void> {
        const result = await deleteRecommendation(id);
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

loadInitialData();
getAllCaseTypes();
getAllReceptionMediums();
getAllCaseStatus();
getAllOffices();

</script>

<template>
  <form ref="formRef">
      <div class="flexbox-grid container">
    <div class="card" style="--min: 50ch">
        <div class="grid">
        
      <h2>{{ $t("SHARED.basic_information") }}</h2>

      <div class="flexbox-grid container">
        <div class="grid">
          <span class="p-float-label">
            <InputNumber v-model="caseinvestigationRecord.id" readonly />
            <label for="id">{{ $t("ID") }}</label>
          </span>
        </div>
        <div class="grid">
          <span class="p-float-label">
            <InputText v-model="caseinvestigationRecord.communicationNumber" readonly />
            <label for="communicationNumber">{{$t("No. Comunicación")}}</label>
          </span>
        </div>
      </div>

      <div class="flexbox-grid container">
        <div class="grid">
          <span class="p-float-label">
            <InputText v-model="caseinvestigationRecord.subject" readonly />
            <label for="subject">{{ $t("Asunto") }}</label>
          </span>
        </div>
      </div>

      <div class="flexbox-grid container">
        <div class="grid">
          <span class="p-float-label">
            <InputText v-model="caseinvestigationRecord.transmitter" readonly />
            <label for="transmitter">{{ $t("Emisor Comunicación") }}</label>
          </span>
          <span class="p-float-label">
            <InputText v-model="caseinvestigationRecord.recipient" readonly />
            <label for="recipient">{{ $t("Destinatario Comunicación") }}</label>
          </span>
          <span class="p-float-label">
            <Calendar v-model="caseinvestigationRecord.dateOfCommunication" dateFormat="dd/mm/yy" readonly />
            <label for="dateOfCommunication">{{$t("Fecha de Comunicación")}}</label>
          </span>
          <span class="p-float-label">
            <Calendar v-model="caseinvestigationRecord.dateOfReceipt" dateFormat="dd/mm/yy" readonly />
            <label for="dateOfReceipt">{{ $t("Fecha Recibida") }}</label>
          </span>
          <span class="p-float-label">
            <Dropdown
              v-model="caseinvestigationRecord.branch"
              :options="allOffices.data"
              optionValue="oficina_Parsed"
              optionLabel="oficina_Completa"
              filter
              readonly disabled
            />
            <label for="caseinvestigationRecord.branch">{{$t("Oficina")}}</label>
          </span>
        </div>
        <div class="grid">
          <span class="p-float-label">
            <InputNumber :minFractionDigits="2" :maxFractionDigits="2" v-model="caseinvestigationRecord.amountDetected" readonly />
            <label for="amountDetected">{{ $t("Monto Detectado") }}</label>
          </span>
          <span class="p-float-label">
            <InputNumber :minFractionDigits="2" :maxFractionDigits="2" v-model="caseinvestigationRecord.amountInvestigated" readonly />
            <label for="amountInvestigated">{{$t("Monto Investigado")}}</label>
          </span>
          <span class="p-float-label">
            <InputNumber :minFractionDigits="2" :maxFractionDigits="2" v-model="caseinvestigationRecord.amountRecovered" readonly />
            <label for="amountRecovered">{{ $t("Monto Recuperado") }}</label>
          </span>
          <span class="p-float-label">
            <InputNumber :minFractionDigits="2" :maxFractionDigits="2" v-model="caseinvestigationRecord.amountLost" readonly />
            <label for="amountLost">{{ $t("Monto Pérdida") }}</label>
          </span>
          <span class="p-float-label">
            <InputText v-model="caseinvestigationRecord.affectedAccount" readonly />
            <label for="affectedAccount">{{ $t("Cuenta Afectada") }}</label>
          </span>
        </div>
      </div>

      <div class="flexbox-grid container">
        <div class="grid">
          <span class="p-float-label">
            <Dropdown
              v-model="caseinvestigationRecord.caseTypeID"
              :options="caseTypeList.data"
              filter
              optionValue="id"
              optionLabel="name"
              readonly disabled
            />
            <label for="caseinvestigationRecord.caseTypeId">{{$t("Tipo Caso")}}</label>
          </span>
          <span class="p-float-label">
            <Dropdown
              v-model="caseinvestigationRecord.receptionMediumID"
              :options="receptionMediumList.data"
              filter
              optionValue="id"
              optionLabel="name"
              readonly disabled
            />
            <label for="caseinvestigationRecord.receptionMediumId">{{$t("Medio Recepción")}}</label>
          </span>

          <span class="p-float-label">
            <Dropdown
              v-model="caseinvestigationRecord.caseStatusID"
              :options="caseStatusList.data"
              optionValue="id"
              optionLabel="name"
              filter
              readonly disabled
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
              readonly 
            />
            <label for="communicationReference">{{$t("Referencia Comunicación")}}</label>
          </span>
        </div>
      </div>
    </div>


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

</template>

<style scoped>
</style>