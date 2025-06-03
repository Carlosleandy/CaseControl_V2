<script setup lang="ts">
import { ref } from 'vue';
import { Case } from '../../domain/entity/case';
import { CaseInvestigationRepository } from '../../domain/repository/caseinvestigationRepository';
import { Binnacle } from '../../domain/entity/binnacle';
import { useToast } from 'primevue/usetoast';
import Dialog from 'primevue/dialog';
import Button from 'primevue/button';
import Textarea from 'primevue/textarea';
import { useDataBinnacle } from '../../../Binnacle/repository/binnacleRepository';
import { formatDate } from "../../../shared/utility/dateUtils";


const toast = useToast();
const dataTableBinnacle = ref();
const binId = ref(0);
const showDialogBinnacle = ref(false);
const isCloningRecord = ref(false);
const binnacleId = ref(0);
const binnacleDescription = ref('');

// Obtener las funciones del repositorio de bitácoras
const { binnacleSave: addBinnacle, deleteBinnacle } = useDataBinnacle();

const props = defineProps<{
  caseinvestigationRecord: Case;
  caseinvestigationRepository: CaseInvestigationRepository;
}>();

const emit = defineEmits(['binnacleAdded', 'binnacleDeleted']);

// Función para abrir el diálogo de bitácora en el boton de nuevo
function handleOpenDialog(binnacleRecord: Binnacle | null, isCloning = false) {
  if (binnacleRecord === null) {
    binId.value = 0;
    binnacleDescription.value = '';
  } else if (typeof binnacleRecord === 'object') {
    binId.value = binnacleRecord.id;
    binnacleDescription.value = binnacleRecord.name;
  }
  
  showDialogBinnacle.value = true;
  isCloningRecord.value = isCloning;
}

// Función para abrir el diálogo de bitácora en el boton de editar
const openFormDialogBinnacle = (binnacleRecord: Binnacle | null, isCloning = false) => {
  if (binnacleRecord) {
    // Load the data from the selected binnacle record
    binId.value = binnacleRecord.id;
    binnacleDescription.value = binnacleRecord.name || '';
    binnacleId.value = binnacleRecord.id;
  } else {
    // Reset form for new record
    binId.value = 0;
    binnacleDescription.value = '';
    binnacleId.value = 0;
  }
  
  showDialogBinnacle.value = true;
  isCloningRecord.value = isCloning;
}

// Función para guardar la bitácora
async function saveBinnacle() {
  try {
    if (!binnacleDescription.value.trim()) {
      toast.add({ severity: 'error', summary: 'Error', detail: 'La descripción es requerida', life: 3000 });
      return;
    }
    
    // Crear objeto de bitácora para guardar en la base de datos
    const binnacle: Partial<Binnacle> = {
      id: binId.value,
      name: binnacleDescription.value, // El campo en el modelo es 'name'
      caseID: props.caseinvestigationRecord.id, // Nota: el campo en el modelo es 'caseID' (con ID en mayúsculas)
      userID: 1 // Este valor debería venir del usuario logueado
    };
    
    // Guardar la bitácora en la base de datos usando el repositorio
    const savedBinnacle = await addBinnacle(binnacle as Binnacle);
    
    // Si estamos editando una bitácora existente
    if (binId.value > 0) {
      // Actualizar la bitácora existente en el array
      if (props.caseinvestigationRecord.binnacles) {
        const index = props.caseinvestigationRecord.binnacles.findIndex(b => b.id === binId.value);
        if (index !== -1) {
          // Actualizar el registro existente
          props.caseinvestigationRecord.binnacles[index] = {
            ...props.caseinvestigationRecord.binnacles[index],
            name: binnacleDescription.value,
            dateRegistered: savedBinnacle.dateRegistered || props.caseinvestigationRecord.binnacles[index].dateRegistered
          };
        }
      }
    } else {
      // Es un nuevo registro
      const newBinnacle = {
        id: savedBinnacle.id,
        name: binnacleDescription.value,
        caseID: props.caseinvestigationRecord.id,
        dateRegistered: savedBinnacle.dateRegistered || new Date().toISOString(),
        user: savedBinnacle.user || { userName: 'Usuario actual' }
      } as Binnacle;
      
      // Inicializar el array de bitácoras si no existe
      if (!props.caseinvestigationRecord.binnacles) {
        props.caseinvestigationRecord.binnacles = [];
      }
      
      // Añadir la nueva bitácora al array
      props.caseinvestigationRecord.binnacles.push(newBinnacle);
    }
    
    // Ordenar el array por ID (ascendente)
    props.caseinvestigationRecord.binnacles.sort((a, b) => a.id - b.id);
    
    // Emitir evento para que el componente padre actualice los datos si es necesario
    emit('binnacleAdded', savedBinnacle);
    
    // Cerrar el diálogo y limpiar el formulario
    showDialogBinnacle.value = false;
    binnacleDescription.value = '';
    binId.value = 0;
    binnacleId.value = 0;
    
  } catch (error) {
    console.error('Error al guardar la bitácora:', error);
    toast.add({ severity: 'error', summary: 'Error', detail: 'Error al guardar la bitácora', life: 3000 });
  }
}

// Función para confirmar eliminación de bitácora
async function handleDeleteBinnacle(event: Event, id: number) {
  if (confirm('¿Está seguro que desea eliminar esta bitácora?')) {
    try {
      // Eliminar la bitácora de la base de datos
      await deleteBinnacle(id);
      
      // Actualizar la lista de bitácoras en la UI
      if (props.caseinvestigationRecord.binnacles) {
        props.caseinvestigationRecord.binnacles = props.caseinvestigationRecord.binnacles.filter(b => b.id !== id);
      }
      
      // Emitir evento para que el componente padre actualice los datos si es necesario
      emit('binnacleDeleted', id);
      
      toast.add({ severity: 'success', summary: 'Éxito', detail: 'Bitácora eliminada correctamente', life: 3000 });
    } catch (error) {
      console.error('Error al eliminar la bitácora:', error);
      toast.add({ severity: 'error', summary: 'Error', detail: 'Error al eliminar la bitácora', life: 3000 });
    }
  }
}

// Inicializar datos
function loadInitialData() {
  if (props?.caseinvestigationRecord) {
    dataTableBinnacle.value?.reset();
  }
}

loadInitialData();
</script>

<template>
  <!-- BINNACLES -->
  <div class="flexbox-grid container">
    <DataTable 
      ref="dataTableBinnacle" 
      :value="caseinvestigationRecord.binnacles" 
      size="small"
      scrollable
      scrollHeight="calc(100vh - 300px)"
      showGridlines 
      tableStyle="min-width: 50rem"                     
      paginator 
      :rows="5"      
      :rowsPerPageOptions="[5, 10, 25, 50]"
    >
      <template #header>
        <div class="flex corn">
          <h1>{{ $t('Bitácora')}}</h1>
          <PButton 
            v-tooltip.bottom="$t('COMMON_BUTTONS.new')" 
            @click="handleOpenDialog(null)" 
            icon="pi pi-plus" 
            severity="info" 
            aria-label="User" 
          />
        </div>
      </template>

      
      <Column field="name" :header="$t('Descripción')" style="width: 60%"></Column>
      <Column field="dateRegistered" :header="$t('Fecha de Registro')" style="width: 10%">
        <template #body="{ data }">
                            {{ formatDate(data.dateRegistered) }}
                        </template>
      </Column>
      <Column field="user.userName" :header="$t('Usuario Registró')" style="width: 10%"></Column>
      <Column :exportable="false" style="width: 10%">
        <template #body="{ data }">
          <div class="grid-actions-container">
            <PButton 
            @click="openFormDialogBinnacle(data)" 
            class="grid-button-text" 
            icon="pi pi-pencil" 
            v-tooltip.top="$t('COMMON_BUTTONS.editStatus')" 
            text 
            rounded 
            raised 
            outlined 
            />
            <PButton 
              @click="($event: Event) => handleDeleteBinnacle($event, data.id)" 
              class="grid-button-text" 
              icon="pi pi-trash" 
              v-tooltip.top="$t('COMMON_BUTTONS.delete')" 
              severity="danger" 
              text 
              rounded 
              raised 
              outlined 
            />
          </div>
        </template>
      </Column>                    
    </DataTable>
  </div>

  <!-- Diálogo para agregar/editar bitácora -->
  <Dialog 
    v-model:visible="showDialogBinnacle" 
    :style="{width: '450px'}" 
    :header="binId ? $t('Editar Bitácora') : $t('Nueva Bitácora')" 
    :modal="true" 
    class="p-fluid"
  >
    <div class="field">
      <label for="description">{{ $t('Descripción') }}</label>
      <Textarea 
        id="description" 
        v-model="binnacleDescription" 
        required="true" 
        rows="3" 
        class="p-inputtext-sm" 
      />
    </div>
    <template #footer>
      <PButton 
        :label="$t('COMMON_BUTTONS.cancel')" 
        icon="pi pi-times" 
        severity="warning" 
        @click="showDialogBinnacle = false" 
        style="background-color: #F9A03F; border-color: #F9A03F;" 
      />
      <PButton 
        :label="$t('COMMON_BUTTONS.save')" 
        icon="pi pi-check" 
        severity="info" 
        @click="saveBinnacle" 
        style="background-color: #2DAAED; border-color: #2DAAED;" 
      />
    </template>
  </Dialog>
</template>