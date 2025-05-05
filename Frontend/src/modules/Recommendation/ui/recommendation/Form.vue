<script setup lang="ts">
    import type { RecommendationRepository } from '../../domain/repository/recommendationRepository'
    import { useRecommendation } from '../../composable/recommendationComposable'
import { useDataRecommendation } from "../../../Recommendation/repository/recommendationRepository"
import { Recommendation } from '../../domain/model/recommendation'
import { ref } from "vue";

import { useDataRecommendationStatus } from "../../../RecommendationStatus/repository/recommendationstatusRepository";
import { useDataRecommendationType } from "../../../RecommendationType/repository/recommendationtypeRepository";
import { useAllCostCenters } from "../../../shared/utility/CostCenter"

const { allCostCenters, getAllCostCenters } = useAllCostCenters();
const { recommendationStatusList, getAllRecommendationStatus } = useDataRecommendationStatus();
const { recommendationTypeList, getAllRecommendationTypes } = useDataRecommendationType();

    const props = defineProps<{
		recommendationRepository: RecommendationRepository,
        recId?: number,
        cloning?: boolean,
        caseId: number
	}>();

    const { getByID, recommendationSave } = useDataRecommendation();

    const { 
        recommendationRecord,
		saveRecommendation,
        getRecommendationById,
        setRecommendationData,
	} = useRecommendation({
        recommendationRepository: props.recommendationRepository
    });	

    const loadInitialData = () => {
        setRecommendationData({} as Recommendation);

        if(props?.recId) {
            // getRecommendationById(props.recId).then((recommendation) => {
            //     if(recommendation) {
            //         if(props.cloning) {
            //             recommendation.id = 0;
            //         }
                    
            //         setRecommendationData(recommendation);
            //     }
            // });

            const recom= getByID(props?.recId).then((recommendation)=>{
               
                    setRecommendationData(recommendation);
               
            });
        }
    }

    const save = async () => {
        if(recommendationRecord) {
            await recommendationSave(recommendationRecord.value);
        }
    }

    getAllCostCenters();
    getAllRecommendationStatus();
    getAllRecommendationTypes();
    loadInitialData();

    recommendationRecord.value.caseID=props.caseId;

    if(props?.recId == undefined || props?.recId == 0){
        recommendationRecord.value.recommendationStatusID=1;
    } 

    defineExpose({ saveRec: save});
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <div class="flexbox-grid container">
                        <div class="grid">
                            <span class="p-float-label">
                                <InputText v-model="recommendationRecord.id" disabled />
                                <label for="id">{{ $t('ID') }}</label>
                            </span>
                        </div>
                         <div class="grid">
                            <span class="p-float-label">
                                <Dropdown
                                v-model="recommendationRecord.recommendationStatusID"
                                :options="recommendationStatusList.data"
                                filter
                                optionValue="id"
                                optionLabel="name"
                                :disabled="recommendationRecord.id==0 || recommendationRecord.id==null"
                            />
                        <label for="recommendationStatusID">{{$t("Estado")}}</label>
                    </span>
                        </div>

                        <div class="grid">
                            <span class="p-float-label">
                                <Dropdown
                                v-model="recommendationRecord.recommendationTypeID"
                                :options="recommendationTypeList.data"
                                filter
                                optionValue="id"
                                optionLabel="name"
                                :disabled="recommendationRecord.id > 0"
                            />
                        <label for="recommendationTypeID">{{$t("Tipo de Recomendación")}}</label>
                    </span>
                        </div>

                    </div>
                     <div class="flexbox-grid container">
                        <div class="grid">
                    <span class="p-float-label">
                        <InputText v-model="recommendationRecord.title" :disabled="recommendationRecord.id > 0" />
                        <label for="title">{{ $t('Título') }}</label>
                    </span>
                        </div>
                    </div>

                    <div class="flexbox-grid container">
                        <div class="grid">
                         
                        <span class="p-float-label">
                            <Dropdown
                            v-model="recommendationRecord.unitToWhichItIsAddressed"
                            :options="allCostCenters.data"
                            optionValue="center"
                            optionLabel="full_Description"
                            filter
                            :disabled="recommendationRecord.id > 0"
                            />
                            <label for="recommendationRecord.unitToWhichItIsAddressed">{{$t("Unidad a la que va dirigida")}}</label>
                        </span>

                        </div>
                        <div class="grid">
                            <span class="p-float-label">
                                <InputText v-model="recommendationRecord.contact" :disabled="recommendationRecord.id > 0" />
                                <label for="contact">{{ $t('Contacto') }}</label>
                            </span>
                        </div>
                    </div>
                    
                    <div class="flexbox-grid container">
                        <div class="grid">
                    <span class="p-float-label">
                        <TextArea v-model="recommendationRecord.response" :disabled="recommendationRecord.id==0 || recommendationRecord.id==null" />
                        <label for="response">{{ $t('Respuesta') }}</label>
                    </span>
                        </div>
                    </div>                    
                  
                    <span class="p-float-label">
                        <InputText v-model="recommendationRecord.caseID" type="hidden" />
                    </span>
                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>