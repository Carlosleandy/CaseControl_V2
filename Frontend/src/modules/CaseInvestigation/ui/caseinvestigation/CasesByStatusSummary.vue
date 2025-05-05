<script setup lang="ts">
    import ConfirmPopup from 'primevue/confirmpopup'
    import { ref } from 'vue'
    import { useI18n } from 'vue-i18n'
    import { useConfirm } from 'primevue/useconfirm'
    import { useDataCases } from "../../repository/caseinvestigationRepository"
    import type { CasesByStatusSummaryDTO } from '../../domain/model/caseinvestigation'
import CasesByStatusDetails from './CasesByStatusDetails.vue'


const { 
        showDialogDetails,
        casesbystatussummary, 
        getCasesByStatusSummary,
        generatePDFCasesByStatusSummary
    } = useDataCases();

    const i18n = useI18n();
    const confirm = useConfirm();

  
    const statusId = ref(0);
    const statusDesc = ref();
    const isCloningRecord = ref(false);
    const formRef = ref();

    const pdfUrl=ref();
    
   
    const openFormDialogDetails = (record: CasesByStatusSummaryDTO | null, isCloning = false) => {
        statusId.value = record ? record.id : 0;
        statusDesc.value = record ? record.status : '';
        showDialogDetails.value = true;
        isCloningRecord.value = isCloning;
    }

    const DownloadReportPDF = async () => {
            await generatePDFCasesByStatusSummary();  
        }
       

    getCasesByStatusSummary();
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

                <DataTable :value="casesbystatussummary.data" 
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
                            <h1>{{ $t('Resúmen de Casos por Estados')}}</h1>
    
                            <!-- <div style="min-width: 40%;">
                                <InputSearch v-model="filter.filter" @onChange="getAllRecords()" />
                            </div> -->
                        </div>
                    </template>
        
                    <Column field="status" :header="$t('Estado')" style="width: 70%"></Column>
                    <Column field="count" :header="$t('Cantidad')" style="width: 20%"></Column>
                    <Column :exportable="false" style="width: 10%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="openFormDialogDetails(data)" class="grid-button-text" icon="pi pi-server" v-tooltip.top="$t('COMMON_BUTTONS.details')" text rounded raised outlined />
                            </div>
                        </template>
                    </Column>
               
                </DataTable>     
                <ProgressBar v-if="loadingRecords" mode="indeterminate" style="height: 1px"></ProgressBar>
                <ConfirmPopup></ConfirmPopup>          
            </div>
        </template>

    </ContentWrapper>


    <PDialog v-model:visible="showDialogDetails" :closeOnEscape="false" modal :header="$t('' + (statusId === 0 ? $t(''): $t('Casos en Estado: (' + statusId + ' - ' + statusDesc + ')')) )" :style="{ width: '88vw' }">
        <div>
            <CasesByStatusDetails 
                :statusId="statusId" 
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