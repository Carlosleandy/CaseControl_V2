<script setup lang="ts">
    import type { RecommendationStatusRepository } from '../../domain/repository/recommendationstatusRepository'
    import { useRecommendationStatus } from '../../composable/recommendationstatusComposable'

    const props = defineProps<{
		recommendationstatusRepository: RecommendationStatusRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        recommendationstatusRecord,
		saveRecommendationStatus,
        getRecommendationStatusById,
        setRecommendationStatusData,
	} = useRecommendationStatus({
        recommendationstatusRepository: props.recommendationstatusRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getRecommendationStatusById(props.recordId).then((recommendationstatus) => {
                if(recommendationstatus) {
                    if(props.cloning) {
                        recommendationstatus.id = 0;
                    }
                    
                    setRecommendationStatusData(recommendationstatus);
                }
            });
        }
    }

    loadInitialData();
    defineExpose({ save: saveRecommendationStatus });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="recommendationstatusRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="recommendationstatusRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>