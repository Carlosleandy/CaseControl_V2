<script setup lang="ts">
import { ref } from "vue";
import type { CaseInvestigation, CaseAssignment } from "../../domain/model/caseinvestigation"
import { useDataCases } from "../../repository/caseinvestigationRepository"
import { useDataUsers } from "../../../User/repository/userRepository"
import { useConfirm } from 'primevue/useconfirm'

const confirm = useConfirm();

const formRefAssignment = ref();

const assignmentRecord = ref({} as CaseAssignment);

const { usersOnly, getAllUserOnly } = useDataUsers();
const { addCaseAssignment } = useDataCases();

const props = defineProps<{
  caseInvestigation?: CaseInvestigation;
}>();

const loadInitialData = () => {
  if (props?.caseInvestigation) {
   assignmentRecord.value.caseID = props.caseInvestigation.id;
  }

getAllUserOnly();
};

const save = async () => {
  if(assignmentRecord) {
    await addCaseAssignment(assignmentRecord.value);
        }
    }

loadInitialData();

defineExpose({ saveAssignment: save });
</script>

<template>
  <form ref="formRef">
      <div class="flexbox-grid container">
    <div class="card" style="--min: 50ch">
        <div class="grid">
        
      <h2>{{ $t("SHARED.basic_information") }}</h2>

      <div class="flexbox-grid container">
        <div class="grid">
         
        </div>
        <div class="grid">
          
        </div>
      </div>
      
      <div class="flexbox-grid container">
        <div class="grid">
           <span class="p-float-label">
            <Dropdown
              v-model="props.caseInvestigation.userID"
              :options="usersOnly.data"
              filter
              optionValue="id"
              optionLabel="employee.nombre_Completo"
              readonly disabled
            />
            <label for="assignmentRecord.userID">{{$t("Usuario Actual")}}</label>
          </span>

          <span class="p-float-label">
            <Dropdown
              v-model="assignmentRecord.userID"
              :options="usersOnly.data"
              filter
              optionValue="id"
              optionLabel="employee.nombre_Completo"
              readonly 
            />
            <label for="assignmentRecord.userID">{{$t("Usuario a Asignar")}}</label>
          </span>

          <span class="p-float-label">
            <TextArea v-model="assignmentRecord.observations" />
            <label for="observations">{{ $t("Observaciones") }}</label>
          </span>

          <span class="p-float-label">
            <InputText v-model="assignmentRecord.caseID" type="hidden" />
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