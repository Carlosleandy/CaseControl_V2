import { set as setStore, get as getStore } from '@/modules/shared/utility/localStorage'
import { ref } from 'vue'
import { useSystemStore } from '@/modules/system/store/system'

function loadDynamicCss(cssFilePath: string) {
    const linkElement = document.getElementById('dynamic-css');

    if (linkElement) {
        linkElement.setAttribute('href', cssFilePath);
    } else {
        const newLinkElement = document.createElement('link');
        newLinkElement.setAttribute('id', 'dynamic-css');
        newLinkElement.setAttribute('rel', 'stylesheet');
        newLinkElement.setAttribute('type', 'text/css');
        newLinkElement.setAttribute('href', cssFilePath);
        document.head.appendChild(newLinkElement);
    }
}

export const useTheme = () => {
    const currentTheme = ref(getStore('theme') || 'light');
    const menuType = ref(getStore('isMin') || '');

    const updateThemeStore = (theme: string) => {
        const store = useSystemStore();
        store.setTheme(theme);
    }
	
    const toggleTheme = () => {
        const container = document.getElementById('main-container');
        const theme = currentTheme.value === 'light' ? 'dark' : 'light';
        container?.classList.remove('layout-' + currentTheme.value);
        container?.classList.add('layout-' + theme);
        
        loadDynamicCss(`./css/${theme}-theme.css`);
        currentTheme.value = theme;
		setStore('theme', theme);
        updateThemeStore(theme);
    }

    const loadTheme = (theme: string) => {        
        loadDynamicCss(`./css/${theme}-theme.css`);
        currentTheme.value = theme;
        updateThemeStore(theme);
    }

    const setMenuType = (menuType: string) => {
        setStore('menuType', menuType);
    }

    const getMenuType = () => {
        return getStore('menuType') || '';
    }

    return {
		currentTheme,
        toggleTheme,
        loadTheme,
        getMenuType,
        setMenuType
    }
}
