<script setup>
import { ref, onMounted } from 'vue';
import axios from '../Services/instance';
import DataTable from './DataTable.vue';

const itemsArray = ref([]);

const product = ref({
  name: '',
  category: '',
  description: '',
  price: 0,
  quantity: 0,
});


const rules ={
  required:(value)=>!!value || "This field is required",
  positiveNumber:(value)=>(value>0 && !isNaN(value))|| "Must be a Positive number",
  maxlength:(length)=> (value)=>(value.length<=length)||  `Must be ${length} characters or less`,
}
const headers = [
  { title: 'Name', key: 'name', },
  { title: 'Category', key: 'category', },
  { title: 'Description', key: 'description',  },
  { title: 'Price', key: 'price' ,},
  {  title: ' Stock Quantity', key: 'stock_quantity' ,},
  {  title: 'Actions', key: 'actions' , },

];

console.log('Product.vue - Headers:', headers);
const editedIndex = ref(-1);


const fetchProducts = async () => {
  try {
    const response = await axios.get('/api/Product/list');
    itemsArray.value = response.data;
  } catch (error) {
    console.error('Error fetching products:', error);
  }
};

const handleEdit = async (item, index) => {
  try {
    await axios.put(`/api/Product/edit/${item.id}`, item);

    await fetchProducts();
  } catch (error) {
    console.error('Error updating product:', error);
  }

  product.value = { ...item };
  editedIndex.value = index;
};

const handleDelete = async (item, index) => {
  try {
    await axios.delete(`/api/Product/delete/${item.id}`);
    await fetchProducts();
  } catch (error) {
    console.error('Error deleting product:', error);
  }
};
const handleSave = async () => {

  try {
    if (editedIndex.value === -1) {
      await axios.post('/api/Product/add', product.value);
    } else {
      const productId = itemsArray.value[editedIndex.value].id;
      await axios.put(`/api/Product/edit/${productId}`, product.value);
    }
    await fetchProducts();
    resetForm();
  } catch (error) {
    console.error('Error saving product:', error);
  }
}
const handleCancel = () => {
  resetForm();
};

const resetForm = () => {
  product.value = {
    name: '',
    category: '',
    description: '',
    price: 0,
    quantity: 0,
  };
  editedIndex.value = -1;
};

onMounted(() => {
  fetchProducts();
});
</script>


<template>
  <DataTable
    :headers="headers"
    :items="itemsArray"
    formTitle="Product Form"
    @edit="handleEdit"
    @delete="handleDelete"
    @save="handleSave"
    @cancel="handleCancel"
    

  >
    <template #dialog-content>
      <v-container>
        <v-row>
          <v-col cols="12" md="6">
            <v-text-field v-model="product.name" label="Product Name" 
            :rules="[rules.required, rules.maxlength(50)]"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field v-model="product.category" label="Category"
            :rules="[rules.required, rules.maxlength(30)]"
             />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field v-model="product.description" label="Description" 
            :rules="[rules.required, rules.maxlength(100)]"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field v-model="product.price" label="Price" type="number" 
             :rules="[rules.required, rules.positiveNumber]"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field v-model="product.stock_quantity" label=" Stock Quantity" type="number" 
            :rules="[rules.required, rules.positiveNumber]"

            
            />
          </v-col>
        </v-row>
      </v-container>
    </template>
  </DataTable>
</template>
