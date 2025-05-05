
<script setup lang="ts">
    import type { AuthRepository } from "../../domain/repository/authRepository"
    import { useAuth } from '../../composable/useAuth'
    import { useTheme } from '@/modules/system/composable/useTheme'

    const props = defineProps<{
        authRepository: AuthRepository
    }>()

    const { currentTheme, loadTheme } = useTheme();

    loadTheme(currentTheme.value);

    const { 
        login, 
        loginError, 
        user, 
        password  
    } = useAuth(props.authRepository);

</script>

<template>
    <div class="flexbox-grid container">
        <form @submit.prevent="login">
            <div class="card" style="--min: 35ch">
                <h2>Login</h2>
                <div class="grid formgrid">
                    <div>
                        <div class="p-inputgroup flex input-container">
                            <span class="p-inputgroup-addon">
                                <i class="pi pi-user"></i>
                            </span>
                            <InputText v-model="user" placeholder="Username" />
                        </div>
                    </div>
                    <div>
                        <div class="p-inputgroup flex input-container">
                            <span class="p-inputgroup-addon">
                                <i class="pi pi-lock"></i>
                            </span>
                            <InputText type="password" v-model="password" placeholder="Password" />
                        </div>
                    </div>

                    <div class="error">
                        <label v-if="loginError" class="p-error" id="text-error">Login error</label>
                    </div>

                    <div>
                        <PButton type="submit" class="right">
                            Login
                        </PButton>
                    </div>
                </div>
            </div>
        </form>        
    </div>

</template>


<style lang="scss" scoped>
    .container {
        display: grid;
        place-items: center;
        min-height: 100vh;
    }
    .card {
        max-width: 400px;
        min-width: 300px;
        display: flex;
        flex-direction: column;
        gap: 20px;        
    }
    .right {
        float: right;
        margin-top: 10px;
    }
    .error {
        position: relative;

        label {
            position: absolute;
            left: 0;
            top: 0;
        }
    }

    .input-container {
        display: flex;
    }

    .p-inputgroup-addon,
    input {
        border: 1px solid var(--login-input-color);
    }

</style>@/modules/system/composable/useTheme