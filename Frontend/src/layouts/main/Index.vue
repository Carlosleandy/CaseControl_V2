<script setup lang="ts">
    import { ref, onMounted } from 'vue'
    import SideBar from './SideBar.vue'
    import TemplateHeader from './TemplateHeader.vue'
    import 'primeicons/primeicons.css'
    import Button from 'primevue/button'
    import { useTheme } from '@/modules/system/composable/useTheme'
    import Toast from 'primevue/toast';
    import { useNotifications } from '@/modules/system/composable/useNotifications'
    import { registerEvent } from '@/modules/shared/utility/eventHandler';

    const { currentTheme, getMenuType, setMenuType } = useTheme();
    const { show } = useNotifications();

    onMounted(() => {
        let header = document.getElementById('main-header');

        document.addEventListener('scroll', function() {
            if(!header) return;
            
            let scrollPos = window.scrollY;

            if ( scrollPos > 20 ) {
                header.classList.add('blur');
            } else {
                header.classList.remove('blur');
            }            
        });
        
        const isMin = getMenuType() === 'min';
        if(isMin) {
            const mainContainer = document.getElementById('main-container');
            mainContainer?.classList.add('min');
        }
        
        changeMenuType(isMin);
        registerEvent('SYSTEM_MESSAGE', show, true);
    });

    const changeMenuType = (isMin: boolean) => {
        const root = document.querySelector(':root') as HTMLElement;        
        if(!root) return;
        
        const menuType = !isMin ? '--max-menu-width': '--min-menu-width';

        const menuWidth = getComputedStyle(root).getPropertyValue(menuType);
        root.style.setProperty('--menu-width', menuWidth);
        setMenuType(isMin ? 'min': '');   
    }

    let toggleMenuType = () => {
        const mainContainer = document.getElementById('main-container');        
        mainContainer?.classList.toggle('min');
        const isMin = mainContainer?.classList.contains('min');

        changeMenuType(!!isMin);      
    }
    
    let showMenu = ref(false);
</script>

<template>
    <div id="main-container" :class="'layout-' + currentTheme ">
        <header id="main-header">
            <Toast />
            <div>
                <Button id="toggleMobileMenu" icon="pi pi-bars" text aria-label="toogle menu" @click="showMenu = !showMenu" />            
                <Button id="toggleDesktopMenu" icon="pi pi-bars" text aria-label="toogle menu" @click="toggleMenuType" />            
            </div>

            <div class="right">                
                <TemplateHeader />
            </div>
        </header>
        <main>
            <aside :class="{show: showMenu}">
                <SideBar class="side-bar"></SideBar>
            </aside>
            <div class="main-content">
                <RouterView />
            </div>
        </main>
        <footer>
    
        </footer>
        <div class="shadow" @click="showMenu = false" :class="{show: showMenu}"></div>
    </div>
</template>

<style lang="scss">
    header {
        width: 100%;
        height: 45px;
        display: flex;
        justify-content: space-between;
        position: sticky;
        top: 0;
        z-index: 9;
        padding-left: var(--menu-width);
        transition: padding-left 0.5s;

        &.blur {
            background: var(--header-blur-color);
            backdrop-filter: blur(4px);     
        }
    }

    body {
        background: var(--surface-ground);
    }

    aside {
        width: var(--menu-width);
        position: fixed;
        height: 100%;
        top: 0;
        background: var(--menu-bg);
        transition: all 0.5s;
        z-index: 99;
        &:hover {
            width: 16rem;

            .sub-menu {
                padding-left: 10px !important;                
            }

            ul.layout-menu {
                overflow-y: auto !important;
            }

            span.layout-menuitem-text {
                opacity: 1 !important;
            }
        }
    }

    .main-content {
        margin-left: var(--menu-width);
        transition: margin 0.5s;
    }

    .card {
        background: var(--surface-card);
        box-sizing: border-box;
        padding: 2rem;
        margin-bottom: 2rem;
        box-shadow: var(--card-shadow);
        border-radius: 24px;
    }

    .main-content {
        padding: 10px;
    }   


    .min {
        .sub-menu {
            padding-left: 0px !important;
            padding-right: 0px !important;
        }
    }

    .side-bar {
        display: flex;
        flex-direction: column;
        height: 100%;
    }

    .shadow {
        display: none;
        position: fixed;
        left: 0;
        top: 0;
        width: 100%;
        height: 100vh;
        background:hsl(0deg 0% 0% / 18.04%);;
        z-index: 1;

        &.show {
            display: block;
        }
    }
    
    .right {
        z-index: 2;
    }

    #toggleDesktopMenu {
        margin-left: 10px;
    }
</style>