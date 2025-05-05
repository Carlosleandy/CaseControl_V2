<script setup lang="ts">
    import type { BinnacleRepository } from '../../domain/repository/binnacleRepository'
    import { useBinnacle } from '../../composable/binnacleComposable'
    import { useDataBinnacle } from "../../../Binnacle/repository/binnacleRepository"
    import { Binnacle } from '../../domain/model/binnacle'

    const props = defineProps<{
		binnacleRepository: BinnacleRepository,
        binId?: number,
        cloning?: boolean,
        caseId: number
	}>();

    const { 
        binnacleRecord,
		saveBinnacle,
        getBinnacleById,
        setBinnacleData,
	} = useBinnacle({
        binnacleRepository: props.binnacleRepository
    });	

    const { binnacleSave } = useDataBinnacle();

    const loadInitialData = () => {
       setBinnacleData({} as Binnacle);

        if(props?.binId) {          
            // const recom= getByID(props?.binId).then((binnacle)=>{
            //     setBinnacleData(binnacle);
            // });
        }
    }

    const save = async () => {
        if(binnacleRecord) {
            await binnacleSave(binnacleRecord.value);
        }
    }

    loadInitialData();

      binnacleRecord.value.caseID=props.caseId;

    defineExpose({ saveBin: save });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="binnacleRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <TextArea v-model="binnacleRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>

                    <span class="p-float-label">
                        <InputText v-model="binnacleRecord.caseID" type="hidden" />
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>