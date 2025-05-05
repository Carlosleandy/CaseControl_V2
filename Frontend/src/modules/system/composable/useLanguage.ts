import { set as setStore, get as gesStore } from '@/modules/shared/utility/localStorage'
import { ref } from 'vue'

export const useLanguage = (globalProperties: any) => {
	const currentLang = ref(gesStore('lang') || 'en');
	
    const changeLanguage = (language: 'es' | 'en') => {
		const languageChage = globalProperties.changeLanguage;
		if(!languageChage) return;

		const lastLoadedLang = gesStore('loaded_lang');
		currentLang.value = language;

		if(lastLoadedLang) {
			loadLanguageKeys(JSON.parse(lastLoadedLang));
		}
		
		languageChage(language);
		setStore('lang', language);
    }

	const loadLanguageKeys = async (keys: string[]) => {
		const addLanguageKeys = globalProperties.addLanguageKeys;
		for(const path of keys)
			addLanguageKeys(currentLang.value, path);

		setStore('loaded_lang', JSON.stringify(keys));
    }
	
    return {
		currentLang,
        changeLanguage,
		loadLanguageKeys
    }
}
