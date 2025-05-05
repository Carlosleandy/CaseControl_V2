<script setup lang="ts">
    import type { CaseTypeFilter } from "../../domain/model/casetype"
    import { ref } from 'vue'
    
    const emit = defineEmits(['update:modelValue', 'onChage']);
    
    const props = defineProps<{
		modelValue: CaseTypeFilter
	}>();

    
    const filter = ref<CaseTypeFilter>({...props.modelValue});

    const clearFilter = () => {
        filter.value = {} as CaseTypeFilter;
        emit('update:modelValue', filter);
        emit('onChage', filter);
    }
    
    const appy = () => {
        emit('update:modelValue', filter);
        emit('onChage', filter);
    }
</script>

<template>
    <div class="filter-container">
        <div class="flexbox-grid">
            <span class="p-float-label">
                <InputText v-model="filter.name" />
                <label for="title">{{ $t('COMMON_BUTTONS.search') }}</label>
            </span>
        </div>

        <div class="filter-actions">
            <PButton :label="$t('COMMON_BUTTONS.clean')" @click="clearFilter" severity="warning" icon="pi pi-eraser" autofocus />
            <PButton :label="$t('COMMON_BUTTONS.apply')" @click="appy" icon="pi pi-check" autofocus />
        </div>
    </div>
</template>