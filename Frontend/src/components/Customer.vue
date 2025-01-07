<script setup>
 
 import { onMounted, ref } from 'vue';
  import axios from '../Services/instance'
  import DataTable from './DataTable.vue';
 
  const itemsArray= ref([])

  const customer  =ref({
    name:'',
    phone:0,
    email :"",
    city :""
  })

const rules = {
   required: [(value)=>!!value || "This field is required"],
  
   
  }
  const headers=[
  { title: 'Name', key: 'name' },
  { title: 'Phone', key: 'phone' },
  { title: 'Email', key: 'email' },
  { title: 'City', key: 'city'},
  { title: 'Actions', key: 'actions' },
  ]


  const editedIndex = ref(-1)

const fetchCustomer = async ()=>{
  try{
    const response = await axios.get("api/Customer/list?history=false");
    itemsArray.value =response.data
  }catch (error){
    console.error("error fetching Customer", error)
  }
}

const handleEdit = async(item, index)=>{
  customer.value ={...item};
  editedIndex.value=index
}

const handleDelete =async (item, index)=>{
  try {
    await axios.delete(`/api/Customer/delete/${item.id}`)
    await fetchCustomer()
  } catch (error) {
    console.error("error deleting a customer", error)
  }
}

const handleSave = async () => {
  try {
    if (editedIndex.value === -1) {
      await axios.post('/api/Customer/add', customer.value);
    } else {
      const customerId = itemsArray.value[editedIndex.value].id;
      await axios.put(`/api/Customer/edit/${customerId}`, customer.value);
    }
    await fetchCustomer();
    resetForm();
  } catch (error) {
    console.error("Error saving customer:", error);
  }
};

const handleCancel =()=>{
  resetForm()
}


const resetForm =()=>{
  customer.value ={
    name:'',
    phone :0,
    Email :'',
    city:''
  }
}

onMounted(()=>{
  fetchCustomer()
})

</script>


<template>
  <!-- <h1> Customer Table</h1> -->
  <DataTable
    :headers="headers"
    :items="itemsArray"
    formTitle="Customer Form"
    @edit="handleEdit"
    @delete="handleDelete"
    @save="handleSave"
    @cancel="handleCancel"
  >
    <template #dialog-content>
      <v-container>
        <v-row>
          <v-col cols="12" md="6">
            <v-text-field v-model="customer.name" label="Name" 
             :rules="rules.required"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field v-model="customer.phone" label="Phone" type="number"
            :rules="rules.required"
             />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field v-model="customer.email" label="Email" 
            :rules="rules.required"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field v-model="customer.city" label="City" 
            :rules="rules.required"
            />
          </v-col>
        </v-row>
      </v-container>
    </template>
  </DataTable>
</template>