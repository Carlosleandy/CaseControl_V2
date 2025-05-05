<script setup lang="ts">
    import { ref } from 'vue';
    const detailOpened = ref(false);
    const detailLoaded = ref(false);

    const props = defineProps<{
        showDetailButton?: boolean
    }>();    
</script>

<template>
    <div class="card mobile-card">
        <div class="content">
            <div class="left">
                <slot name="left"></slot>
            </div>
            <div class="right">
                <slot name="right"></slot>
            </div>
        </div>

        <div class="detail" :class="{ 'open' : detailOpened, 'loaded': detailLoaded}">
            <div class="detail-content">
                <slot name="detail"></slot>
            </div>
        </div>

        <div class="actions">
            <slot name="actions" ></slot>
            <PButton v-if="props.showDetailButton" @click="detailOpened = !detailOpened; detailLoaded = true" text aria-label="Filter">
                <i class="pi " :class="{'pi-angle-double-down': !detailOpened, 'pi-angle-double-up': detailOpened}" style="font-size: 1.3rem"></i>
            </PButton>
        </div>   
    </div>
</template>

<style lang="scss" scoped>
    .card {
        border-radius: 10px;
        padding: 10px;    
    }

    .content {
        display: flex;
        justify-content: space-between;
    }

    .left, .right {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        gap: 5px;
    }

    .actions {
        display: flex;
        justify-content: right;
    }

    @keyframes heightAnimation {
        from {
            max-height: 0;
        }
        to {
            max-height: 200px;
            min-height: 50px;
            overflow: auto;
        }
    }

    @keyframes heightAnimationClose {
        from {
            max-height: 200px;
        }
        to {
            max-height: 0;
            min-height: 0;
            overflow: hidden;
        }
    }

    .detail {
        overflow: hidden;
        max-height: 0px;
        background: var(--surface-ground);
        border-radius: 10px;
        margin-top: 6px;
        
        .detail-content {
            padding: 10px;
        }
        
        &.loaded {
            animation: heightAnimationClose 0.7s ease forwards;                
        }

        &.open {
            animation: heightAnimation 0.7s ease forwards;
        }
    }
</style>