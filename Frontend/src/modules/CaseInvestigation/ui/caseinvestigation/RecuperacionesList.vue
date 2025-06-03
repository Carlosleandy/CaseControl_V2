<script setup lang="ts">
import { ref, defineProps, defineEmits, reactive, computed } from 'vue';
import { useConfirm } from 'primevue/useconfirm';
import { useToast } from 'primevue/usetoast';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import Button from 'primevue/button';
import PButton from 'primevue/button';
import Dialog from 'primevue/dialog';
import PDialog from 'primevue/dialog';
import Calendar from 'primevue/calendar';
import InputNumber from 'primevue/inputnumber';
import Textarea from 'primevue/textarea';
import TextArea from 'primevue/textarea';
import InputText from 'primevue/inputtext';
import type { RecoveryHistory } from "../../domain/model/caseinvestigation";
import { useDataCases } from "../../repository/caseinvestigationRepository";

import {formatDate} from "../../../shared/utility/dateUtils";

const props = defineProps<{
  caseinvestigationRecord: any;
  caseinvestigationRepository: any;
}>();

// Get the current user ID from the case investigation record
const getCurrentUserId = (): number => {
  // If the case investigation record has a userId property, use that
  if (props.caseinvestigationRecord && props.caseinvestigationRecord.userId) {
    return props.caseinvestigationRecord.userId;
  }
  // Otherwise return a default value or 0
  return 0;
};

const emit = defineEmits(['refreshData']);

const confirm = useConfirm();
const toast = useToast();
const { addCaseAmountRecovery } = useDataCases();

const showDialogRecovery = ref(false);
const recoveryRecord = ref<RecoveryHistory>({} as RecoveryHistory);

// Create a reactive copy of the recovery histories for better reactivity
const recoveryHistories = computed(() => {
  return props.caseinvestigationRecord?.recoveryHistories || [];
});

const openFormDialogRecovery = () => {
  recoveryRecord.value = {} as RecoveryHistory;
  recoveryRecord.value.caseID = props.caseinvestigationRecord.id;
  recoveryRecord.value.userID = getCurrentUserId();
  showDialogRecovery.value = true;
};

async function deleteRecoveryById(id: number): Promise<void> {
  try {
    const result = await props.caseinvestigationRepository.deleteRecovery(id);
    if (result) {
      emit('refreshData');
    }
  } catch (error) {
    console.error('Error deleting recovery:', error);
  }
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

function confirmSaveAmountRecovery(event: any) {
  saveAmountRecovery();
}

const saveAmountRecovery = async () => {
  if(recoveryRecord) {
    // Ensure caseID and userID are set correctly
    recoveryRecord.value.caseID = props.caseinvestigationRecord.id;
    recoveryRecord.value.userID = getCurrentUserId();
    
    // Validate required fields
    if (!recoveryRecord.value.dateRecovery) {
      toast.add({ severity: 'error', summary: 'Error', detail: 'La fecha de recuperación es requerida', life: 3000 });
      return;
    }
    
    if (!recoveryRecord.value.amountRecovery) {
      toast.add({ severity: 'error', summary: 'Error', detail: 'El monto recuperado es requerido', life: 3000 });
      return;
    }
    
    try {
      // Log the data being sent
      console.log('Saving recovery record:', recoveryRecord.value);
      
      // Call the repository method directly from useDataCases
      const result = await addCaseAmountRecovery(recoveryRecord.value);
      console.log('Save result:', result);
      
      // Close dialog
      showDialogRecovery.value = false;
      
      // Important: Emit the refreshData event to tell the parent Form.vue to reload the data
      // This will trigger loadInitialData() in the parent component
      emit('refreshData');
      
      // Reload the entire case investigation data to ensure all related data is refreshed
      // This is handled by the parent Form.vue component through the refreshData event
    } catch (error) {
      console.error('Error saving recovery:', error);
      toast.add({ severity: 'error', summary: 'Error', detail: 'Error al guardar la recuperación', life: 3000 });
    }
  }
};
</script>

<template>
  <div style="width: 100%;">
    <div class="grid">
      <div class="flexbox-grid container">
        <div class="grid">
          <DataTable :value="recoveryHistories" 
            size="small"
            scrollable
            scrollHeight="calc(100vh - 300px)"
            showGridlines 
            tableStyle="min-width: 50rem"                     
            ref="dt"                     
            paginator 
            :rows="10"      
            :rowsPerPageOptions="[10, 15, 25, 50]"
          >
            <template #header>
              <div class="flex corn">
                <h1>{{ $t('Recuperaciones')}}</h1>
                <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.new')" @click="openFormDialogRecovery()" icon="pi pi-plus" severity="info" aria-label="User" />
              </div>
            </template>
            <Column field="dateRecovery" :header="$t('Fecha de Recuperación')" style="width: 15%">
              <template #body="{ data }">
                            {{ formatDate(data.dateRecovery) }}
                        </template>
            </Column>
            <Column field="amountRecovery" :header="$t('Monto Recuperado')" style="width: 15%"></Column>
            <Column field="observations" :header="$t('Observaciones')" style="width: 30%"></Column>
            <Column field="dateRegistered" :header="$t('Fecha de Registro')" style="width: 10%">
              <template #body="{ data }">
                            {{ formatDate(data.dateRegistered) }}
                        </template>
            </Column>
            <Column field="user.userName" :header="$t('Usuario Registró')" style="width: 10%"> </Column>
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


  <!-- Recovery Form Dialog -->
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

                            <!-- Hidden field for caseID, no need for visual representation -->
                                <input type="hidden" :value="recoveryRecord.caseID" />
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
</template>