<script setup lang="ts">
    import type { RoleRepository } from '../../domain/repository/roleRepository'
    import { useRole } from '../../composable/roleComposable'
    import { useDataRole } from '../../repository/roleRepository'

    const { getRolByID, saveRole } = useDataRole();

    const props = defineProps<{
		roleRepository: RoleRepository,
        recordId?: number,
        cloning?: boolean
	}>();

    const { 
        roleRecord,
		// saveRole,
        getRoleById,
        setRoleData,
	} = useRole({
        roleRepository: props.roleRepository
    });	

    const loadInitialData = async () => {
        if(props?.recordId) {
            // getRoleById(props.recordId).then((role) => {
            //     if(role) {
            //         if(props.cloning) {
            //             role.id = 0;
            //         }
                    
            //         setRoleData(role);
            //     }
            // });

            const rol = await getRolByID(props.recordId);
            if(rol){
                setRoleData(rol);
            }
        }
    }

    const save = async () => {
        if(roleRecord) {
            await saveRole(roleRecord.value);
        }
    }

    loadInitialData();
    defineExpose({ save: save });
</script>

<template>
    <form ref="formRef">
        <div class="flexbox-grid container">
            <div class="card" style="--min: 50ch">
                <div class="grid">
                    <h2>{{  $t('SHARED.basic_information') }}</h2>

                    <span class="p-float-label">
                        <InputText v-model="roleRecord.id" disabled />
                        <label for="id">{{ $t('ID') }}</label>
                    </span>
                    <span class="p-float-label">
                        <InputText v-model="roleRecord.name" />
                        <label for="name">{{ $t('Descripción') }}</label>
                    </span>

                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    
</style>