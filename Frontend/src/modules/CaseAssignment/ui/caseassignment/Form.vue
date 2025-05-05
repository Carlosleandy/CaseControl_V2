<script setup lang="ts">
    import type { CaseAssignmentRepository } from '../../domain/repository/caseassignmentRepository'
    import { useCaseAssignment } from '../../composable/caseassignmentComposable'

    const props = defineProps<{
		caseassignmentRepository: CaseAssignmentRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        caseassignmentRecord,
		saveCaseAssignment,
        getCaseAssignmentById,
        setCaseAssignmentData,
	} = useCaseAssignment({
        caseassignmentRepository: props.caseassignmentRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getCaseAssignmentById(props.recordId).then((caseassignment) => {
                if(caseassignment) {
                    if(props.cloning) {
                        caseassignment.id = 0;
                    }
                    
                    setCaseAssignmentData(caseassignment);
                }
            });
        }
    }

    loadInitialData();
    defineExpose({ save: saveCaseAssignment });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="caseassignmentRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="caseassignmentRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>