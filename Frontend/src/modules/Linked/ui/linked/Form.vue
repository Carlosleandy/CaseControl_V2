<script setup lang="ts">
    import type { LinkedRepository } from '../../domain/repository/linkedRepository'
    import { useLinked } from '../../composable/linkedComposable'
    import { useDataLinkedType } from "../../repository/linkedRepository"
    import { useDataEmployee } from "../../../shared/utility/employees"
    import type { CaseInvestigation } from "../../../CaseInvestigation/domain/model/caseinvestigation"
    import { ref } from "vue";
import caseinvestigationRepository from '../../../CaseInvestigation/repository/caseinvestigationRepository';
import type { Linked } from "../../domain/model/linked"
import type { Employee } from "../../../shared/utility/employees"

    const { linkedTypeList, getAllLinkedType, addLinked } = useDataLinkedType();
    const { employee, getEmployee } = useDataEmployee();

const linkedRec = ref({} as Linked);
const ident = ref('');
const employeeCharged = ref({} as Employee);

    const props = defineProps<{ 
		linkedRepository: LinkedRepository,
        caseInvestigation?: CaseInvestigation,
        recordId?: number,
	}>();

    const { 
        linkedRecord,
		saveLinked,
        getLinkedById,
        setLinkedData,
	} = useLinked({
        linkedRepository: props.linkedRepository
    });	

    const loadInitialData = () => {debugger;
        if(props?.recordId) {
            getLinkedById(props.recordId).then((linked) => {
                if(linked) {                   
                    setLinkedData(linked);
                }
            });
        }
        else{
            linkedRec.value.id=props.recordId;
            linkedRec.value.caseID = props.caseInvestigation.id;
            setLinkedData(linkedRec.value);
        }
    }

const setDataEmployee = (emp: Employee) => { 
        linkedRecord.value.code=emp.data.codigo;
        linkedRecord.value.name=emp.data.nombre;
        linkedRecord.value.lastName=emp.data.apellido;
        linkedRecord.value.identification=emp.data.identificacion;
        linkedRecord.value.birthdate=emp.data.fecha_de_Nacimiento;
        linkedRecord.value.address=emp.data.address;
    };

    const getEmployeeByIdent = async() => { debugger;
        await getEmployee(ident.value);
        if(employee){
            setDataEmployee(employee.value);
        }
    };

    const save = async() => { 
        addLinked(linkedRecord.value);
    };

    getAllLinkedType();
    loadInitialData();
    defineExpose({ saveLinked: save });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

<div class="buttons-toolbar">
    <span class="p-float-label">
                        <InputText v-model="ident" />
                        <label for="ident">{{ $t('Código o Identificación') }}</label>
                    </span>
                    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.search')" @click="getEmployeeByIdent()" icon="pi pi-search" severity="success" aria-label="User" />
                </div>

  <span class="p-float-label">
                            <InputText v-model="linkedRecord.id" disabled hidden />
                            
                        </span>

<div class="flexbox-grid container">
        <div class="grid">
          <span class="p-float-label">
                        <Dropdown
                        v-model="linkedRecord.linkTypeID"
                        :options="linkedTypeList.data"
                        optionValue="id"
                        optionLabel="name"
                        filter
                        />
                        <label for="linkedRecord.linkTypeID">{{$t("Tipo Vinculado")}}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="linkedRecord.code" />
                        <label for="code">{{ $t('Código') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="linkedRecord.name" />
                        <label for="name">{{ $t('Nombre') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="linkedRecord.lastName" />
                        <label for="lastName">{{ $t('Apellido') }}</label>
                    </span>
        </div>
        <div class="grid">
            <span class="p-float-label">
                        <Calendar v-model="linkedRecord.birthdate" dateFormat="dd/mm/yy" />
                        <label for="birthdate">{{ $t('Fecha Nacimiento') }}</label>
                    </span>
          <span class="p-float-label">
                        <InputText v-model="linkedRecord.identification" />
                        <label for="identification">{{ $t('Identificación') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="linkedRecord.phone" />
                        <label for="phone">{{ $t('Teléfono') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="linkedRecord.cellPhone" />
                        <label for="cellPhone">{{ $t('Celular') }}</label>
                    </span>
        </div>
      </div>







                  
                    
                   
                    
                    
                    
                    
                    

                    <span class="p-float-label">
                        <InputText v-model="linkedRecord.address" />
                        <label for="address">{{ $t('Dirección') }}</label>
                    </span>
                    
                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>