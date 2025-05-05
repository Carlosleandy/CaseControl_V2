<script setup lang="ts">
    import type { WorkingGroupRepository } from '../../domain/repository/workinggroupRepository'
    import { useWorkingGroup } from '../../composable/workinggroupComposable'

    const props = defineProps<{
		workinggroupRepository: WorkingGroupRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        workinggroupRecord,
		saveWorkingGroup,
        getWorkingGroupById,
        setWorkingGroupData,
	} = useWorkingGroup({
        workinggroupRepository: props.workinggroupRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getWorkingGroupById(props.recordId).then((workinggroup) => {
                if(workinggroup) {
                    if(props.cloning) {
                        workinggroup.id = 0;
                    }
                    
                    setWorkingGroupData(workinggroup);
                }
            });
        }
    }

    loadInitialData();
    defineExpose({ save: saveWorkingGroup });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="workinggroupRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="workinggroupRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>