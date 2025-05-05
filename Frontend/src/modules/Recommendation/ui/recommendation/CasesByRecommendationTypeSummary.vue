<script setup lang="ts">
    import ConfirmPopup from 'primevue/confirmpopup'
    import { ref } from 'vue'
    import { useI18n } from 'vue-i18n'
    import { useConfirm } from 'primevue/useconfirm'
    import { useDataRecommendation } from "../../repository/recommendationRepository"
    import type { CasesByRecommendationTypeSummaryDTO } from '../../domain/model/recommendation'
 import CasesByRecommendationTypeDetails from './CasesByRecommendationTypeDetails.vue'


const { 
        showDialogDetails,
        casesbyrecommendationtypesummary, 
        getCasesByRecommendationTypeSummary,
        generatePDFCasesByRecommendationTypeSummary
    } = useDataRecommendation();

    const i18n = useI18n();
    const confirm = useConfirm();

  
    const recommendationId = ref(0);
    const recommendationDesc = ref();
    const isCloningRecord = ref(false);
    const formRef = ref();
    
   
    const openFormDialogDetails = (record: CasesByRecommendationTypeSummaryDTO | null, isCloning = false) => {
        recommendationId.value = record ? record.id : 0;
        recommendationDesc.value = record ? record.recommendationType : '';
        showDialogDetails.value = true;
        isCloningRecord.value = isCloning;
    }

    const DownloadReportPDF = async () => {
            await generatePDFCasesByRecommendationTypeSummary();  
        }

    getCasesByRecommendationTypeSummary();
</script>

<template>
    
    <ContentWrapper @onContentFinish="getAllRecords" :searching="loadingRecords">        

        <template #desktop>
            <div class="card">
        
                <div class="buttons-toolbar">
                    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.exportToPDF')" @click="DownloadReportPDF()" icon="pi pi-file-pdf" severity="success" aria-label="User"  />
                </div>

                <DataTable :value="casesbyrecommendationtypesummary.data" 
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
                            <h1>{{ $t('Resúmen de Casos por Tipo de Recomendación')}}</h1>
    
                            <!-- <div style="min-width: 40%;">
                                <InputSearch v-model="filter.filter" @onChange="getAllRecords()" />
                            </div> -->
                        </div>
                    </template>
        
                    <Column field="recommendationType" :header="$t('Tipo de Recomendación')" style="width: 70%"></Column>
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


    <PDialog v-model:visible="showDialogDetails" :closeOnEscape="false" modal :header="$t('' + (recommendationId === 0 ? $t(''): $t('Casos con Recomendación Tipo: (' + recommendationId + ' - ' + recommendationDesc + ')')) )" :style="{ width: '88vw' }">
        <div>
            <CasesByRecommendationTypeDetails 
                :recommendationId="recommendationId" 
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