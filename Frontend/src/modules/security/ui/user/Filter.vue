<script setup lang="ts">
    import type { UserFilter } from "../../domain/entity/user"
    import Calendar from "primevue/calendar"
    import Dropdown from 'primevue/dropdown'
    import { useI18n } from "vue-i18n"
    import { ref } from 'vue'
    
    const emit = defineEmits(['update:modelValue', 'onChage']);
    
    const i18n = useI18n();
    const props = defineProps<{
		modelValue: UserFilter
	}>();

    const filter = ref<UserFilter>({...props.modelValue});

    const status  = ref([
        {id: true, name: i18n.t('SHARED.active')},
        {id: false, name: i18n.t('SHARED.inactive')},
    ]);

    const clearFilter = () => {
        filter.value = {} as UserFilter;
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
                <Calendar v-model="filter.creationDateFrom" :dateOnly="true" dateFormat="dd/mm/yy" showIcon iconDisplay="input" />
                <label for="title">{{ $t('SECURITY.creation_date_from') }}</label>
            </span>

            <span class="p-float-label">
                <Calendar v-model="filter.creationDateTo" sdateFormat="dd/mm/yy" showIcon iconDisplay="input" />
                <label for="title">{{ $t('SECURITY.creation_date_to') }}</label>
            </span>

            <span class="p-float-label">
                <Dropdown placeholder="Select an item" :showClear="true" optionLabel="name" v-model="filter.status" optionValue="id" :options="status" filter  >
                </Dropdown>
                <label for="Origin">{{ $t('SECURITY.status') }}</label>
            </span>
        </div>

        <div class="filter-actions">
            <PButton :label="$t('COMMON_BUTTONS.clean')" @click="clearFilter" severity="warning" icon="pi pi-eraser" autofocus />
            <PButton :label="$t('COMMON_BUTTONS.apply')" @click="appy" icon="pi pi-check" autofocus />
        </div>
    </div>
</template>