<script setup lang="ts">
    import ConfirmPopup from 'primevue/confirmpopup'
    import { ref } from 'vue'
    import { useI18n } from 'vue-i18n'
    import { useConfirm } from 'primevue/useconfirm'
    import { useDataCases } from "../../repository/caseinvestigationRepository"

const props = defineProps<{
  caseid?: number;
}>();

const { 
        recoveryHist, 
        recoveryHistTotal,
        getCasesAmountRecovery,
        generatePDFCasesAmountRecovery
    } = useDataCases();

    const i18n = useI18n();
    const confirm = useConfirm();


const rec=ref();

const loadInitialData = () => {
  if (props?.caseid) {
    getCasesAmountRecovery(props.caseid);
    };
};

const sumRecovery = (data) => {
return data.map(a=>a.amountRecovery).reduce((n,{amountRecovery}) => n + amountRecovery, 0);

};

const DownloadReportPDF = async () => {
            await generatePDFCasesAmountRecovery(props.caseid);  
        };


    loadInitialData();
</script>

<template>
    
    <ContentWrapper @onContentFinish="getAllRecords" :searching="loadingRecords">        

        <template #desktop>
            <div class="card">

                <div class="buttons-toolbar">
                    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.exportToPDF')" @click="DownloadReportPDF()" icon="pi pi-file-pdf" severity="success" aria-label="User"  />
                
                <div v-if="pdfUrl">
                    <iframe :src="pdfUrl" width="100%" height="600px"></iframe>
                </div>
                
                </div>

                <DataTable :value="recoveryHist.data" 
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
                            <h1>{{ $t('Listado de Recuperaciones')}}</h1>
    <!-- <h2>Total Recuperado: ${{recoveryHist.data.reduce((n,{amountRecovery}) => n + amountRecovery, 0)}}</h2> -->
                            <!-- <div style="min-width: 40%;">
                                <InputSearch v-model="filter.filter" @onChange="getAllRecords()" />
                            </div> -->
                        </div>
                    </template>
        
                    <Column field="id" :header="$t('ID')" style="width: 5%"></Column>
                    <Column field="amountRecovery" :header="$t('Monto Recuperado')" style="width: 15%"></Column>
                    <Column field="user.employee.nombre_Completo" :header="$t('Usuario Registró')" style="width: 15%"></Column>
                    <Column field="dateRecovery" :header="$t('Fecha Recuperación')" style="width:15%"></Column>
                    <Column field="dateRegistered" :header="$t('Fecha Registro')" style="width: 15%"></Column>
                    <Column field="observations" :header="$t('Observaciones')" style="width: 35%"></Column>
                                                           
                </DataTable>     
                <ProgressBar v-if="loadingRecords" mode="indeterminate" style="height: 1px"></ProgressBar>
                <ConfirmPopup></ConfirmPopup>          
            </div>
        </template>

    </ContentWrapper>

</template>

<style lang="scss"></style>