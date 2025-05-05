<script setup lang="ts">
    import type { RecommendationTypeRepository } from '../../domain/repository/recommendationtypeRepository'
    import { useRecommendationType } from '../../composable/recommendationtypeComposable'

    const props = defineProps<{
		recommendationtypeRepository: RecommendationTypeRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        recommendationtypeRecord,
		saveRecommendationType,
        getRecommendationTypeById,
        setRecommendationTypeData,
	} = useRecommendationType({
        recommendationtypeRepository: props.recommendationtypeRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getRecommendationTypeById(props.recordId).then((recommendationtype) => {
                if(recommendationtype) {
                    if(props.cloning) {
                        recommendationtype.id = 0;
                    }
                    
                    setRecommendationTypeData(recommendationtype);
                }
            });
        }
    }

    loadInitialData();
    defineExpose({ save: saveRecommendationType });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="recommendationtypeRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="recommendationtypeRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>