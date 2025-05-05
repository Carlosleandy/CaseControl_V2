<script setup lang="ts">
    import type { InterviewRepository } from '../../domain/repository/interviewRepository'
    import { useInterview } from '../../composable/interviewComposable'
import type { CaseInvestigation } from "../../../CaseInvestigation/domain/model/caseinvestigation"
import { useDataInterview } from "../../repository/interviewRepository"
import { useDataLinkedType } from "../../../Linked/repository/linkedRepository"

    const props = defineProps<{
		interviewRepository: InterviewRepository,
        caseInvestigation?: CaseInvestigation,
        recordId?: number
	}>();

    const { 
        interviewRecord,
		saveInterview,
        getInterviewById,
        setInterviewData,
	} = useInterview({
        interviewRepository: props.interviewRepository
    });	

    const { intervieweeTypeList, getIntervieweeTypes, addInterview } = useDataInterview();
    const { linkedList, getAllLinkeds } = useDataLinkedType();

    const loadInitialData = () => {
        if(props?.recordId) {
            getInterviewById(props.recordId).then((interview) => {
                if(interview) {                    
                    setInterviewData(interview);
                }
            });
        }

        interviewRecord.value.caseID=props.caseInvestigation.id;
    }

    const save = async() => { 
        addInterview(interviewRecord.value);
    };

    getIntervieweeTypes();
    getAllLinkeds();
    loadInitialData();
    defineExpose({ saveInterview: save });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="interviewRecord.id" disabled hidden/>
                    </span>
                    <span class="p-float-label">
                        <Dropdown
                        v-model="interviewRecord.intervieweeTypeID"
                        :options="intervieweeTypeList.data"
                        optionValue="id"
                        optionLabel="name"
                        filter
                        />
                        <label for="interviewRecord.intervieweeType.name">{{$t("Tipo Entrevistado")}}</label>
                    </span>   
                    <span class="p-float-label">
                        <Dropdown
                        v-model="interviewRecord.linkedID"
                        :options="linkedList.data"
                        optionValue="id"
                        optionLabel="fullName"
                        filter
                        />
                        <label for="interviewRecord.linked.fullName">{{$t("Vinculado")}}</label>
                    </span>  
                    <span class="p-float-label">
                        <Calendar v-model="interviewRecord.dateInterview" dateFormat="dd/mm/yy" />
                        <label for="birthdate">{{ $t('Fecha Entrevista') }}</label>
                    </span>             
                    <span class="p-float-label">
                        <TextArea
                            v-model="interviewRecord.description"
                            variant="filled"
                            rows="4"
                        />
                        <label for="description">{{ $t('Descripción') }}</label>
                    </span>                   

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>