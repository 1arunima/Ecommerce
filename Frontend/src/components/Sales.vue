<script setup>

import { ref } from 'vue';
import axios from '../Services/instance'  

const sales = ref({
    id: 0,
    customer_id: "",
    product_id: "",
    quantity: "",
    total_price:"",
    sales_date:"",
    payment_method:"",
  });
const headers =[
    {text:'Id' ,value:'id'},
    {text:'CustomerId' ,value:'customer_id'},
    {text:'ProductId' ,value:'product_id'},
    {text:'TotalAmount' ,value:'totalamount'},
    {text:'Quantity' ,value:'quantity'},
    {text:'SaleDate' ,value:'saledate'},
    {text:'paymentmethod' ,value:'paymentmethod'},
]



const itemsArray = ref([])

const fetchSalesData= async ()=>{
    try {
        
    } catch (error) {
        
    }
}

</script>

<template>
    <v-container>

    <v-data-table
    :headers:"headers"
    class="elavation-1"
    items-per-page="5"
    >
    </v-data-table>
    </v-container>
</template>