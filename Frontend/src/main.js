import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import Router from '../src/router/index'

import { createPinia } from 'pinia'


// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

const pinia = createPinia()
const vuetify = createVuetify({
  components,
  directives,
})

const app=createApp(App)
app.use(Router)
app.use(vuetify)
app.use(pinia)
app.mount('#app')
 
{/* <template>
    <v-container>
    <v-text-field
      v-model="search"
      label="Search"
      class="mb-4"
      append-inner-icon="tabler-search"
      
    >
      <!-- Add Tabler Icon in the append-inner slot -->
      <template #append-inner>  
        <v-icon  size="24">
          <IconSearch />
        </v-icon>
      </template>
    </v-text-field> 
      <v-data-table :headers="headers" :items="items" class="elevation-1" :search="search"  fixed-header
      style="height: 500px; width:800px; overflow: auto;"
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>{{ formTitle }}</v-toolbar-title>
            <v-btn class="mb-2" color="primary" dark @click="dialog = true">New Item</v-btn>
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
  </template> */}