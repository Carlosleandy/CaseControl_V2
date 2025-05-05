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
        changeHist, 
        getCasesStatusChangeHistAsync,
        generatePDFCasesStatusChangeHist
    } = useDataCases();

    const i18n = useI18n();
    const confirm = useConfirm();


const rec=ref();

const loadInitialData = () => {
  if (props?.caseid) {
    getCasesStatusChangeHistAsync(props.caseid);
    };
};

const DownloadReportPDF = async () => {
            await generatePDFCasesStatusChangeHist(props.caseid);  
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

                <DataTable :value="changeHist.data" 
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
                            <h1>{{ $t('Historial de Movimientos')}}</h1>
                        </div>
                    </template>
      
                    <Column field="case.subject" :header="$t('Asunto Caso')" style="width:45%"></Column>
                    <Column field="dateRegistered" :header="$t('Fecha Regisgtro')" style="width: 15%"></Column>
                    <Column field="userNameRegistered" :header="$t('Usuario Registró')" style="width: 15%"></Column>
                    <Column field="caseStatus.name" :header="$t('Estado')" style="width: 25%"></Column>
                    
                                                           
                </DataTable>     
                <ProgressBar v-if="loadingRecords" mode="indeterminate" style="height: 1px"></ProgressBar>
                <ConfirmPopup></ConfirmPopup>          
            </div>
        </template>

    </ContentWrapper>

</template>

<style lang="scss"></style>