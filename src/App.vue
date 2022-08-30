<template>
  <v-app id="inspire">
    <router-view/>
  </v-app>
</template>

<script>
export default {
  mounted () {
    this.$router.push('/login')
  }
}
</script>

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
