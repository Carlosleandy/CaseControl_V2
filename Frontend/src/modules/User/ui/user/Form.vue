<script setup lang="ts">
    import type { UserRepository } from '../../domain/repository/userRepository'
    import { useUser } from '../../composable/userComposable'

import { useDataWorkingGroup } from "../../../WorkingGroup/repository/workingGroupRepository";
import { useDataUserLevel } from "../../../UserLevel/repository/userLevelRepository";

import {ref} from 'vue';

const { workingGroupList, getAllWorkingGroups } = useDataWorkingGroup();
const { userLevelList, getAllUserLevels } = useDataUserLevel();

let isActive= ref();
let isAdmin= ref();

    const props = defineProps<{
		userRepository: UserRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        userRecord,
		saveUser,
        getUserById,
        setUserData,
	} = useUser({
        userRepository: props.userRepository
    });	

    const loadInitialData = () => {
        if(props?.recordId) {
            getUserById(props.recordId).then((user) => {
                if(user) {
                    if(props.cloning) {
                        user.id = 0;
                    }
                    
                    setUserData(user);
                }
            });
        }
    }

    getAllWorkingGroups();
    getAllUserLevels();
    loadInitialData();
    defineExpose({ save: saveUser });
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
                        <InputText v-model="userRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                        </div>
                         <div class="grid">
                             
 <div class="flex align-items-center">
                     <input type="checkbox" v-model="userRecord.isActive" :value="isActive" />
                            <label for="isActive">{{ $t('Está Activo?') }}</label>
                             </div>

                         </div>
                          <div class="grid">
                    <div class="flex align-items-center">
                     <input type="checkbox" v-model="userRecord.isAdmin" :value="isAdmin" />
                            <label for="isAdmin">{{ $t('Es Administrador?') }}</label>
                        </div>
                        </div>
                    </div>

                    <div class="flexbox-grid container">
                        <div class="grid">
                    
                    <span class="p-float-label">
                        <InputText v-model="userRecord.userName" />
                        <label for="userName">{{ $t('Nombre de Usuario') }}</label>
                    </span>

                    
                    <div class="grid">
                            <span class="p-float-label">
                                <Dropdown
                                v-model="userRecord.workingGroupID"
                                :options="workingGroupList.data"
                                filter
                                optionValue="id"
                                optionLabel="name"
                            />
                        <label for="workingGroupID">{{$t("Grupo de Trabajo")}}</label>
                    </span>
                        </div>

                        <div class="grid">
                            <span class="p-float-label">
                                <Dropdown
                                v-model="userRecord.userLevelID"
                                :options="userLevelList.data"
                                filter
                                optionValue="id"
                                optionLabel="name"
                            />
                        <label for="userLevelID">{{$t("Nivel de Usuario")}}</label>
                    </span>
                        </div>
                         </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>