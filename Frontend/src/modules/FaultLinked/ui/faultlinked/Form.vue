<script setup lang="ts">
    import type { FaultLinkedRepository } from '../../domain/repository/faultlinkedRepository'
    import { useDataFaultLinked } from "../../repository/faultlinkedRepository"
    import type { FaultLinked } from '../../domain/model/faultlinked'
    import { useDataFaultType } from "../../../Fault/repository/faultRepository"
import type { CaseInvestigation } from "../../../CaseInvestigation/domain/model/caseinvestigation"
import { ref } from 'vue'

const selectedFaults = ref();
const selectedlinked = ref(0);
const faultlinkedRecord = ref({} as FaultLinked)

    const props = defineProps<{
        faultlinkedRepository: FaultLinkedRepository,
        caseInvestigation?: CaseInvestigation,
	}>();

const { saveFaultLinked } = useDataFaultLinked();
const { faultsList, getAllFaults } = useDataFaultType();

const save = async () => { debugger;
        if(selectedFaults) {
            for (const element of selectedFaults.value) { 
                faultlinkedRecord.value.id = 0;
                faultlinkedRecord.value.caseID = props.caseInvestigation.id;
                faultlinkedRecord.value.linkedID = selectedlinked.value;
                faultlinkedRecord.value.faultID = element;    
                await saveFaultLinked(faultlinkedRecord.value);
            }
        }
    }


    // loadInitialData();
    getAllFaults();
    defineExpose({ saveFaultLinked: save });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <Dropdown
                        v-model="selectedlinked"
                        :options="caseInvestigation.linkeds"
                        optionValue="id"
                        optionLabel="fullName"
                        filter
                        />
                        <label for="faultlinkedRecord.linkedID">{{$t("Vinculado")}}</label>
                    </span> 
                    <span class="p-float-label">
                        <MultiSelect
                            v-model="selectedFaults"
                            :options="faultsList.data"
                            optionValue="id"
                            optionLabel="name"
                            filter
                            placeholder="Seleccione las Faltas"
                        />
                        <label for="faultlinkedRecord.faultID">{{$t("Faltas")}}</label>
                    </span> 
                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>