<template>
  <div>
    <input 
      type="file" 
      @change="handleFileUpload" 
      :accept="acceptedFileTypes"
      multiple
    />
    <div v-if="files.length">
      <h3>Archivos seleccionados:</h3>
      <ul>
        <li v-for="(file, index) in files" :key="index">
          {{ file.name }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue';

export default {
  setup() {
    // Estado reactivo para los archivos seleccionados
    const files = ref([]);
    const acceptedFileTypes = 'image/*, .pdf'; // Puedes especificar tipos de archivos permitidos
    
    // Función para manejar la carga de archivos
    const handleFileUpload = (event) => {
      const selectedFiles = event.target.files;
      if (selectedFiles.length > 0) {
        files.value = Array.from(selectedFiles); // Convertimos FileList a Array
      }
    };

    // Retornamos las variables y funciones que usaremos en la plantilla
    return {
      files,
      acceptedFileTypes,
      handleFileUpload,
    };
  },
};
</script>

<style scoped>
/* Puedes agregar estilos aquí */
</style>