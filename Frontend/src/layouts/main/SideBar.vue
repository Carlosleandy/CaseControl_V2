<script lang="ts" setup>
    import Menu from './Menu.vue'

    let toggleMenuType = () => {
        const mainContainer = document.getElementById('main-container');
        const root = document.querySelector(':root') as HTMLElement;
        
        if(!root) return;
        
        mainContainer?.classList.toggle('min');
        const isMin = mainContainer?.classList.contains('min');
        const menuType = !isMin ? '--max-menu-width': '--min-menu-width';

        const menuWidth = getComputedStyle(root).getPropertyValue(menuType);
        root.style.setProperty('--menu-width', menuWidth);
    }
</script>

<template>
    <div class="container">        
        <Menu />
    </div>
</template>


<style lang="scss" scoped>

    .container {
        position: relative;
    }

    .layout-sidebar-anchor {
        position: absolute;
        right: 26px;
        top: 5px;
        width: 1.15rem;
        height: 1.15rem;
        border-radius: 50%;
        border: 2px solid var(--divider-color);
        background-color: var(--primary-light-color);
        outline: none;
        transition: background-color var(--transition-duration),transform .3s;
        display: block;
        animation: px-fadein .15s linear;
        border: 2px solid var(--primary-light-color);
    }
    
    .min {
        .layout-sidebar-anchor {
            background-color: transparent;
        }
    } 

</style>