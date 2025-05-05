<script setup lang="ts">

    const props = defineProps<{
        label: string
    }>();

</script>

<template>
    <span>{{ props.label }}</span>
</template>

<style scoped>
    span {
        position: relative;
        color: var(--bg-color);
    }
    
    span::before {
        content: '';
        display: block;
        height: 12px;
        width: 12px;
        border-radius: 50%;
        position: absolute;
        background: var(--bg-color);
        left: -15px;
    }
</style>