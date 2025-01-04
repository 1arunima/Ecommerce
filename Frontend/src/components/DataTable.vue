<script setup>
import { ref } from 'vue';
import { IconPencil, IconTrash } from "@tabler/icons-vue";

const props = defineProps({
  headers: Array,
  items: Array,
  formTitle: String,
  dialogContent: Object,
});

const emits = defineEmits(['save', 'edit', 'delete', 'cancel']);

const itemToDelete = ref(null);
const dialog = ref(false);
const dialogDelete = ref(false);
const editedIndex = ref(-1);

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
  emits('save');
  closeDialog();
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
  }
};
</script>

<template>
    <v-container>
      <v-data-table :headers="headers" :items="items" class="elevation-1">
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>Table</v-toolbar-title>
            <v-btn class="mb-2" color="primary" dark @click="dialog = true">New</v-btn>
          </v-toolbar>
  
          <v-dialog v-model="dialog" max-width="500px">
            <v-card>
              <v-card-title>
                <span class="text-h5">{{ formTitle }}</span>
              </v-card-title>
  
              <v-card-text>
                <slot name="dialog-content"></slot>
              </v-card-text>
  
              <v-card-actions>
                <v-spacer />
                <v-btn color="blue-darken-1" variant="text" @click="closeDialog">Cancel</v-btn>
                <v-btn color="blue-darken-1" variant="text" @click="handleSave">Save</v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
  
          <!-- Delete Confirmation Dialog -->
          <v-dialog v-model="dialogDelete" max-width="500px">
            <v-card>
              <v-card-title class="text-h5">Are you sure you want to delete?</v-card-title>
              <v-card-actions>
                <v-spacer />
                <v-btn color="blue-darken-1" variant="text" @click="closeDeleteDialog">Cancel</v-btn>
                <v-btn color="blue-darken-1" variant="text" @click="confirmDelete">OK</v-btn>
                <v-spacer />
              </v-card-actions>
            </v-card>
          </v-dialog>
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
    </v-container>
  </template>