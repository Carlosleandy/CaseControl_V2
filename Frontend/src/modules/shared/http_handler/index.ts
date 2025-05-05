import axios from "axios";
import { useAuthStore } from "@/modules/auth/store/auth";
import { useAuth } from "@/modules/auth/composable/useAuth";

export const DEFAULT_API_PATH = import.meta.env.VITE_API_URL;

export const get = (url: string, params: Record<string, unknown> = {}): Promise<any> => {
    return axios.get<Response>(url, params);
}

export const getOnly = (url: string): Promise<any> => {
    return axios.get<Response>(url);
}

export const post = (url: string, data: Record<string, unknown>): Promise<any> => {
    return axios.post(url, data);
}

export const put = (url: string, data: Record<string, unknown>): Promise<any> => {
    return axios.put(url, data);
}

export const patch = (url: string, data: Record<string, unknown>): Promise<any> => {
    return axios.patch(url, data);
}

export const deleteRecord = (url: string): Promise<any> => {
    return axios.delete(url);
}

export const postFormData = (url: string, data: any): Promise<any> => {
    const formData = new FormData();
    const files = data['file_object'] ? data['file_object'].files : [];

    for(let i = 0; i < files.length; i++){
        formData.append("file", files[i]);
    }

    for(const d in data) {
        if(d != 'file_object')
            formData.append(d, data[d]);
    }

    return axios.post(url, formData, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
}

axios.interceptors.request.use((config) => {
    const system = useAuthStore();
    const user_token = system.token;
	if(!user_token) return config;
    
    config.headers.Authorization =  `Bearer ${user_token}`;
    return config;
}, (error) => {
	return Promise.reject(error);
});

axios.interceptors.response.use(
    response => response,
    error => {
        if(error.response.status === 401) {
            const auth = useAuth();
            auth.logout();
        }
    });