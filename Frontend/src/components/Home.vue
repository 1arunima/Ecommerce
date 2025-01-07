<script setup>
import { ref } from 'vue';

const window =ref(0)
const length = ref(3)
const tab =ref(null)
</script>




<template>
    <v-window
      v-model="window"
      show-arrows
      reverse
      direction="vertical"

    >
      <v-window-item
        v-for="n in length"
        :key="n"
      >
        <v-card class="d-flex justify-center align-center" height="200px">
          <span class="text-h2">Card {{ n }}</span>
        </v-card>
      </v-window-item>
    </v-window>

    <v-card>
    <v-tabs
      v-model="tab"
      bg-color="primary"
    >
      <v-tab value="one">Item One</v-tab>
      <v-tab value="two">Item Two</v-tab>
      <v-tab value="three">Item Three</v-tab>
    </v-tabs>

    <v-card-text>
      <v-tabs-window v-model="tab">
        <v-tabs-window-item value="one">
          One
        </v-tabs-window-item>

        <v-tabs-window-item value="two">
          Two
        </v-tabs-window-item>

        <v-tabs-window-item value="three">
          Three
        </v-tabs-window-item>
      </v-tabs-window>
    </v-card-text>
  </v-card>

  </template>


  