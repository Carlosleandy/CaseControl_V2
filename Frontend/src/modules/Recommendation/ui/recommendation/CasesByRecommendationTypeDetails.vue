<script setup lang="ts">
    import ConfirmPopup from 'primevue/confirmpopup'
    import { ref } from 'vue'
    import { useI18n } from 'vue-i18n'
    import { useConfirm } from 'primevue/useconfirm'
    import { useDataRecommendation } from "../../repository/recommendationRepository"

const props = defineProps<{
  recommendationId?: number;
}>();


const { 
        casesbyrecommendationtype, 
        getCasesByRecommendationType,
        generatePDFCasesByRecommendationType
    } = useDataRecommendation();

    const i18n = useI18n();
    const confirm = useConfirm();

const loadInitialData = () => {
  if (props?.recommendationId) {
    getCasesByRecommendationType(props.recommendationId);
    };
};
 
const DownloadReportPDF = async () => {
        await generatePDFCasesByRecommendationType(props?.recommendationId);  
    }

    loadInitialData();
</script>

<template>
    
    <ContentWrapper @onContentFinish="getAllRecords" :searching="loadingRecords">        

        <template #desktop>
            <div class="card">
        
                <div class="buttons-toolbar">
                    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.exportToPDF')" @click="DownloadReportPDF()" icon="pi pi-file-pdf" severity="success" aria-label="User"  />
                </div>

                <DataTable :value="casesbyrecommendationtype.data" 
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
                            <h1>{{ $t('Listado de Casos')}}</h1>
    
                            <!-- <div style="min-width: 40%;">
                                <InputSearch v-model="filter.filter" @onChange="getAllRecords()" />
                            </div> -->
                        </div>
                    </template>
        
                    <Column field="case.id" :header="$t('ID')" style="width: 5%"></Column>
                    <Column field="case.subject" :header="$t('Asunto')" style="width: 25%"></Column>
                    <Column field="case.communicationNumber" :header="$t('No. Comunicación')" style="width: 10%"></Column>
                    <Column field="case.branch" :header="$t('Oficina')" style="width:5%"></Column>
                    <Column field="case.caseType.name" :header="$t('Tipo Caso')" style="width: 15%"></Column>
                    <Column field="case.amountDetected" :header="$t('Monto Detectado')" style="width: 10%"></Column>
                    <Column field="case.amountInvestigated" :header="$t('Monto Investigado')" style="width: 10%"></Column>
                    <Column field="case.amountRecovered" :header="$t('Monto Recuperado')" style="width: 10%"></Column>
                    <Column field="case.amountLost" :header="$t('Monto Pérdida')" style="width: 10%"></Column>
                                       
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