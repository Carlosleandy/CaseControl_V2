<script setup lang="ts">
    import type { BinnacleFilter } from "../../domain/model/binnacle"
    import { ref } from 'vue'
    
    const emit = defineEmits(['update:modelValue', 'onChage']);
    
    const props = defineProps<{
		modelValue: BinnacleFilter
	}>();

    
    const filter = ref<BinnacleFilter>({...props.modelValue});

    const clearFilter = () => {
        filter.value = {} as BinnacleFilter;
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
                <label for="title">{{ $t('ENTITY.name') }}</label>
            </span>
        </div>

        <div class="filter-actions">
            <PButton :label="$t('COMMON_BUTTONS.clean')" @click="clearFilter" severity="warning" icon="pi pi-eraser" autofocus />
            <PButton :label="$t('COMMON_BUTTONS.apply')" @click="appy" icon="pi pi-check" autofocus />
        </div>
    </div>
</template>