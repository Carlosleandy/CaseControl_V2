<script setup lang="ts">
    import IconField from 'primevue/iconfield'
    import InputIcon from 'primevue/inputicon'

    const props = defineProps<{
        modelValue: string | undefined
    }>();

    const emit = defineEmits(['update:modelValue', 'onChange'])    

    let timer: null | ReturnType<typeof setTimeout> = null;

    const onInputChange = (value: string) => {
        if(timer) {
            clearTimeout(timer);
        }

        timer = setTimeout(() => {
            emit('update:modelValue', value);
            emit('onChange', value);
        }, 500)
    }
</script>

<template>
    <IconField iconPosition="left" class="input-search">
        <InputIcon class="pi pi-search"> </InputIcon>
        <InputText :value="props.modelValue" :onInput="((e)=>onInputChange((e.target as HTMLTextAreaElement).value))" :placeholder="$t('SHARED.search')" />
    </IconField>
</template>

<style scoped>
    input {
        width: 100%;
    }
</style>