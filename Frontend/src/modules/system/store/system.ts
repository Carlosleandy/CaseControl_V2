import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useSystemStore = defineStore('system', () => {
    const theme = ref("");

    function setTheme(systemTheme: string) {
        theme.value = systemTheme;
    }

    return { 
        theme,
        setTheme
    }
})
