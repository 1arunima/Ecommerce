import {  createRouter, createWebHistory } from "vue-router";
import Sales from "../components/Sales.vue";
import Product from "../components/Product.vue";
import Customer from "../components/Customer.vue";
import Home from "../components/Home.vue";

const routes =[
    {
        path:'/home',
        name:'Home',
        component:Home
    },

    {
        path:'/sales',
        name:'Sales',
        component:Sales
    },
    {
        path:'/product',
        name:'Product',
        component:Product
    },
    {
        path:'/Customer',
        name:'customer',
        component:Customer
    }
]

const router = createRouter({history:createWebHistory(),routes})


export default router