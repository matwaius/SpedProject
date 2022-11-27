<template>
  <sidebar-layout-vue>
      <v-container class="fill-height flex-column">
          <v-row>
            <v-col>
            <br/>
            <br/>
            <br/>
            <h1 class="sobre">Sobre nós</h1>
            <br/>
            <br/>
            <h3 class="p1">Essa aplicação foi desenvolvida para atender clientes que estão com arquivos de sped armazenados e que tem a </h3>  
            <h3 class="p2">necessidade de abstrair os dados armazenados e emitir relatórios para uma tomada de decisão.</h3>
            </v-col>
          </v-row>
      </v-container>
  </sidebar-layout-vue>
</template>

<script>
import SidebarLayoutVue from '@/layouts/SidebarLayout.vue'

export default {
  name: 'aboutPage',
  components: {
    SidebarLayoutVue
  }
}
</script>
<style scoped>
  .sobre{
    text-align: center;
  }
  .p1{
    text-align: center;
  }
  .p2{
    text-align: center;
  }
</style>
