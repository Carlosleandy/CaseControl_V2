<script setup lang="ts">
    import type { UserLevelRepository } from '../../domain/repository/userlevelRepository'
    import { useUserLevel } from '../../composable/userlevelComposable'
    import { useDataRole } from '../../../Role/repository/roleRepository'

const { roleList, getAllRoles } = useDataRole();

    const props = defineProps<{
		userlevelRepository: UserLevelRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        userlevelRecord,
		saveUserLevel,
        getUserLevelById,
        setUserLevelData,
	} = useUserLevel({
        userlevelRepository: props.userlevelRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getUserLevelById(props.recordId).then((userlevel) => {
                if(userlevel) {
                    if(props.cloning) {
                        userlevel.id = 0;
                    }
                    
                    setUserLevelData(userlevel);
                }
            });
        }
    }

    getAllRoles();
    loadInitialData();
    defineExpose({ save: saveUserLevel });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="userlevelRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="userlevelRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>
                  
                    <span class="p-float-label">
            <Dropdown
              v-model="userlevelRecord.roleID"
              :options="roleList.data"
              filter
              optionValue="id"
              optionLabel="name"
            />
            <label for="userlevelRecord.roleId">{{$t("Rol")}}</label>
          </span>
                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>