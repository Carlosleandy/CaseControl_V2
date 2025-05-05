<script setup lang="ts">
    import type { ReceptionMediumRepository } from '../../domain/repository/receptionmediumRepository'  
    import { useReceptionMedium } from '../../composable/receptionmediumComposable'
    import type { ReceptionMedium, ReceptionMediumRow } from '../../domain/model/receptionmedium'
    import ConfirmPopup from 'primevue/confirmpopup'
    import ReceptionMediumForm from './Form.vue'
    import Filter from './Filter.vue'
    import { ref } from 'vue'
    import { useI18n } from 'vue-i18n'
    import { useConfirm } from 'primevue/useconfirm'
    
    const i18n = useI18n();
    const confirm = useConfirm();

    const props = defineProps<{
        receptionmediumRepository : ReceptionMediumRepository      
    }>();

    const recordId = ref(0);
    const isCloningRecord = ref(false);
    const formRef = ref();
    
    const {
        loadingRecords,
        entities,
        filter,
        savingRecord,
        showFilter,
        showDialog,
        totalRecords,
        filterIsApplied,        
        getAllRecords,
        deleteRecord
    } = useReceptionMedium({
        receptionmediumRepository: props.receptionmediumRepository
    });
    
    async function save(): Promise<void> {
        savingRecord.value = true;        
        const result = await formRef.value.save();
        savingRecord.value = false;
        getAllRecords();         
        showDialog.value = false;
    }

    const openFormDialog = (record: ReceptionMedium | null, isCloning = false) => {
        recordId.value = record ? record.id : 0;
        showDialog.value = true;
        isCloningRecord.value = isCloning;
    }

    async function deleteReceptionMediumById(id: number): Promise<void> {
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
            deleteReceptionMediumById(id);
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
                    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.new')" @click="openFormDialog(null)" icon="pi pi-plus" severity="info" aria-label="User" />
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
                            <h1>{{ $t('Medios de Recepción')}}</h1>
    
                            <div style="min-width: 40%;">
                                <InputSearch v-model="filter.filter" @onChange="getAllRecords()" />
                            </div>
                        </div>
                    </template>
        
                    <Column field="id" :header="$t('ID')" style="width: 10%"></Column>
                    <Column field="name" :header="$t('Descripción')" style="width: 80%"></Column>
                    <Column  :exportable="false" style="width: 10%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="openFormDialog(data)" class="grid-button-text" icon="pi pi-pencil" v-tooltip.top="$t('COMMON_BUTTONS.edit')" text rounded raised outlined />
                                <PButton @click="($event: Event)=>{ confirmDelete($event, data.id) }" class="grid-button-text" icon="pi pi-trash" v-tooltip.top="$t('COMMON_BUTTONS.delete')" severity="danger" text rounded raised outlined />
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
            <ReceptionMediumForm 
                :receptionmediumRepository="receptionmediumRepository" 
                :cloning="isCloningRecord" 
                :recordId="recordId"
                ref="formRef" 
            />
        </div>
        <template #footer>            
            <PButton @click="showDialog = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="save" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

    <Sidebar v-model:visible="showFilter"  position="right">
        <Filter v-model="filter.filters" @onChage="getAllRecords"></Filter>
    </Sidebar>
</template>

<style lang="scss"></style>