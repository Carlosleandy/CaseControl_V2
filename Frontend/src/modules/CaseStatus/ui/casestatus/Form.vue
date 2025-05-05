<script setup lang="ts">
    import type { CaseStatusRepository } from '../../domain/repository/casestatusRepository'
    import { useCaseStatus } from '../../composable/casestatusComposable'

    const props = defineProps<{
		casestatusRepository: CaseStatusRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        casestatusRecord,
		saveCaseStatus,
        getCaseStatusById,
        setCaseStatusData,
	} = useCaseStatus({
        casestatusRepository: props.casestatusRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getCaseStatusById(props.recordId).then((casestatus) => {
                if(casestatus) {
                    if(props.cloning) {
                        casestatus.id = 0;
                    }
                    
                    setCaseStatusData(casestatus);
                }
            });
        }
    }

    loadInitialData();
    defineExpose({ save: saveCaseStatus });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="casestatusRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="casestatusRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputNumber v-model="casestatusRecord.percent" mode="decimal" :minFractionDigits="2" :maxFractionDigits="2" suffix="%" />
                        <label for="percent">{{ $t('Porciento') }}</label>
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>