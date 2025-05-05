<script setup lang="ts">
    import ConfirmPopup from 'primevue/confirmpopup'
    import { ref } from 'vue'
    import { useI18n } from 'vue-i18n'
    import { useConfirm } from 'primevue/useconfirm'
    import { useDataCases } from "../../repository/caseinvestigationRepository"
    import type { CasesUserDTO } from "../../domain/model/caseinvestigation"

const props = defineProps<{
}>();


const { 
        rankingcasesbyuser, 
        getRankingCasesByUser,
        GeneratePDFRankingCasesByUser
    } = useDataCases();

    const i18n = useI18n();
    const confirm = useConfirm();

const loadInitialData = () => {
    getRankingCasesByUser();
};
 
  const DownloadReportPDF = async () => {
        await GeneratePDFRankingCasesByUser();  
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

                <DataTable :value="rankingcasesbyuser.data.casesUserDTOs" 
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
                            <h1>{{ $t('Ranking de Casos por Usuario')}}</h1>
    
                        </div>
                    </template>
        
                    <Column field="user.userName" :header="$t('Usuario')" style="width: 20%"></Column>
                    <Column field="user.employee.nombre_Completo" :header="$t('Nombre')" style="width: 70%"></Column>
                    <Column field="casesCount" :header="$t('Cantidad')" style="width: 10%"></Column>
                              
                </DataTable>     
                <ProgressBar v-if="loadingRecords" mode="indeterminate" style="height: 1px"></ProgressBar>
                <ConfirmPopup></ConfirmPopup>          
            </div>
        </template>

    </ContentWrapper>

</template>

<style lang="scss"></style>