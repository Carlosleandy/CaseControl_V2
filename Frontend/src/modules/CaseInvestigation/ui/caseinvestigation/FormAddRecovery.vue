<script setup lang="ts">
import { ref } from "vue";
import type { CaseInvestigation, RecoveryHistory } from "../../domain/model/caseinvestigation"
import { useDataCases } from "../../repository/caseinvestigationRepository"

const formRefRecovery = ref();

const recoveryRecord = ref({} as RecoveryHistory);

const { addCaseAmountRecovery } = useDataCases();

const props = defineProps<{
  caseInvestigation?: CaseInvestigation;
}>();

const loadInitialData = () => {
  if (props?.caseInvestigation) {
   recoveryRecord.value.caseID = props.caseInvestigation.id;
  }
};

const save = async () => {
        if(recoveryRecord) {
            await addCaseAmountRecovery(recoveryRecord.value);
        }
    }

loadInitialData();

defineExpose({ saveRecovery: save });
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
  </form>
</template>

<style scoped>
</style>