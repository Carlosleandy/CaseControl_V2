<script setup lang="ts">
import { ref, computed } from 'vue';
import { Case } from '../../domain/entity/case';
import { CaseInvestigationRepository } from '../../domain/repository/caseinvestigationRepository';
import type { CaseAssignment } from '../../../CaseAssignment/domain/model/caseassignment';
import { useToast } from 'primevue/usetoast';
import Dialog from 'primevue/dialog';
import Button from 'primevue/button';
import Textarea from 'primevue/textarea';
import { useDataCaseAssignment } from '../../../CaseAssignment/repository/caseassignmentRepository';
import { formatDate } from "../../../shared/utility/dateUtils";


const toast = useToast();
const dataTableAssignment = ref();
const assignmentId = ref(0);
const showDialogAssignment = ref(false);
const isCloningRecord = ref(false);
const assignmentDescription = ref('');

// Obtener las funciones del repositorio de asignaciones
const { caseassignmentSave: addAssignment, deleteCaseAssignment: deleteAssignment } = useDataCaseAssignment();

const props = defineProps<{
  caseinvestigationRecord: Case;
  caseinvestigationRepository: CaseInvestigationRepository;
}>();

const emit = defineEmits(['assignmentAdded', 'assignmentDeleted']);

// Function to get the current user ID
const getCurrentUserId = (): number => {
  if (props.caseinvestigationRecord && props.caseinvestigationRecord.userID) {
    return props.caseinvestigationRecord.userID;
  }
  return 1; // Default user ID if none is found
};

// Get user for dropdown selection
const usersOnly = computed(() => {
  if (props.caseinvestigationRecord?.user) {
    return [props.caseinvestigationRecord.user];
  }
  return [];
});

// Función para abrir el diálogo de bitácora en el boton de nuevo
function handleOpenDialog(assignmentRecord: CaseAssignment | null, isCloning = false) {
  if (assignmentRecord === null) {
    assignmentId.value = 0;
    assignmentDescription.value = '';
  } else if (typeof assignmentRecord === 'object') {
    assignmentId.value = assignmentRecord.id;
    assignmentDescription.value = assignmentRecord.observations || '';
  }
  
  showDialogAssignment.value = true;
  isCloningRecord.value = isCloning;
}

// Función para abrir el diálogo de bitácora en el boton de editar
const openFormDialogAssignment = (assignmentRecord: CaseAssignment | null, isCloning = false) => {
  if (assignmentRecord) {
    // Load the data from the selected assignment record
    assignmentId.value = assignmentRecord.id;
    assignmentDescription.value = assignmentRecord.observations || '';
    assignmentId.value = assignmentRecord.id;
  } else {
    // Reset form for new record
    assignmentId.value = 0;
    assignmentDescription.value = '';
    assignmentId.value = 0;
  }
  
  showDialogAssignment.value = true;
  isCloningRecord.value = isCloning;
}

// Función para guardar la bitácora
async function saveAssignment() {
  try {
    if (!assignmentDescription.value.trim()) {
      toast.add({ severity: 'error', summary: 'Error', detail: 'La descripción es requerida', life: 3000 });
      return;
    }
    
    // Crear objeto de bitácora para guardar en la base de datos
    const assignment: Partial<CaseAssignment> = {
      id: assignmentId.value,
      observations: assignmentDescription.value, // El campo en el modelo es 'observations'
      caseID: props.caseinvestigationRecord.id, // Nota: el campo en el modelo es 'caseID' (con ID en mayúsculas)
      userID: 1 // Este valor debería venir del usuario logueado
    };
    
    // Guardar la bitácora en la base de datos usando el repositorio
    const savedAssignment = await addAssignment(assignment as CaseAssignment);
    
    // Si estamos editando una bitácora existente
    if (assignmentId.value > 0) {
      // Actualizar la bitácora existente en el array
      if (props.caseinvestigationRecord.assignments) {
        const index = props.caseinvestigationRecord.assignments.findIndex(b => b.id === assignmentId.value);
        if (index !== -1) {
          // Actualizar el registro existente
          props.caseinvestigationRecord.assignments[index] = {
            ...props.caseinvestigationRecord.assignments[index],
            observations: assignmentDescription.value,
            dateRegistered: savedAssignment.dateRegistered || props.caseinvestigationRecord.assignments[index].dateRegistered
          };
        }
      }
    } else {
      // Es un nuevo registro
      const newAssignment = {
        id: savedAssignment.id,
        observations: assignmentDescription.value,
        caseID: props.caseinvestigationRecord.id,
        dateRegistered: savedAssignment.dateRegistered || new Date().toISOString(),
        user: savedAssignment.user || { userName: 'Usuario actual' }
      } as CaseAssignment;
      
      // Inicializar el array de bitácoras si no existe
      if (!props.caseinvestigationRecord.assignments) {
        props.caseinvestigationRecord.assignments = [];
      }
      
      // Añadir la nueva bitácora al array
      props.caseinvestigationRecord.assignments.push(newAssignment);
    }
    
    // Ordenar el array por ID (ascendente)
    props.caseinvestigationRecord.assignments.sort((a, b) => a.id - b.id);
    
    // Emitir evento para que el componente padre actualice los datos si es necesario
    emit('assignmentAdded', savedAssignment);
    
    // Cerrar el diálogo y limpiar el formulario
    showDialogAssignment.value = false;
    assignmentDescription.value = '';
    assignmentId.value = 0;
    
  } catch (error) {
    console.error('Error al guardar la bitácora:', error);
    toast.add({ severity: 'error', summary: 'Error', detail: 'Error al guardar la bitácora', life: 3000 });
  }
}

// Función para confirmar eliminación de bitácora
async function handleDeleteAssignment(event: Event, id: number) {
  if (confirm('¿Está seguro que desea eliminar esta bitácora?')) {
    try {
      // Eliminar la bitácora de la base de datos
      await deleteAssignment(id);
      
      // Actualizar la lista de bitácoras en la UI
      if (props.caseinvestigationRecord.assignments) {
        props.caseinvestigationRecord.assignments = props.caseinvestigationRecord.assignments.filter(b => b.id !== id);
      }
      
      // Emitir evento para que el componente padre actualice los datos si es necesario
      emit('assignmentDeleted', id);
      
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
    // No need to call reset() as it doesn't exist on DataTable
    // Just ensure the component is properly initialized
  }
}

loadInitialData();
</script>

<template>
  <!-- ASSIGNMENTS -->
  <div class="flexbox-grid container">
    <DataTable 
      ref="dataTableAssignment" 
      :value="props.caseinvestigationRecord?.assignments || []" 
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
          <h1>{{ $t('Asignaciones')}}</h1>
          <PButton 
            v-tooltip.bottom="$t('COMMON_BUTTONS.new')" 
            @click="handleOpenDialog(null)" 
            icon="pi pi-plus" 
            severity="info" 
            aria-label="User" 
          />
        </div>
      </template>

      <template #empty>
        <div class="p-4 text-center">
          No hay asignaciones registradas para este caso.
        </div>
      </template>

      <Column :header="$t('Usuario Asignado')" style="width: 25%">
        <template #body="{ data }">
          {{ data && data.user ? data.user.userName : 'No disponible' }}
        </template>
      </Column>
      <Column field="observations" :header="$t('Observaciones')" style="width: 40%"></Column>
      <Column field="dateRegistered" :header="$t('Fecha de Registro')" style="width: 10%">
        <template #body="{ data }">
          {{ formatDate(data.dateRegistered) }}
        </template>
      </Column>
      <Column field="userNameRegistered" :header="$t('Usuario Registró')" style="width: 10%"></Column>
      <Column :exportable="false" style="width: 10%">
        <template #body="{ data }">
          <div class="grid-actions-container">
            <PButton 
              @click="openFormDialogAssignment(data)" 
              class="grid-button-text" 
              icon="pi pi-pencil" 
              v-tooltip.top="$t('COMMON_BUTTONS.editStatus')" 
              text 
              rounded 
              raised 
              outlined 
            />
            <PButton 
              @click="($event: Event) => handleDeleteAssignment($event, data.id)" 
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

  <!-- Diálogo para agregar/editar asignación -->
  <Dialog 
    v-model:visible="showDialogAssignment" 
    :style="{width: '450px'}" 
    :header="assignmentId ? $t('Editar Asignación') : $t('Nueva Asignación')" 
    :modal="true" 
    class="p-fluid"
  >
  <div class="field">
      <label for="userID">{{ $t('Usuario a Asignar') }}</label>
      <Dropdown
        id="userID"
        :modelValue="getCurrentUserId()"
        :options="usersOnly"
        optionValue="id"
        optionLabel="userName"
        class="p-inputtext-sm"
        
      />
    </div>

    <div class="field">
      <label for="observations">{{ $t('Observaciones') }}</label>
      <Textarea 
        id="observations" 
        v-model="assignmentDescription" 
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
        @click="showDialogAssignment = false" 
        style="background-color: #F9A03F; border-color: #F9A03F;" 
      />
      <PButton 
        :label="$t('COMMON_BUTTONS.save')" 
        icon="pi pi-check" 
        severity="info" 
        @click="saveAssignment" 
        style="background-color: #2DAAED; border-color: #2DAAED;" 
      />
    </template>
  </Dialog>
</template>