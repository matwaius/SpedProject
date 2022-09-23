<template>
  <app name="reports">
    <sidebar-layout-vue>
    <v-container class="fill-height flex-column">
        <v-card :width="800"
          height="100%"
          max-height="430"
          class="mx-auto"
          outilined
          elevation="3"
          max-width="350"
    >
    <v-list dense>
      <v-subheader>Relatórios</v-subheader>
      <v-list-item-group
        v-model="selectedItem"
        color="primary"
      >
        <v-list-item
          v-for="(item, i) in items"
          :key="i"
          :to="item.to"
          link
        >
          <v-list-item-icon>
            <v-icon v-text="item.icon"></v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title v-text="item.text"></v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list-item-group>
    </v-list>
    </v-card>
    </v-container>
    </sidebar-layout-vue>
    <footer-layout-vue></footer-layout-vue>
  </app>
</template>

<script>
import SidebarLayoutVue from '@/layouts/SidebarLayout.vue'
import FooterLayoutVue from '@/layouts/FooterLayout.vue'

export default {
  name: 'ReportMain',
  data: vm => ({
    items: [
      { text: 'Relatório de Cupom Fiscal', icon: 'mdi-hexagon', to: '/ReportCFDay' },
      { text: 'Relatório de Nota Fiscal', icon: 'mdi-hexagon', to: '/ReportNFDay' },
      { text: 'Relatório de Nota Fisca por ICMS', icon: 'mdi-hexagon', to: '/ReportNFDayByICMS' },
      { text: 'Relatório de Nota Fisca por ICMS ST', icon: 'mdi-hexagon', to: '/ReportNFDayByICMSST' },
      { text: 'Relatório por Redução Z', icon: 'mdi-hexagon', to: '/ReportNFDepartureDayByRedZ' },
      { text: 'Relatório de Nota Fiscal por UF', icon: 'mdi-hexagon', to: '/ReportNFDepartureDayByUF' },
      { text: 'Relatório Nota Fiscal por Quantidade', icon: 'mdi-hexagon', to: '/ReportNFQtdDay' },
      { text: 'Relatório Nota Fiscal por Quantidade por UF', icon: 'mdi-hexagon', to: '/ReportNFQtdDayByUF' },
      { text: 'Relatório Nota Fiscal por Totalizadores', icon: 'mdi-hexagon', to: '/ReportTotalizersDay' }
    ]
  }),
  components: {
    SidebarLayoutVue,
    FooterLayoutVue
  }
}

</script>
