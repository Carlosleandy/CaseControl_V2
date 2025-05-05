<script setup lang="ts">
    defineProps<{
        total: number,
        shown: number,
    }>();    
</script>

<template>
    <div>
        <span>
            {{ $t('SHARED.showing_n_records', { shown, total } ) }}
        </span>
    </div>
</template>