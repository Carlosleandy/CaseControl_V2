<script setup lang="ts">
    import type { FaultRepository } from '../../domain/repository/faultRepository'
    import { useFault } from '../../composable/faultComposable'
    import { useDataFaultType } from "../../repository/faultRepository"

const { faultTypeList, getAllFaultTypes } = useDataFaultType();

    const props = defineProps<{
		faultRepository: FaultRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        faultRecord,
		saveFault,
        getFaultById,
        setFaultData,
	} = useFault({
        faultRepository: props.faultRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getFaultById(props.recordId).then((fault) => {
                if(fault) {
                    if(props.cloning) {
                        fault.id = 0;
                    }
                    
                    setFaultData(fault);
                }
            });
        }
    }

    getAllFaultTypes();
    loadInitialData();
    defineExpose({ save: saveFault });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="faultRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="faultRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>

                    <span class="p-float-label">
                        <Dropdown
                        v-model="faultRecord.faultTypeID"
                        :options="faultTypeList.data"
                        filter
                        optionValue="id"
                        optionLabel="name"
                        />
                        <label for="caseinvestigationRecord.caseTypeId">{{$t("Tipo Caso")}}</label>
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>