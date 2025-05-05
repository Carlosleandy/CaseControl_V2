<script setup lang="ts">
    import type { EvidenceRepository } from '../../domain/repository/evidenceRepository'
    import { useEvidence } from '../../composable/evidenceComposable'

    const props = defineProps<{
		evidenceRepository: EvidenceRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        evidenceRecord,
		saveEvidence,
        getEvidenceById,
        setEvidenceData,
	} = useEvidence({
        evidenceRepository: props.evidenceRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getEvidenceById(props.recordId).then((evidence) => {
                if(evidence) {
                    if(props.cloning) {
                        evidence.id = 0;
                    }
                    
                    setEvidenceData(evidence);
                }
            });
        }
    }

    loadInitialData();
    defineExpose({ save: saveEvidence });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="evidenceRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="evidenceRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>