<script setup lang="ts">
    import type { CaseInvestigationRepository } from '../../domain/repository/caseinvestigationRepository'  
    import type { RecommendationRepository } from '../../../Recommendation/domain/repository/recommendationRepository'
    import { useCaseInvestigation } from '../../composable/caseinvestigationComposable'
    import { useRecommendation } from '../../../Recommendation/composable/recommendationComposable'
    import type { CaseInvestigation, CaseInvestigationRow } from '../../domain/model/caseinvestigation'
    import type { Recommendation, RecommendationRow } from '../../../Recommendation/domain/model/recommendation'
    import ConfirmPopup from 'primevue/confirmpopup'
    import CaseInvestigationForm from './Form.vue'
    import CaseInvestigationFormDetails from './FormDetails.vue'
    import FormStatusChange from './FormStatusChange.vue'
    import CasesStatusChangeHist from './CasesStatusChangeHist.vue'
    import RecommendationForm from '../../../Recommendation/ui/recommendation/Form.vue'
    import FormAddRecovery from './FormAddRecovery.vue'
    import FormCaseAssignment from './FormCaseAssignment.vue'
    import Filter from './Filter.vue'
    import { ref } from 'vue'
    import { useI18n } from 'vue-i18n'

    import { useConfirm } from 'primevue/useconfirm'




    const i18n = useI18n();
    const confirm = useConfirm();

    const props = defineProps<{
        caseinvestigationRepository : CaseInvestigationRepository,
        recommendationRepository : RecommendationRepository
    }>();

    const recordId = ref(0);
    const isCloningRecord = ref(false);
    const formRef = ref();
    const caseInvestigationSelected = ref();
    
    const {
        loadingRecords,
        entities,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        showDialogDetails,
        showDialogStatusChange,
        showDialogAddRecovery,
        showDialogChangeHist,
        showDialogLinked,
        showDialogAddAssignment,
        showDialogRecoveryHist,
        totalRecords,
        filterIsApplied,        
        getAllRecords,
        deleteRecord
    } = useCaseInvestigation({
        caseinvestigationRepository: props.caseinvestigationRepository
    });


    const {
        loadingRecordsRecommendation,
        savingRecordRecommendation,
        showFilterRecommendation,
        showDialogRecommendation,
        filterIsAppliedRecommendation,       
    } = useRecommendation({
        recommendationRepository: props.recommendationRepository
    });
    
    async function save(): Promise<void> {
        savingRecord.value = true;        
        const result = await formRef.value.save();
        savingRecord.value = false;
        getAllRecords();         
        showDialog.value = false;
    }

    async function saveRec(): Promise<void> {
        savingRecord.value = true;        
        const result = await formRef.value.saveRec();
        savingRecord.value = false;
        getAllRecords();         
        showDialog.value = false;
    }

     async function saveRecovery(): Promise<void> {
        savingRecord.value = true;        
        const result = await formRef.value.saveRecovery();
        savingRecord.value = false;
        getAllRecords();         
        showDialogAddRecovery.value = false;
    }

    async function saveAssignment(): Promise<void> {
        savingRecord.value = true;        
        const result = await formRef.value.saveAssignment();
        savingRecord.value = false;
        getAllRecords();         
        showDialogAddAssignment.value = false;
    }

    async function saveStatusChange(): Promise<void> {
        savingRecord.value = true;        
        const result = await formRef.value.saveStatus();
        savingRecord.value = false;
        getAllRecords();         
        showDialogStatusChange.value = false;
    }

    const openFormDialog = (record: CaseInvestigation | null, isCloning = false) => {
        recordId.value = record ? record.id : 0;
        showDialog.value = true;
        isCloningRecord.value = isCloning;
    }

    const openFormDialogDetails = (record: CaseInvestigation | null, isCloning = false) => {
        recordId.value = record ? record.id : 0;
        showDialogDetails.value = true;
        isCloningRecord.value = isCloning;
    }

    const openFormDialogStatusChange = (record: CaseInvestigation | null, isCloning = false) => {
        recordId.value = record ? record.id : 0;
        caseInvestigationSelected.value = record;
        showDialogStatusChange.value = true;
        isCloningRecord.value = isCloning;
    }

    const openFormDialogAddAmountRecovery = (record: CaseInvestigation | null, isCloning = false) => {
        recordId.value = record ? record.id : 0;
        caseInvestigationSelected.value = record;
        showDialogAddRecovery.value = true;
    }

    const openFormDialogStatusChangeHist = (record: CaseInvestigation | null, isCloning = false) => {
        recordId.value = record ? record.id : 0;
        caseInvestigationSelected.value = record;
        showDialogChangeHist.value = true;
        isCloningRecord.value = isCloning;
    }

    const openFormDialogLinked = (record: CaseInvestigation | null) => {
        recordId.value = record ? record.id : 0;
        caseInvestigationSelected.value = record;
        showDialogLinked.value = true;
    }

    const openFormDialogAddCaseAssignment = (record: CaseInvestigation | null, isCloning = false) => {
        recordId.value = record ? record.id : 0;
        caseInvestigationSelected.value = record;
        showDialogAddAssignment.value = true;
        isCloningRecord.value = isCloning;
    }

    const openFormDialogAddBinnacle = (record: CaseInvestigation | null, isCloning = false) => {
        recordId.value = record ? record.id : 0;
        showDialogDetails.value = true;
        isCloningRecord.value = isCloning;
    }

    const openFormDialogAddRecommendation = (record: Recommendation | null, isCloning = false) => {
        recordId.value = record ? record.id : 0;
        showDialogRecommendation.value = true;
        isCloningRecord.value = isCloning;
    }

    async function deleteCaseInvestigationById(id: number): Promise<void> {
        const result = await deleteRecord(id);
        if (result) {
            getAllRecords(); 
        }
    }

    function confirmDelete(event: any, id: number) {
        confirm.require({
            target: event.currentTarget,
            message: '¿Está seguro(a) que desea eliminar este registro?',
            icon: 'pi pi-question-circle',
            accept: () => {
            deleteCaseInvestigationById(id);
            },
        });
    }
    
    getAllRecords();
</script>

<template>
    
    <ContentWrapper @onContentFinish="getAllRecords" :searching="loadingRecords">        

        <template #desktop>
            <div class="card">
        
                <div class="buttons-toolbar">
                    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.new')" @click="openFormDialog(null,true)" icon="pi pi-plus" severity="info" aria-label="User"  />
                </div>

                <DataTable :value="entities" 
                    size="small"
                    scrollable
                    scrollHeight="calc(100vh - 300px)"
                    showGridlines 
                    tableStyle="min-width: 50rem"                     
                    ref=""                     
                    paginator 
                    :rows="5"      
                    :rowsPerPageOptions="[5, 10, 25, 50]"
                >
                    <template #header>
                        <div class="flex corn">
                            <h1>{{ $t('Casos')}}</h1>
    
                            <div style="min-width: 40%;">
                                <InputSearch v-model="filter.filter" @onChange="getAllRecords()" />
                            </div>
                        </div>
                    </template>
        
                    <Column field="id" :header="$t('ID')" style="width: 10%"></Column>
                    <Column field="subject" :header="$t('Asunto')" style="width: 40%"></Column>
                    <Column field="communicationNumber" :header="$t('No. Comunicación')" style="width: 10%"></Column>
                    <Column field="branch" :header="$t('Oficina')" style="width: 10%"></Column>
                    <Column field="caseType.name" :header="$t('Tipo Caso')" style="width: 10%"></Column>
                    <Column field="caseStatus.name" :header="$t('Estado')" style="width: 10%"></Column>
                    <Column  :exportable="false" style="width: 10%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="openFormDialog(data,false)" class="grid-button-text" icon="pi pi-pencil" v-tooltip.top="$t('COMMON_BUTTONS.edit')" text rounded raised outlined />
                                <PButton @click="openFormDialogStatusChange(data)" class="grid-button-text" icon="pi pi-code" v-tooltip.top="$t('COMMON_BUTTONS.statusChange')" text rounded raised outlined />
                                <PButton @click="openFormDialogStatusChangeHist(data)" class="grid-button-text" icon="pi pi-list" v-tooltip.top="$t('COMMON_BUTTONS.statusChangeHist')" text rounded raised outlined />
                            </div>
                        </template>
                    </Column>
                    
                </DataTable>     
                <ProgressBar v-if="loadingRecords" mode="indeterminate" style="height: 1px"></ProgressBar>
                <ConfirmPopup></ConfirmPopup>          
            </div>
        </template>

    </ContentWrapper>
    
    <PDialog v-model:visible="showDialog" :closeOnEscape="false" modal :header="$t('' + (recordId === 0 ? $t('COMMON_TITLES.new_record'): $t('COMMON_TITLES.edit_record')) )" :style="{ width: '88vw' }">
        <div>
            <CaseInvestigationForm 
                :caseinvestigationRepository="caseinvestigationRepository" 
                :isnew="isCloningRecord" 
                :recordId="recordId"
                ref="formRef" 
            />
        </div>
        <template #footer>            
            <PButton @click="showDialog = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="save" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

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

    <PDialog v-model:visible="showDialogAddRecovery" :closeOnEscape="false" modal :header="$t('' + (recordId === 0 ? $t(''): $t('Agregar Recuperación al Caso: ' + recordId)) )" :style="{ width: '88vw' }">
        <div>
            <FormAddRecovery 
                :caseInvestigation="caseInvestigationSelected" 
                ref="formRef" 
            />
        </div>
        <template #footer>            
            <PButton @click="showDialogAddRecovery = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="saveRecovery" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

    <PDialog v-model:visible="showDialogAddAssignment" :closeOnEscape="false" modal :header="$t('' + (recordId === 0 ? $t(''): $t('Asignaciones del Caso: ' + recordId)) )" :style="{ width: '88vw' }">
        <div>
            <FormCaseAssignment 
                :caseInvestigation="caseInvestigationSelected" 
                ref="formRef" 
            />
        </div>
        <template #footer>            
            <PButton @click="showDialogAddAssignment = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="saveAssignment" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

    <PDialog v-model:visible="showDialogStatusChange" :closeOnEscape="false" modal :header="$t('' + (recordId === 0 ? $t(''): $t('Cambio de Estado al Caso: ' + recordId)) )" :style="{ width: '88vw' }">
        <div>
            <FormStatusChange 
                :caseinvestigationRepository="caseinvestigationRepository" 
                :caseInvestigation="caseInvestigationSelected"
                :cloning="isCloningRecord" 
                :recordId="recordId"
                ref="formRef" 
            />
        </div>
        <template #footer>            
            <PButton @click="showDialogStatusChange = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="saveStatusChange" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

    <PDialog v-model:visible="showDialogChangeHist" :closeOnEscape="false" modal :header="$t('' + (recordId === 0 ? $t(''): $t('Historial de Cambios de Estados del Caso: ' + recordId)) )" :style="{ width: '88vw' }">
        <div>
            <CasesStatusChangeHist 
                :caseid="recordId"
                ref="formRef" 
            />
        </div>
        <template #footer>            
        </template>
    </PDialog>

    <PDialog v-model:visible="showDialogLinked" :closeOnEscape="false" modal :header="$t('' + (recordId === 0 ? $t(''): $t('Vinculados del Caso: ' + recordId)) )" :style="{ width: '88vw' }">
        <div>
            <CaseLinkeds 
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