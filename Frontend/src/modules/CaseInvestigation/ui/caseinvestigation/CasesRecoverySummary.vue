<script setup lang="ts">
    import ConfirmPopup from 'primevue/confirmpopup'
    import { ref } from 'vue'
    import { useI18n } from 'vue-i18n'
    import { useConfirm } from 'primevue/useconfirm'
    import { useDataCases } from "../../repository/caseinvestigationRepository"
    import CasesRecoveryHist from './CasesRecoveryHist.vue'
    import type { CasesRecoverySummaryDTO } from '../../domain/model/caseinvestigation'


const { 
        showDialogDetails,
        casesrecoverysummary, 
        getCasesRecoverySummary,
        generatePDFCasesRecoverySummary
    } = useDataCases();

    const i18n = useI18n();
    const confirm = useConfirm();

  
    const recordId = ref(0);    
    const isCloningRecord = ref(false);
    const formRef = ref();

    const pdfUrl=ref();
    
    const openFormDialogRecoveryHist = (record: CasesRecoverySummaryDTO | null, isCloning = false) => {
        recordId.value = record ? record.case.id : 0;
        showDialogDetails.value = true;
        isCloningRecord.value = isCloning;
    }
   
    const DownloadReportPDF = async () => {
            await generatePDFCasesRecoverySummary();  
        }
       

    getCasesRecoverySummary();
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

                <DataTable :value="casesrecoverysummary.data" 
                id="element-to-pdf"
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
                            <h1>{{ $t('Resúmen de Recuperaciones por Casos')}}</h1>
    
                            <!-- <div style="min-width: 40%;">
                                <InputSearch v-model="filter.filter" @onChange="getAllRecords()" />
                            </div> -->
                        </div>
                    </template>
        
                    <Column field="case.id" :header="$t('ID')" style="width: 5%"></Column>
                    <Column field="case.subject" :header="$t('Asunto')" style="width: 20%"></Column>
                    <Column field="case.communicationNumber" :header="$t('No. Comunicación')" style="width: 10%"></Column>
                    <Column field="case.caseType.name" :header="$t('Tipo Caso')" style="width: 20%"></Column>
                    <Column field="amountInvestigated" :header="$t('Monto Investigado')" style="width: 10%"></Column>
                    <Column field="amountRecovery" :header="$t('Monto Recuperado')" style="width: 10%"></Column>
                    <Column field="amountDifference" :header="$t('Diferencia')" style="width: 10%"></Column>
                    <Column field="percentRecovery" :header="$t('Porciento Recuperado')" style="width: 10%"></Column>
                    <Column :exportable="false" style="width: 5%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="openFormDialogRecoveryHist(data)" class="grid-button-text" icon="pi pi-sort-numeric-up" v-tooltip.top="$t('COMMON_BUTTONS.getHistRecoveries')" text rounded raised outlined />
                            </div>
                        </template>
                    </Column>
               
                </DataTable>     
                <ProgressBar v-if="loadingRecords" mode="indeterminate" style="height: 1px"></ProgressBar>
                <ConfirmPopup></ConfirmPopup>          
            </div>
        </template>

    </ContentWrapper>


    <PDialog v-model:visible="showDialogDetails" :closeOnEscape="false" modal :header="$t('' + (recordId === 0 ? $t(''): $t('Recuperaciones del Caso: ' + recordId)) )" :style="{ width: '88vw' }">
        <div>
            <CasesRecoveryHist 
                :caseid="recordId" 
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