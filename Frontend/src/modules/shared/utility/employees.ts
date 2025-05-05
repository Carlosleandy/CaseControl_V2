import { get, getOnly, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { ref } from 'vue'

export type Employee  =  {
    id: number;
    codigo: string;
    nombre: string;
    apellido: string;
    nombre_Completo: string;
    usuario: string;
    correo: string;
    identificacion: string;
    iD_de_Puesto: string;
    puesto: string;
    tipo_de_Empleado: string;
    gerencia: string;
    ubicacion: string;
    empresa: string;
    fecha_de_Nacimiento: Date;
    fecha_de_Ingreso: Date;
}

export const useDataEmployee = () => {
    const employee = ref({} as Employee);
   
    async function getEmployee (ident: string) {
        try {
            employee.value = await getOnly(DEFAULT_API_PATH + '/Util/GetEmployees/' + ident);     
            return employee;       
        } catch (error: any) {
            throw new Error('Se ha presentado un eror: ' + error.message);
        }
    }
    return {
        employee,
        getEmployee
    }
}


