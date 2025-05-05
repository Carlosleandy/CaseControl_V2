<script setup lang="ts">
import type { UserRepository as UserRepo } from "../../domain/repository/userRepository";
// import type { RoleRepository as RoleRepo } from "../../domain/repository/roleRepository";
import MultiSelect from "primevue/multiselect";
import { useUser } from "../../composable/userComposable";
// import { useRole } from "../../composable/roleComposable";
import { computed, ref } from "vue";
const props = defineProps<{
  userRepositoy: UserRepo;
  // roleRepository: RoleRepo;
  userId?: string;
  cloning?: boolean;
}>();
const value = ref("One-Way");
const options = ref(["Activo", "Inactivo"]);

const isBlueBackground = ref(false); // Cambia a true si quieres que el fondo sea azul

// Aquí podrías tener una función que cambie la clase basado en alguna lógica
const toggleBackground = () => {
    isBlueBackground.value = !isBlueBackground.value;
};

const checked = ref(false);
const selectedJobprofile = ref();
const jobprofile = ref([
  { name: "Gerente", code: "NY" },
  { name: "Subgerente", code: "RM" },
  { name: "Junior", code: "LDN" },
]);
const {
  userForm,
  loggedUserPriviles,
  loggedUserRoles,
  filter,
  saveUser,
  getUserById,
  setUserData,
  getAllUsers,
  getUserPrivileges,
} = useUser({
  userRepository: props.userRepositoy,
  // roleRepository: props.roleRepository,
  userId: props?.userId,
});

// const { getUserRoles } = useRole({
//   roleRepository: props.roleRepository,
// });

const loadInitialData = () => {
  getUserPrivileges("").then((response) => {
    loggedUserPriviles.value = response;
  });

  // getUserRoles("").then((response) => {
  //   loggedUserRoles.value = response || [];
  // });

  if (props?.userId) {
    getUserById(props.userId).then((user) => {
      if (user) {
        if (props.cloning) {
          user.id = "";
          user.userName = "";
          user.email = "";
          user.lastName = "";
          user.name = "";
          user.phone = "";
        }

        setUserData(user);
      }
    });
  }
};
getAllUsers();
loadInitialData();
defineExpose({ save: saveUser });
</script>

<template>
    <form ref="formRef">
               <Toolbar style="display: flex; flex-direction: column; align-items: flex-start;">
      <template #start>
        <span class="toolbar-title" style="margin-bottom: 10px;">Códigos de empleados</span>
      </template>

      <template #end>
        <!-- El contenedor para el campo de búsqueda con ícono -->
        <span class="p-input-icon-left search-container">
          <!-- Icono de la lupa -->
          <i class="pi pi-search" />
          <!-- Campo de entrada de texto -->
          <InputText
            v-model="filter.filters.search"
            @onChange="getAllUsers"
            style="width: 100%;"
            placeholder="Buscar códigos de empleados"
          />
        </span>
      </template>
    </Toolbar>




    <div class="flexbox-grid container">
      <div class="card" style="--min: 50ch">
        <div class="grid">
          <h2>{{ $t("SHARED.basic_information") }}</h2>
          <span class="p-float-label">
            <InputText v-model="userForm.name" />
            <label for="name">{{ $t("SECURITY.name") }}</label>
          </span>
          <span class="p-float-label">
            <InputText v-model="userForm.lastName" />
            <label for="last_name">{{ $t("SECURITY.last_name") }}</label>
          </span>
          <span class="p-float-label">
            <InputText v-model="userForm.phone" />
            <label for="phone">{{ $t("SECURITY.phone") }}</label>
          </span>
          <span class="p-float-label">
            <InputText type="email" v-model="userForm.email" />
            <label for="email">{{ $t("SECURITY.email") }}</label>
          </span>
        </div>
      </div>

      <div class="card" style="--min: 45ch">
        <div class="grid">
          <h2>{{ $t("SECURITY.security") }}</h2>
          <div class="flex flex-row">
            <span class="p-float-label w-6">
              <MultiSelect
                :virtualScrollerOptions="{ itemSize: 50 }"
                optionValue="id"
                :maxSelectedLabels="4"
                :multiple="true"
                v-model="userForm.roles"
                :options="loggedUserRoles"
                filter
                optionLabel="name"
              />
              <label for="role">{{ $t("SECURITY.role") }}</label>
            </span>
            <span class="p-float-label w-6 ml-2">
              <Dropdown
                variant="filled"
                v-model="userForm.jobprofile"
                id="Perfil de trabajo"
                :options="jobprofile"
                showClear
                filter
                optionLabel="name"
                class="w-full"
              />
              <label for="name">{{ $t("Perfil de trabajo") }}</label>
            </span>

             <span class="p-float-label w-6 ml-2">
            <Calendar v-model="userForm.expirationDate" sdateFormat="dd/mm/yy" showIcon iconDisplay="input" />
            <label for="title">{{ $t("Fecha expiración") }}</label>
          </span>
           </div>

            <span class="p-float-label ">
              <InputText v-model="userForm.userName" :disabled="userForm.id != '' && userForm.id != undefined" />
              <label for="user_name">{{ $t("SECURITY.user_name") }}</label>
            </span>
         
          <span class="p-float-label">
            <Password
              id="Password"
              weakLabel="Débil"
              mediumLabel="Medio"
              strongLabel="Fuerte"
              promptLabel="Ingresa tu contraseña"
              v-model="userForm.password"
              :disabled="userForm.id != '' && userForm.id != undefined"
            />
            <label for="password">{{ $t("SECURITY.password") }}</label>
          </span>
          <span class="p-float-label">
            <Password
              id="Password"
              weakLabel="Débil"
              mediumLabel="Medio"
              strongLabel="Fuerte"
              promptLabel="Ingresa tu contraseña"
              v-model="userForm.confirmationPassword"
              :disabled="userForm.id != '' && userForm.id != undefined"
            />
            <label for="password_confirmation">{{ $t("SECURITY.password_confirmation") }}</label>
          </span>
        </div>
      </div>
    </div>
    <div class="flexbox-grid container">
      <div class="card">
        <h2>{{ $t("SECURITY.privileges") }}</h2>
        <data class="privilege-container">
          <div v-for="privileModule in loggedUserPriviles" :key="privileModule.id">
            <span>{{ $t("SECURITY." + privileModule.name) }}</span>
            <ul>
              <li v-for="privilege in privileModule.privileges" :key="privilege.id">
                <Checkbox v-model="userForm.privileges" :value="privilege.id" :inputId="`${privileModule.id}-${privilege.id}`" />
                <label :for="`${privileModule.id}-${privilege.id}`">{{ $t("SECURITY." + privilege.name) }}</label>
              </li>
            </ul>
          </div>
        </data>
           <div class="flexbox-grid container">
             <div style="display: flex; justify-content: flex-end; width: 100%;">
        <SelectButton v-model="value" :options="options" aria-labelledby="basic" style="margin-top: -80px;" />
      </div>
    </div>
      </div>  
         </div>
  </form>
</template>


<style scoped>
.privilege-container {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(15rem, 1fr));
  grid-auto-flow: dense;
  gap: 1rem;
  padding: 10px 5px 10px;
  margin-top: 20px;

  span {
    font-weight: bold;
  }

  & > div {
    margin-top: 10px;
  }
}

ul {
  display: flex;
  flex-direction: column;
  gap: 10px;
  margin-top: 10px;
}

li {
  display: flex;
  gap: 5px;
  align-items: center;
}
.flex {
  display: flex;
  flex-wrap: nowrap;
}

.flex-row {
  flex-direction: row;
}

.w-6 {
  flex-basis: 45%;
}

.ml-2 {
  margin-left: 1rem;
}
.flexbox-container {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(15rem, 1fr));
  grid-auto-flow: dense;
  gap: 1rem;
  padding: 10px 5px 10px;
  margin-top: 20px;

  span {
    font-weight: bold;
  }

  & > div {
    margin-top: 10px;
  }
}
.flexbox-grid.container {
  display: flex;
  justify-content: start; /* Alinea los elementos al principio */
}

.custom-checkbox-group {
  display: flex;
  align-items: center;
  gap: 16px; /* Ajusta este valor según necesites */
}

.flex.items-center {
  display: flex;
  align-items: center;
}
.toolbar-title {
  font-size: 1.5rem; 
  font-weight: bold;
}
.flexbox-grid {
  display: flex;
  justify-content: flex-end; /* Alinea los elementos a la derecha */
  align-items: flex-end; /* Alinea los elementos a la parte inferior */
}
.flex.column {
    display: flex;
    flex-direction: column; /* Ordenar elementos en columna */
    align-items: start; /* Alinea los elementos al principio */
}
 
/* Cambia el color de las letras cuando el fondo es azul */
.blue-background .header-title,
.blue-background .search-bar input {
    color: white; /* Letras blancas si el fondo es azul */
}
 
/* Cambia el color de las letras cuando el fondo es blanco */
.white-background .header-title,
.white-background .search-bar input {
    color: black; /* Letras negras si el fondo es blanco */
}
 
.search-container {
  position: relative;
  width: 700px;
}

.p-input-icon-left i.pi-search {
  position: absolute;
  left: 10px; /* Ajusta la posición horizontal del icono */
  top: 50%;
  transform: translateY(-50%);
  color: #666; /* Color del icono */
}

.p-input-icon-left input {
  padding-left: 35px; /* Aumenta el espacio para el icono */
}

</style>