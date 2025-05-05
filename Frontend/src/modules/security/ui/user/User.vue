<script setup lang="ts">
    import type { UserRepository as UserRepo } from '../../domain/repository/userRepository'
    import type { RoleRepository as RoleRepo } from '../../domain/repository/roleRepository'
    import { useUser } from '../../composable/userComposable'
    import StatusIndicator from '@/components/widgets/StatusIndicator.vue'
    import { showConfirm } from '@/modules/shared/utility/prime'
    import type { User } from '../../domain/entity/user'
    import { useConfirm } from "primevue/useconfirm"
    import ConfirmPopup from 'primevue/confirmpopup'
    import { useI18n } from 'vue-i18n'
    import UserForm from './Form.vue'
    import Filter from './Filter.vue'
    import { onMounted, ref } from 'vue'
    import { useRouter } from 'vue-router'
    
    const confirm = useConfirm();
    const i18n = useI18n();
    const router = useRouter();    
    
    const props = defineProps<{
        UserRepository : UserRepo,
        RoleRepository: RoleRepo,        
    }>();

    const userRecordId = ref('');
    const isCloningRecord = ref(false);
    const formRef = ref();
    
    const {
        loadingRecords,
        users,
        filter,
        userId,
        savingRecord,
        showFilter,
        showDialog,
        filterIsApplied,
        totalRecords,
        getAllUsers,
        toggleUserStatus
    } = useUser({
        userRepository: props.UserRepository
    });

    const save = async () => {
        savingRecord.value = true;
        const result = await formRef.value.save();        
        savingRecord.value = false;        
        if(!result) return;
        showDialog.value = false;
    }

    const openFormDialog = (record: User | null, isCloning = false) => {
        userRecordId.value = '';
        if(record) {
            userRecordId.value = record.id;
            router.push({ params: {id: record.id } });
        }

        showDialog.value = true;
        isCloningRecord.value = isCloning;
    }

    const confirmStatusChange = (event: any, user: User) => {
        const options = {
            confirm, event,
            callback: () => { toggleUserStatus(user) },
            message: i18n.t('SECURITY.' + (user.isActive ? 'confirm_unactivate': 'confirm_activate')),
            confirmText: i18n.t('SHARED.confirm'),
            cancelText: i18n.t('SHARED.cancel')
        };

        showConfirm(options);
    }

    onMounted(() => {
        const route = router.currentRoute.value;

        if(route.params.id) {
            userRecordId.value = route.params.id.toString();
            showDialog.value = true;
        }
    });

    const onDialogClose = () => {
        router.push({path: '/users'});
    }
    
    getAllUsers();
</script>

<template>
    
    <ContentWrapper @onContentFinish="getAllUsers" :searching="loadingRecords">        

        <template #desktop>
            <div class="card">
        
                <div class="buttons-toolbar">
                    <PButton :class="{filtered: filterIsApplied}" v-tooltip.bottom="$t('COMMON_BUTTONS.filter')" @click="showFilter = true"  icon="pi pi-search" severity="success" aria-label="Search" />
                    <PButton v-tooltip.bottom="$t('COMMON_BUTTONS.new')" @click="openFormDialog(null)" icon="pi pi-plus" severity="info" aria-label="User" />
                </div>

                <DataTable :value="users" 
                    size="small"
                    scrollable
                    scrollHeight="calc(100vh - 300px)"
                    showGridlines tableStyle="min-width: 50rem" 
                >
                    <template #header>
                        <div class="flex corn">
                            <h1>{{ $t('SECURITY.users')}}</h1>
    
                            <div style="min-width: 40%;">
                                <InputSearch v-model="filter.filters.search" @onChange="getAllUsers" />
                            </div>
                        </div>
                    </template>
        
                    <Column field="createdAt" :header="$t('SECURITY.creation_date')" style="width: 10%"></Column>
                    <Column field="name" :header="$t('SECURITY.name')" style="width: 30%"></Column>
                    <Column field="userName" :header="$t('SECURITY.user_name')" style="width: 20%"></Column>
                    <Column field="isActive" :header="$t('SECURITY.status')" style="width: 8%; padding-left: 25px;">
                        <template #body="{ data }">
                            <StatusIndicator :style="{ '--bg-color': (data.isActive ? '#21b421': '#f13c3c') }" :label="$t('SHARED.' + (data.isActive ? 'active': 'inactive'))"></StatusIndicator>
                        </template>
                    </Column>
                    <Column  :exportable="false" style="width: 20%">
                        <template #body="{ data }">
                            <div class="grid-actions-container">
                                <PButton @click="confirmStatusChange($event, data)" class="grid-button-text" icon="pi pi-lock"  v-tooltip.top="'Eliminar'"  text rounded />
                                <PButton @click="openFormDialog(data)" class="grid-button-text" icon="pi pi-pencil" v-tooltip.top="'Editar'" text rounded  />
                                <PButton @click="openFormDialog(data, true)" class="grid-button-text" icon="pi pi-copy" v-tooltip.top="'Clonar'" text rounded  />
                            </div>
                        </template>
                    </Column>
                    <template #footer>
                        <PaginationInfo :shown="users.length" :total="totalRecords"></PaginationInfo>                        
                    </template>                                   
                </DataTable>     
                <ProgressBar v-if="loadingRecords" mode="indeterminate" style="height: 1px"></ProgressBar>
                <ConfirmPopup></ConfirmPopup>           
            </div>
        </template>

        <template #mobile>
            <div>
                <section class="mobile-input-search">
                    <h2>
                        <i class="pi pi-user" style="font-size: 1.3rem; margin-right: 10px;"></i>
                        {{ $t('SECURITY.user_list') }}
                    </h2>
                    
                    <div>
                        <InputSearch v-model="filter.filters.search"   @onChange="getAllUsers" />
                    </div>
                </section>

                <section class="mobile-card-wrapper">
                    <MobileGridCard v-for="user in users" v-bind:key="user.id">
                        
                        <template #left>
                            <h3>{{ user.name }}</h3>
                            <span>{{ $t('SECURITY.user_name')}}: {{ user.userName }}</span>
                            <span>{{ $t('SECURITY.email') }}: {{ user.email }}</span>                        
                        </template>
    
                        <template #right>
                            <StatusIndicator :style="{ '--bg-color': (user.isActive ? '#21b421': '#f13c3c') }" :label="$t('SHARED.' + (user.isActive ? 'active': 'inactive'))"></StatusIndicator>
                        </template>
    
                        <template #actions>
                            <PButton @click="openFormDialog(user, true)" icon="pi pi-copy" text />
                            <PButton @click="openFormDialog(user)" icon="pi pi-pencil" text />
                            <PButton @click="confirmStatusChange($event, user)" icon="pi pi-lock" text />
                        </template>
    
                    </MobileGridCard>
                </section>
                <ProgressSpinner v-if="loadingRecords" style="width: 25px; height: 25px; margin: auto" strokeWidth="4" animationDuration="1.5s" />
                <ConfirmPopup></ConfirmPopup>
            </div>
        </template>

        <template #mobile-buttons>            
            <PButton :label="$t('COMMON_BUTTONS.filter')" @click="showFilter = true" text aria-label="Filter">
                <i class="pi pi-filter"></i>
                <span>{{ $t('COMMON_BUTTONS.filter') }}</span>
            </PButton>                               
            <PButton :label="$t('COMMON_BUTTONS.new')" @click="openFormDialog(null)" text aria-label="Nuevo">
                <i class="pi pi-plus"></i>
                <span>{{ $t('COMMON_BUTTONS.new') }}</span>
            </PButton>   
        </template>

        <template #mobile-extra-buttons></template>
    </ContentWrapper>
    
    <PDialog  @hide="onDialogClose" v-model:visible="showDialog" :closeOnEscape="false" modal :header="$t('' + (userId === '' ? 'Registro de usuario': 'user_update') )" :style="{ width: '88vw' }">
        <div>
            <UserForm :userRepositoy="UserRepository" :cloning="isCloningRecord" :userId="userRecordId" :roleRepository="RoleRepository" ref="formRef" />
        </div>
        <template #footer>            
            <PButton @click="showDialog = false" severity="warning" :label="$t('COMMON_BUTTONS.cancel')" icon="pi pi-times" />
            <PButton @click="save" :label="$t('COMMON_BUTTONS.save')" icon="pi pi-check" autofocus />
        </template>
    </PDialog>

    <Sidebar v-model:visible="showFilter"  position="right">
        <Filter v-model="filter.filters" @onChage="getAllUsers"></Filter>
    </Sidebar>
</template>

<style lang="scss"></style>