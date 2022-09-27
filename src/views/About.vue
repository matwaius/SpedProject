<template>
  <sidebar-layout-vue>
    <v-container class="fill-height flex-column">
      <v-row>
        <v-col>
        <h1>Sobre nós</h1>
        <br/>
        <br/>
        <h3>Teste teste teste testes</h3>
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
