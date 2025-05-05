<script setup lang="ts">
    import type { EvidenceClassificationRepository } from '../../domain/repository/evidenceclassificationRepository'
    import { useEvidenceClassification } from '../../composable/evidenceclassificationComposable'

    const props = defineProps<{
		evidenceclassificationRepository: EvidenceClassificationRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        evidenceclassificationRecord,
		saveEvidenceClassification,
        getEvidenceClassificationById,
        setEvidenceClassificationData,
	} = useEvidenceClassification({
        evidenceclassificationRepository: props.evidenceclassificationRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getEvidenceClassificationById(props.recordId).then((evidenceclassification) => {
                if(evidenceclassification) {
                    if(props.cloning) {
                        evidenceclassification.id = 0;
                    }
                    
                    setEvidenceClassificationData(evidenceclassification);
                }
            });
        }
    }

    loadInitialData();
    defineExpose({ save: saveEvidenceClassification });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="evidenceclassificationRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="evidenceclassificationRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>