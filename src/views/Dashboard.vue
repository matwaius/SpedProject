<template>
  <app name="dashboard">
    <sidebar-layout-vue>
      <div class="d-flex">
        <FaturamentoPorDia/>
        <VendasPorDiatesteVue/>
        <VendasPorDiatesteVue/>
        <VendasPorDiatesteVue/>
      </div>

      <div class="d-flex">
        <FaturamentoTeste/>
      </div>

    </sidebar-layout-vue>
    <footer-layout-vue></footer-layout-vue>
  </app>
</template>

<script>
import SidebarLayoutVue from '@/layouts/SidebarLayout.vue'
import FooterLayoutVue from '@/layouts/FooterLayout.vue'
import FaturamentoPorDia from '../reports/FaturamentoPorDia.vue'
import VendasPorDiatesteVue from '@/reports/VendasPorDiateste.vue'
import FaturamentoTeste from '@/reports/Teste.vue'

export default {
  name: 'Dashboard',
  components: {
    SidebarLayoutVue,
    FooterLayoutVue,
    FaturamentoPorDia,
    VendasPorDiatesteVue,
    FaturamentoTeste
  }
}
</script>
