<script setup>
import { ref } from 'vue';
import { IconPencil, IconTrash , IconSearch ,IconPlus  } from "@tabler/icons-vue";

const props = defineProps({
  headers: Array,
  items: Array,
  formTitle: String,
  dialogContent: Object,
});

const formRef = ref(null)
const isFormValid = ref(false)

const emits = defineEmits(['save', 'edit', 'delete', 'cancel']);

const itemToDelete = ref(null);
const dialog = ref(false);
const dialogDelete = ref(false);
const editedIndex = ref(-1);
const search =ref('')


// Snackbar state
const snackbar = ref(false);
const snackbarMessage = ref('');
const snackbarOpts = {
  color: "success"
}

 const validateForm=()=>{
  if(formRef.value){
    formRef.value.validate();
  }
 }
const closeDialog = () => {
  dialog.value = false;
  editedIndex.value = -1;
  emits('cancel');
};

// Log headers and items for debugging
console.log('DataTable.vue - Headers:', props.headers);
console.log('DataTable.vue - Items:', props.items);

const closeDeleteDialog = () => {
  dialogDelete.value = false;
};

const handleSave = () => {
  validateForm();
  if (isFormValid.value) {
    emits('save');
        snackbarOpts.color = 'success'
    snackbarMessage.value = 'Successfully saved!';
    snackbar.value = true;

    closeDialog();
  } else {
    snackbarMessage.value = 'There are some required fields!';
    snackbarOpts.color = 'error'
    snackbar.value = true;
    console.log("Form is invalid");
  }
};

const handleEdit = (item, index) => {
  editedIndex.value = index;
  dialog.value = true;
  emits('edit', item, index);
};

const handleDelete = (item, index) => {
  itemToDelete.value = { item, index };
  dialogDelete.value = true;
  
};
const confirmDelete = () => {
  if (itemToDelete.value && itemToDelete.value.item) {
    emits('delete', itemToDelete.value.item, itemToDelete.value.index);
    closeDeleteDialog(); 
    snackbarMessage.value = 'Successfully deleted!';
    snackbar.value = true;

  }
};
</script>

<template>
  <v-container fluid class="d-flex flex-column overflow-hidden h-100">
   

    <v-data-table 
      :headers="headers" 
      :items="items" 
      class="elevation-1" 
      :search="search"  
      fixed-header
      height="calc(100vh - 208px)" 
    >
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>{{ formTitle }}</v-toolbar-title>
           <v-text-field
            class="me-2"
            v-model="search"
            label="Search"
            style="max-width: 200px; height: 40px; margin-bottom: 10px;"
            density="compact"
            variant="outlined"
            :clearable="true"
    >
      <!-- Add Tabler Icon in the append-inner slot -->
      <template #append-inner>
        <v-icon size="24">
          <IconSearch />
        </v-icon>
      </template>
    </v-text-field> 
          <v-btn class="mb-2" color="primary" dark @click="dialog = true">
      <v-icon left>
    <IconPlus /> 
    </v-icon>
      New Item
</v-btn>
        </v-toolbar>
      </template>

      <template v-slot:item.actions="{ item, index }">
        <!-- Tabler Edit Icon -->
        <v-icon class="ma-2" size="small" @click="handleEdit(item, index)" style="color: blue">
          <IconPencil />
        </v-icon>

        <!-- Tabler Delete Icon -->
        <v-icon class="ma-2" size="small" @click="handleDelete(item, index)" style="color: red">
          <IconTrash />
        </v-icon>
      </template>
    </v-data-table>

    <v-dialog v-model="dialog" max-width="500px">
      <v-form ref="formRef" v-model="isFormValid">
        <v-card>
          <v-card-title>
            <span class="text-h5">{{ formTitle }}</span>
          </v-card-title>

          <v-card-text>
            <slot name="dialog-content"></slot>
          </v-card-text>

          <v-card-actions>
            <v-btn color="blue-darken-1" variant="text" @click="closeDialog">Cancel</v-btn>
            <v-btn color="blue-darken-1" variant="text" @click="handleSave">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-form>
    </v-dialog>

    <!-- Delete Confirmation Dialog -->
    <v-dialog v-model="dialogDelete" max-width="500px">
      <v-card>
        <v-card-title class="text-h5">Are you sure you want to delete?</v-card-title>
        <v-card-actions>
          <v-btn color="blue-darken-1" variant="text" @click="closeDeleteDialog">Cancel</v-btn>
          <v-btn color="blue-darken-1" variant="text" @click="confirmDelete">OK</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>

  <v-snackbar v-model="snackbar" timeout="3000" :color="snackbarOpts.color">
      {{ snackbarMessage }}
      <template #action>
        <v-btn color="white" text @click="snackbar = false">Close</v-btn>
      </template>
    </v-snackbar>
</template>
<style scoped>
  .v-container {
    height: 100%;
    overflow: hidden;
    
  }
</style>