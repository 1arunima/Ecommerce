<script setup>
import { ref } from 'vue';
import { RouterLink } from 'vue-router';
import {  IconMenu2, IconShoppingCart,IconUser,IconLetterSSmall, IconHome } from '@tabler/icons-vue'; 

const drawer = ref(true); // State for the drawer

// Navigation Links
// const links = [
//   { title: 'Sales', to: '/sales',  },
//   { title: 'Customer', to: '/customer', },
//   { title: 'Product', to: '/product',  },
// ];

// Toggle function to open/close drawer
const toggleDrawer = () => {
  drawer.value = !drawer.value;
}
</script>

<template>
  <v-app>
    <!-- Menu Button to toggle drawer -->
    <!-- <v-btn @click="toggleDrawer" icon>
      <v-icon>{{ drawer ? '<IconX/>' : 'IconMenu' }}</v-icon> <!-- Toggle icon -->
    <!-- </v-btn> --> 

    <v-container>
      <!-- Navigation Drawer -->
      <v-navigation-drawer
        v-model="drawer"
        app
        expand-on-hover
        rail
        style="position: fixed;"
      >
        <!-- Title -->
        <v-list>
          <v-list-item ><IconMenu2/></v-list-item>
        </v-list>

        <v-divider></v-divider>
          <v-list density="compact" nav>
          <v-list-item :prepend-icon="IconShoppingCart"><RouterLink to="/product" class="nav-link ">Products</RouterLink></v-list-item>
          <v-list-item :prepend-icon="IconUser "><RouterLink to="/customer" class="nav-link">Customer</RouterLink></v-list-item>
          <v-list-item :prepend-icon="IconLetterSSmall"><RouterLink to="/sales" class="nav-link">Sales</RouterLink></v-list-item>
          <v-list-item :prepend-icon="IconHome"><RouterLink to="/home" class="nav-link">Home</RouterLink></v-list-item>
            
        </v-list>
      </v-navigation-drawer>

      <!-- Main Content -->
      <v-main>
        <v-container fluid>
          <router-view />
        </v-container>
      </v-main>
    </v-container>
  </v-app>
</template>
