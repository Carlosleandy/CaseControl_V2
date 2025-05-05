<script setup lang="ts">
    import type { LinkTypeRepository } from '../../domain/repository/linktypeRepository'
    import { useLinkType } from '../../composable/linktypeComposable'

    const props = defineProps<{
		linktypeRepository: LinkTypeRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        linktypeRecord,
		saveLinkType,
        getLinkTypeById,
        setLinkTypeData,
	} = useLinkType({
        linktypeRepository: props.linktypeRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getLinkTypeById(props.recordId).then((linktype) => {
                if(linktype) {
                    if(props.cloning) {
                        linktype.id = 0;
                    }
                    
                    setLinkTypeData(linktype);
                }
            });
        }
    }

    loadInitialData();
    defineExpose({ save: saveLinkType });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="linktypeRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="linktypeRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>