import { ref } from "vue";
import { defineStore } from "pinia";
import axios from '../Services/instance'

 export const useProductStore =defineStore("product", ()=>{
    const ProductsArray= ref([]);

    const product = ref({
        // id: "",
        name: "",
        category:"",
        description: "",
        price: 0,
        stock_quantity:0
    })

const saveProduct =async()=>{
    console.log("save");
    
       try {
        const res=await axios.post("/api/Product/add",product.value) 
        console.log("successfully inserted",res.data);
        

       } catch (error) {
        console.log("error",error)
       }
}


return {
    saveProduct,
    product,ProductsArray
  
}

 })