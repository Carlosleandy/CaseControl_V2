import { createI18n } from 'vue-i18n'
import { registerEvent } from '@/modules/shared/utility/eventHandler'
import { get as gesStore } from '@/modules/shared/utility/localStorage'

import en from './default/en'
import es from './default/es'

const loadedLangs: Record<string, boolean> = {};

export const i18n = createI18n({
    locale: gesStore('lang') || 'en',
    legacy: false,
    globalInjection: true,    
    messages: {
        en, es
    }
});

const addLanguageKeys = async (language: string, path: string): Promise<void> => {
	const langKey = language + path;

    if(loadedLangs[langKey]) return;
    
    const response = await fetch(`./lang/${path}/${language}.json`);
    const keys = await response.json();

    i18n.global.mergeLocaleMessage(language, keys);
    loadedLangs[langKey] = true;
}

const onLanguageChange = (lang: 'es' | 'en') => {
    i18n.global.locale.value = lang;
}

const useInternalization = (app: any) => {
    app.use(i18n);
    app.config.globalProperties.addLanguageKeys = addLanguageKeys;
    app.config.globalProperties.changeLanguage = onLanguageChange;

    registerEvent('LANGUAGE_CHANGE',  onLanguageChange);
}

export default useInternalization;
