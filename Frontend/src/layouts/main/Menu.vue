<script lang="ts" setup>
    import { useAuthStore } from '@/modules/auth/store/auth'
    import { computed, onMounted } from 'vue'
    import { useRoute } from 'vue-router'

    const system = useAuthStore();
    const route = useRoute();

    const path = computed(() => route.path);

    let toggleMenu = (e: any): void => {
        const listItem = e.target.closest('li');
        const classList =listItem.classList;

        if(classList.contains('open')) {
            classList.remove('open')
            return;
        }
        
       listItem.classList.add('open');       
    }

    onMounted(() => {
        const element = document.querySelectorAll('li.active');
    
        if(element && element[0]) {
            element[0].closest('li.main-option')?.classList.add('open');
        }
    });

</script>

<template>
    <div>
        &nbsp;
    </div>
    <ul role="menu" class="layout-menu">
        <li v-for="(menu, index) in system.systemMenu" :key="index" class="main-option">
            <div v-on:click="toggleMenu" v-if="menu?.children.length > 0">
                <i v-if="!menu.svg_icon" class="pi" :class="menu.icon"></i>
                <i v-if="menu.svg_icon" v-html="menu.svg_icon" ></i>
                <span class="layout-menuitem-text">{{ $t(menu.name) }}</span>
                <i class="pi pi-fw pi-chevron-down toggler"></i>                
            </div>

            <div v-if="menu.children.length === 0">
                <a :href="menu.path" class="p-ripple" exact="">
                    <i v-if="!menu.svg_icon" class="pi" :class="menu.icon"></i>
                    <i v-if="menu.svg_icon" v-html="menu.svg_icon" ></i>
                    <span class="layout-menuitem-text">{{  $t(menu.name) }}</span>
                    <span class="p-ink" role="presentation" aria-hidden="true"></span>
                </a>             
            </div>

            <ul role="menu" class="sub-menu">
                <li class="" role="menuitem" v-for="(subMenu, index2) in menu.children" :key="index2" :class="{active: '/#' + path == subMenu.path}">
                    <a :href="subMenu.path" class="p-ripple" exact="">
                        <i v-if="!subMenu.svg_icon" class="pi" :class="subMenu.icon"></i>
                        <i v-if="subMenu.svg_icon" v-html="subMenu.svg_icon" ></i>
                        <span class="layout-menuitem-text">{{  $t(subMenu.name) }}</span>
                        <span class="p-ink" role="presentation" aria-hidden="true"></span>
                    </a>
                </li>
            </ul>            
        </li>
    </ul>
</template>

<style lang="scss" scoped>
    ul {
        list-style: none;
        width: 100%;
        overflow: hidden;
        overflow-y: auto;
        padding: 10px;
        padding-left: 15px;

        li div {
            cursor: pointer;
        }

        li.active a{
            color: var(--primary-600);
        }
    }

    a {
        color: var(--root-menuitem-text-color);
        width: 100%;
        transition: all .1s;

    }
    
    svg {
        color: var(--root-menuitem-text-color) !important;
    }

    ul span.layout-menuitem-text {
        opacity: 1;
        white-space: nowrap;
        transition: all .1s;
    }

    .min ul  {
        overflow-y: hidden;
        
        span {
            opacity: 0;
        }
    }

   .sub-menu li {
        cursor: pointer;
        i, span, div {
            pointer-events: none; 
            -webkit-user-select: none; -moz-user-select: none; -ms-user-select: none; 
            user-select: none;
        }
        .pi-chevron-down {
            transition: transform 0.5s;
        }
    }
    
    li div, .sub-menu li a {
        display: flex;        
        gap: 10px;
        padding: 10px;
        align-items: center;        
        span {
            flex: 1;
            font-weight: 600;
            font-size: 12px;
            line-height: 14px;            
        }

        a {
            display: flex;
            gap: 10px;
            align-items: center;
        }
    }

    li div {
        padding: 0;
    }

    .sub-menu li a {
        gap: 10px;
        justify-content: flex-start;
        text-decoration: none;
        color: var(--menuitem-text-color);
        &:hover {
            color: var(--primary-600);
        }
    }

    li i {
        font-size: 16px;
    }

    li div i{
        font-size: 24px;
    }

    i.toggler {
        font-size: 14px;
    }

    li.open {
        ul {
            transition: max-height 1s ease; 
            max-height: 100vh;
        }

        .pi-chevron-down {
            transform: rotate(-180deg);
        }        
    }
    
    ul.sub-menu {
        max-height: 0;
        overflow: hidden;         
        padding-left: 10px;
        transition: max-height .5s ease, padding 0.5s ease; 
    }

    // ul span.layout-menuitem-text[data-v-792beb1c] {
    // white-space: nowrap;
    // transition: all 0.1s;
    // color: #ffff;
    // }

    // i.toggler[data-v-792beb1c] {
    // font-size: 14px;
    // color: #f89522;
    // }

    // .pi-shield:before {
    // content: "\e9b9";
    // color: #f89522;
    // }

    // .pi-microsoft:before {
    // content: "\e988";
    // color: #f89522;
    // }

    
</style>