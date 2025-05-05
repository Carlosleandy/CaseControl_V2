<script setup lang="ts">

    const props = defineProps<{
        title: string,
        description: string,
        class?: string,
        style?: any,
        color?: '#FFE5D5'
    }>();

</script>

<template>
    <div class="card info-card" :class="props.class">
        <section>
            <slot name="icon"></slot>
        </section>
        <section class="description">
            <h3>{{  props.title  }}</h3>
            <span>{{ props.description }}</span>
        </section>
    </div>
</template>

<style>
    .info-card {
        display: flex;
        gap: 10px;
        align-items: center;

        .icon-container{
            border-radius: 15px;
            background: #FFE5D5;
            padding: 17px;
            display: flex;
        }        

        section.description {
            display: flex;
            flex-direction: column;
        }
    }
</style>