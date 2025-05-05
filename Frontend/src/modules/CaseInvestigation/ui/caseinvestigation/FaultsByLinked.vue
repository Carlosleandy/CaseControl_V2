<script setup lang="ts">
    import ConfirmPopup from 'primevue/confirmpopup'
    import { ref } from 'vue'
    import { useI18n } from 'vue-i18n'
    import { useConfirm } from 'primevue/useconfirm'
    import { useDataEmployee } from  "../../../shared/utility/employees"
    import type { Linked } from "../../../Linked/domain/model/linked"
    import { useDataFaultLinked } from "../../../FaultLinked/repository/faultlinkedRepository"

    const { employee, getEmployee } = useDataEmployee();

const ident = ref('');
const linkTypeName = ref('');
const linkedRecord = ref({} as Linked);
const faultsList = ref([]);

const props = defineProps<{
  userName?: string;
}>();


    const { 
        faultsbycodelinked,
        getFaultsByCodeLinked,
        generatePDFFaultsByLinkedCodeAsync
    } = useDataFaultLinked();

    const i18n = useI18n();
    const confirm = useConfirm();

const loadInitialData = () => {
  if (props?.userName) {
    getFaultsByCodeLinked(props.userName);
    };
};
 

 const getCasesByLinked = async() => { debugger;
        await getFaultsByCodeLinked(ident.value);     
        linkedRecord.value=faultsbycodelinked.value.data[0].linked;
        linkTypeName.value=linkedRecord.value.linkType.name;       
    };

  const DownloadReportPDF = async () => {
        await generatePDFFaultsByLinkedCodeAsync(ident.value);  
    }

    loadInitialData();
</script>

<template>
    
    <ContentWrapper @onContentFinish="getAllRecords" :searching="loadingRecords">        

        <template #desktop>
            <div class="card">
<Card>
    <template #title>Faltas por Vinculado</template>
    <template #content>
         <div class="buttons-toolbar">
                                    <span class="p-float-label">
                                        <InputText v-model="ident" />
                                        <label for="ident">{{ $t('Código') }}</label>
                                    </span>
                                    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.search')" @click="getCasesByLinked()" icon="pi pi-search" severity="success" aria-label="User" />
                                </div>
        


        <div class="flexbox-grid container">
        <div class="grid">
                    <span class="p-float-label">
                        <InputText v-model="linkTypeName" disabled/>
                        <label for="name">{{ $t('Tipo de Vinculado') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="linkedRecord.code"  disabled/>
                        <label for="code">{{ $t('Código') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="linkedRecord.name"  disabled/>
                        <label for="name">{{ $t('Nombre') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="linkedRecord.lastName"  disabled/>
                        <label for="lastName">{{ $t('Apellido') }}</label>
                    </span>
        </div>
        <div class="grid">
            <span class="p-float-label">
                        <Calendar v-model="linkedRecord.birthdate" dateFormat="dd/mm/yy"  disabled/>
                        <label for="birthdate">{{ $t('Fecha Nacimiento') }}</label>
                    </span>
          <span class="p-float-label">
                        <InputText v-model="linkedRecord.identification"  disabled/>
                        <label for="identification">{{ $t('Identificación') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="linkedRecord.phone"  disabled/>
                        <label for="phone">{{ $t('Teléfono') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="linkedRecord.cellPhone"  disabled/>
                        <label for="cellPhone">{{ $t('Celular') }}</label>
                    </span>
        </div>
      </div>
    </template>
</Card>

                <DataTable :value="faultsbycodelinked.data" 
                    size="small"
                    scrollable
                    scrollHeight="calc(100vh - 300px)"
                    showGridlines 
                    tableStyle="min-width: 50rem"                     
                    ref=""                     
                    paginator 
                    :rows="10"      
                    :rowsPerPageOptions="[10, 20, 35, 50]"
                >
                    <template #header>
                        <div class="flex corn">
                            <h1>{{ $t('Listado de Faltas')}}</h1>
                                    
                                    <div class="buttons-toolbar">
                    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.exportToPDF')" @click="DownloadReportPDF()" icon="pi pi-file-pdf" severity="success" aria-label="User"  />
                </div>
                        </div>
                    </template>
                            
                    <Column field="caseID" :header="$t('Caso')" style="width: 10%"></Column>
                    <Column field="dateRegistered" :header="$t('Fecha Registro Falta')" style="width: 10%"></Column>
                    <Column field="fault.faultType.name" :header="$t('Tipo de Falta')" style="width: 10%"></Column>
                    <Column field="fault.name" :header="$t('Falta')" style="width: 70%"></Column>
                                     
                </DataTable>     
                <ProgressBar v-if="loadingRecords" mode="indeterminate" style="height: 1px"></ProgressBar>
                <ConfirmPopup></ConfirmPopup>          
            </div>
        </template>

    </ContentWrapper>


    <PDialog v-model:visible="showDialogDetails" :closeOnEscape="false" modal :header="$t('' + (recordId === 0 ? $t(''): $t('Caso: ' + recordId)) )" :style="{ width: '88vw' }">
        <div>
            <CaseInvestigationFormDetails 
                :caseinvestigationRepository="caseinvestigationRepository" 
                :recommendationRepository="recommendationRepository"
                :cloning="isCloningRecord" 
                :recordId="recordId"
                ref="formRef" 
            />
        </div>
        <template #footer>            
            
        </template>
    </PDialog>

    <Sidebar v-model:visible="showFilter"  position="right">
        <Filter v-model="filter.filters" @onChage="getAllRecords"></Filter>
    </Sidebar>
</template>

<style lang="scss"></style>