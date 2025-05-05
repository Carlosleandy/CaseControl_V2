<script setup lang="ts">
import type { CaseInvestigationRepository } from "../../domain/repository/caseinvestigationRepository";
import { useCaseInvestigation } from "../../composable/caseinvestigationComposable";
import type { CaseInvestigation, CaseStatusChange } from "../../domain/model/caseinvestigation";
import { useDataCaseStatus } from "../../../CaseStatus/repository/casestatusRepository";
import { useDataCases } from '../../repository/caseinvestigationRepository'

import { ref } from "vue";
import { useConfirm } from 'primevue/useconfirm'

const confirm = useConfirm();

const recId = ref(0);
const binId = ref(0);
const isCloningRecord = ref(false);
const formRefRecommendation = ref();
const formRefBinnacle = ref();

const caseStatusChange = ref({} as CaseStatusChange);


const props = defineProps<{
  caseinvestigationRepository: CaseInvestigationRepository;
  caseInvestigation: CaseInvestigation;
  recordId?: number;
  cloning?: boolean;
}>();

  const { caseStatusList, getAllCaseStatus } = useDataCaseStatus();

  const { addCaseStatusChange } = useDataCases();
  

const {
  caseinvestigationRecord,
  getCaseInvestigationById,
  setCaseInvestigationData,
  saveCaseInvestigation,
} = useCaseInvestigation({
  caseinvestigationRepository: props.caseinvestigationRepository,
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

 const save = async () => {
   debugger;
    if(caseinvestigationRecord) {
      //await saveCaseInvestigation();
      caseStatusChange.value.caseID = caseinvestigationRecord.value.id;
      caseStatusChange.value.caseStatusID = caseinvestigationRecord.value.caseStatusID;
      await addCaseStatusChange(caseStatusChange.value);
    }
  }


loadInitialData();
getAllCaseStatus();

defineExpose({ saveStatus: save });

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
          
        </div>
      </div>

      <div class="flexbox-grid container">
        <div class="grid">
          <span class="p-float-label">
            <InputText v-model="caseinvestigationRecord.subject" readonly />
            <label for="subject">{{ $t("Asunto") }}</label>
          </span>
          <span class="p-float-label">
            <Dropdown
              v-model="caseinvestigationRecord.caseStatusID"
              :options="caseStatusList.data"
              optionValue="id"
              optionLabel="name"
              filter
              readonly 
            />
            <label for="caseinvestigationRecord.caseStatusId">{{$t("Estado")}}</label>
          </span>
        </div>
      </div>

    </div>


    </div>
    </div>
  </form>

</template>

<style scoped>
</style>