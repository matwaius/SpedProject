<template>
  <v-app id="inspire">
    <router-view/>
  </v-app>
</template>

<style>
  .main{
    height: 100vh;
    width: 100%;
    max-height: calc(100%-64px);
    overflow: hidden;
    overflow-y: auto;
    top: 64px;
    padding-top: 0px !important;
  }
</style>
