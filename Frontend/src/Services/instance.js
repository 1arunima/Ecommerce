import axios from 'axios'

const Instance =axios.create({
    baseURL: 'https://192.168.1.127:5000', 
    // headers: {
    //     "Content-Type": "application/json",
    //   },
    });
    
    export default Instance;
   