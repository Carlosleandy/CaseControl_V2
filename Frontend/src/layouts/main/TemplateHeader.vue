<script setup lang="ts">
    import Menu from 'primevue/menu'
    import { computed, getCurrentInstance, ref } from 'vue'
    import { useAuth } from '@/modules/auth/composable/useAuth'
    import { useAuthStore } from '@/modules/auth/store/auth'
    import { useLanguage } from '@/modules/system/composable/useLanguage'
    import authRepository from '@/modules/auth/repository/authRepository'
    import { useTheme } from '@/modules/system/composable/useTheme'
    import { useI18n } from 'vue-i18n';
    
    const menu = ref();
    const languageMenu = ref();
    const instance = getCurrentInstance();
    const auth = useAuth(authRepository);
    const authStore = useAuthStore();
    const { t } = useI18n();

    const { toggleTheme, currentTheme, loadTheme } = useTheme();
    const { currentLang, changeLanguage } = useLanguage(instance?.appContext.config.globalProperties);

    loadTheme(currentTheme.value);
    auth.scheduleTokenRenew();
    
    const toggle = (event: Event) => {
        menu.value.toggle(event);
    };

    const toggleLanguageMenu = (event: Event) => {
        languageMenu.value.toggle(event);
    }

    const items = ref([
        {
            label: '',
            items: [           
                { separator: true },
                {
                    label: computed(() => t('SYSTEM.logout', {test: 5})),
                    icon: 'pi pi-sign-out',
                    command: auth.logout
                },            
            ]
        }
    ]);

    const languages = ref([
        {
            items: [           
                {
                    label: 'English',
                    icon: 'en.png',
                    lang: 'en',
                    command: () => {
                        changeLanguage('en');
                    }
                },
                {
                    label: 'Español',
                    icon: 'es.png',
                    lang: 'es',
                    command: () => {
                        changeLanguage('es');
                    }
                },                            
            ]
        }
    ]);
</script>

<template>
    <div class="menu-container">        
        <Menu class="lang-menu" :model="languages" ref="languageMenu" :popup="true" aria-haspopup="true" aria-controls="overlay_menu">
            <template #item="{ item }">
                <div v-if="item.label" class="lang-option">
                    <img
                        width="40"
                        alt="Lang"
                        :src="'./img/icons/flags/' + item.icon"
                    />
                    <span>{{ item.label }}</span>
                    <i v-if="currentLang === item.lang" class="pi pi-check" style="font-size: 1.3rem"></i>            
                </div>
            </template>
        </Menu>

        <Menu :model="items" class="user-menu" ref="menu" :popup="true" aria-haspopup="true" aria-controls="overlay_menu">
            <template #start>
                <div class="avatar">
                    <i class="pi pi-user" style="font-size: 1.3rem"></i>
                    <div class="flex flex-column">
                        <span class="font-bold">{{ authStore.user.userName }}</span>
                    </div>
                </div>
            </template>
        </Menu>

        <PButton @click="toggleTheme" id="overlay_menu" text aria-label="Filter">
            <i v-if="currentTheme === 'light'" class="pi pi-moon" style="font-size: 1.3rem"></i>
            <i v-if="currentTheme === 'dark'" class="pi pi-sun" style="font-size: 1.3rem"></i>
        </PButton>

        <PButton @click="toggleLanguageMenu" id="overlay_menu" text aria-label="Filter">
            <i class="pi pi-globe" style="font-size: 1.3rem"></i>
        </PButton>

        <PButton @click="toggle" icon="pi pi-ellipsis-v" id="overlay_menu" text aria-label="Filter">
            <i class="pi pi-ellipsis-v" style="font-size: 1.3rem"></i>
        </PButton>
    </div>
</template>

<style lang="scss">
    .menu-container{
        button {
            font-size: 16px;
        }
    }

    .avatar {
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 10px;
        margin-top: 10px;
        margin-bottom: -15px;
    }

    .lang-menu {
        .p-submenu-header {
            display: none;
        }
    }

    .lang-option {
        padding: 5px 10px;
        display: flex;
        justify-content: space-between;
        gap: 15px;
        align-items: center;
        cursor: pointer;

        span {
            flex: 1;
        }
    }

    .p-menuitem-content {
        padding: 10px;

        a {
            cursor: pointer;
        }
    }

    .user-menu .p-menuitem-content{
        display: flex;
        justify-content: center;
    }
</style>@/modules/auth/composable/useAuth@/modules/auth/store/auth@/modules/system/composable/useLanguage@/modules/system/composable/useTheme