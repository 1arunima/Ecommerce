<script setup>
import { ref, onMounted } from 'vue';
import axios from '../Services/instance';
import DataTable from './DataTable.vue';
import dayjs from 'dayjs';

const sales = ref({
  id: 0,
  customer_id: "",
  product_id: "",
  quantity: 0,
  total_amount: 0,
  sale_date: "",
  payment_method: "",
});

const customerOptions = ref([]);
const productOptions = ref([]);
const itemsArray = ref([]);
const dialogVisible = ref(false);

const rules = {
  required: [(v) => !!v || "This field is required"],
  positiveNumber: [(v) => v > 0 || "Must be a positive number"],
  date: [(v) => !!v || "Sale date is required"],
  paymentMethod: [(v) => !!v || "Payment method is required"],
};

const headers = [
  { title: 'Customer Name', key: 'customerName' },
  { title: 'Product Name', key: 'productName' },
  { title: 'Quantity', key: 'quantity' },
  { title: 'Total Amount', key: 'total_amount' },
  { title: 'Sale Date', key: 'sale_date', value: (item) => dayjs(item.sale_date).format("DD-MM-YYYY") },
  { title: 'Payment Method', key: 'payment_method' },
  { title: 'Actions', key: 'actions' },
];

const fetchSalesData = async () => {
  try {
    const salesResponse = await axios.get('/api/Sales/GetSales');
    itemsArray.value = salesResponse.data || [];
  } catch (error) {
    console.error('Error fetching data:', error);
  }
};

const handleSave = async () => {
  try {
    if (sales.value.id === 0) {
      await axios.post('/api/Sales/AddSales', sales.value);
    } else {
      await axios.put('/api/Sales/Edit', sales.value);
    }
    fetchSalesData();
    resetForm();
  } catch (error) {
    console.error('Error saving sales data:', error);
  }
};

const getCustomerList = async () => {
  const customersResponse = await axios.get('/api/Customer/getCustomerList');
  customerOptions.value = customersResponse.data;
};

const getProductList = async () => {
  const productsResponse = await axios.get('/api/Product/getProductList');
  productOptions.value = productsResponse.data;
};

const handleEdit = (item) => {
  sales.value = { ...item };
  sales.value.sale_date = dayjs(sales.value.sale_date).format("YYYY-MM-DD");
};

const handleDelete = async (item) => {
  try {
    await axios.delete(`/api/Sales/Delete/${item.id}`);
    fetchSalesData();
  } catch (error) {
    console.error('Error deleting sales:', error);
  }
};

const resetForm = () => {
  sales.value = {
    id: 0,
    customer_id: "",
    product_id: "",
    quantity: 0,
    total_amount: 0,
    sale_date: "",
    payment_method: "",
  };
  dialogVisible.value = false;
};

onMounted(fetchSalesData);
</script>

<template>

  <DataTable
    :headers="headers"
    :items="itemsArray"
    formTitle="Sales Form"
    @edit="handleEdit"
    @delete="handleDelete"
    @save="handleSave"
    @cancel="resetForm"
  >
    <template #dialog-content>
      <v-container>
        <v-row>
          <v-col cols="12" md="6">
            <v-autocomplete
              v-model="sales.customer_id"
              :items="customerOptions"
              itemTitle="label"
              itemValue="id"
              label="Select Customer"
              :rules="rules.required"
              @focus="getCustomerList"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-autocomplete
              v-model="sales.product_id"
              :items="productOptions"
              label="Select Product"
              item-value="id"
              item-title="label"
              :rules="rules.required"
              @focus="getProductList"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="sales.quantity"
              label="Quantity"
              type="number"
              :rules="rules.required"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="sales.total_amount"
              label="Total Amount"
              type="number"
              :rules="rules.required"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="sales.sale_date"
              label="Sale Date"
              type="date"
              :rules="rules.date"
            />
          </v-col>
          <v-col cols="12" md="6">
            <v-select
              v-model="sales.payment_method"
              :items="['Card', 'Cash']"
              label="Payment Method"
              :rules="rules.paymentMethod"
            />
          </v-col>
        </v-row>
      </v-container>
    </template>
  </DataTable>
</template>


<style>
/* Chrome, Safari, Edge, Opera */
input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

/* Firefox */
input[type=number] {
  -moz-appearance: textfield;
}
</style>
