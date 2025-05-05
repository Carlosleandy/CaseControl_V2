<script setup lang="ts">
    import type { ReceptionMediumRepository } from '../../domain/repository/receptionmediumRepository'
    import { useReceptionMedium } from '../../composable/receptionmediumComposable'

    const props = defineProps<{
		receptionmediumRepository: ReceptionMediumRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        receptionmediumRecord,
		saveReceptionMedium,
        getReceptionMediumById,
        setReceptionMediumData,
	} = useReceptionMedium({
        receptionmediumRepository: props.receptionmediumRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getReceptionMediumById(props.recordId).then((receptionmedium) => {
                if(receptionmedium) {
                    if(props.cloning) {
                        receptionmedium.id = 0;
                    }
                    
                    setReceptionMediumData(receptionmedium);
                }
            });
        }
    }

    loadInitialData();
    defineExpose({ save: saveReceptionMedium });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="receptionmediumRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="receptionmediumRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>