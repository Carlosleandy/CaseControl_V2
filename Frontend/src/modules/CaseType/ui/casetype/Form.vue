<script setup lang="ts">
    import type { CaseTypeRepository } from '../../domain/repository/casetypeRepository'
    import { useCaseType } from '../../composable/casetypeComposable'

    const props = defineProps<{
		casetypeRepository: CaseTypeRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        casetypeRecord,
		saveCaseType,
        getCaseTypeById,
        setCaseTypeData,
	} = useCaseType({
        casetypeRepository: props.casetypeRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getCaseTypeById(props.recordId).then((casetype) => {
                if(casetype) {
                    if(props.cloning) {
                        casetype.id = 0;
                    }
                    
                    setCaseTypeData(casetype);
                }
            });
        }
    }

    loadInitialData();
    defineExpose({ save: saveCaseType });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="casetypeRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="casetypeRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>