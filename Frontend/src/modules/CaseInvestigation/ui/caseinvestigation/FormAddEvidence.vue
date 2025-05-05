<script setup lang="ts">
import { ref, defineComponent } from "vue";
import type { CaseInvestigation, Evidence, FileUploadResponse } from "../../domain/model/caseinvestigation"
import { useDataCases } from "../../repository/caseinvestigationRepository"
import { useDataEvidenceClassification } from "../../../EvidenceClassification/repository/evidenceclassificationRepository"
import { useConfirm } from 'primevue/useconfirm'

import FileUpload from 'primevue/fileupload';
import axios from 'axios';

const evidenceRecord = ref({} as Evidence);
let fileNameSelected = ref('');

const { addCaseEvidence } = useDataCases();
const { classificationList, getAllClassifications } = useDataEvidenceClassification();

const props = defineProps<{
  caseInvestigation?: CaseInvestigation;
}>();

const loadInitialData = () => { debugger;
  if (props?.caseInvestigation) {
   evidenceRecord.value.caseID = props.caseInvestigation.id;
  }
};

    const file = ref<File | null>(null); 

    const customUpload = async (event: { files: File[] }) => { debugger;
  if (!event.files || event.files.length === 0) {
    console.log('Debe seleccionar un archivo');
    return;
  }

  file.value = event.files[0];
  fileNameSelected.value = file.value.name;
};


    const save = async() => { debugger;    
if (!file || file.value.length === 0) {
        return;
      }

      if(evidenceRecord) {
        const evidenceRecordJson = JSON.stringify(evidenceRecord.value); 

        const formData = new FormData();
        formData.append('file', file.value); 
        formData.append('evidence', evidenceRecordJson); 

        addCaseEvidence(formData);
      }
    };

loadInitialData();
getAllClassifications();
defineExpose({ saveEvidence: save });

</script>

<template>
  <form ref="formRef">
     

 <div class="flexbox-grid container">
    <div class="card" style="--min: 50ch">
        <div class="grid">
        
      <h2>{{ $t("SHARED.basic_information") }}</h2>

 <div>
        <h3>Cargar Archivo</h3>

          <FileUpload 
            mode="basic" 
            name="demo[]" 
            :auto="true"
            customUpload 
            @uploader="customUpload" 
            chooseLabel="Cargar Archivo"
          />
          <p>{{fileNameSelected}}</p>

      </div>

      <div class="flexbox-grid container">
        <div class="grid">
          <span class="p-float-label">
            <Dropdown
              v-model="evidenceRecord.evidenceClassificationID"
              :options="classificationList.data"
              filter
              optionValue="id"
              optionLabel="name"
              readonly 
            />
            <label for="evidenceRecord.evidenceClassificationID">{{$t("Clasificación de Evidencia")}}</label>
          </span>
          <span class="p-float-label">
            <InputText v-model="evidenceRecord.name" />
            <label for="name">{{$t("Nombre")}}</label>
          </span>
          <span class="p-float-label">
            <TextArea v-model="evidenceRecord.description" />
            <label for="description">{{ $t("Descripción") }}</label>
          </span>
        </div>
      </div>

    </div>
    </div>
    </div>

  </form>
</template>

<style scoped>
</style>


