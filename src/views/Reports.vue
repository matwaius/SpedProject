<template>
    <sidebar-layout-vue>
    <v-container class="fill-height flex-column">
        <v-card :width="800"
          height="100%"
          max-height="390"
          class="mx-auto"
          outilined
          elevation="3"
          max-width="350"
    >
    <!--CABEÇALHO-->
    <v-row dense>
        <v-col cols="12">
        <v-toolbar flat
                rounded
                dense
                class="blue-grey lighten-4">
          <v-toolbar-title class="font-weight-medium">Relatórios</v-toolbar-title>
        </v-toolbar>
      </v-col>
    </v-row>
    <v-list dense>
      <v-list-item-group
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
</template>

<script>
import SidebarLayoutVue from '@/layouts/SidebarLayout.vue'

export default {
  name: 'ReportMain',
  data: vm => ({
    items: [
      { text: 'Relatório de Nota Fiscal', icon: 'mdi-hexagon', to: '/ReportNFDay' },
      { text: 'Relatório de Nota Fiscal por ICMS', icon: 'mdi-hexagon', to: '/ReportNFDayByICMS' },
      { text: 'Relatório de Nota Fiscal por ICMS ST', icon: 'mdi-hexagon', to: '/ReportNFDayByICMSST' },
      { text: 'Relatório de Nota Fiscal por UF', icon: 'mdi-hexagon', to: '/ReportNFDayByUF' },
      { text: 'Relatório Nota Fiscal por Quantidade', icon: 'mdi-hexagon', to: '/ReportNFQtdDay' },
      { text: 'Relatório Nota Fiscal por Quantidade por UF', icon: 'mdi-hexagon', to: '/ReportNFQtdDayByUF' },
      { text: 'Relatório de Cupom Fiscal', icon: 'mdi-hexagon', to: '/ReportCFDay' },
      { text: 'Relatório de Totalizadores por Dia', icon: 'mdi-hexagon', to: '/ReportTotalizersDay' },
      //{ text: 'Relatório por Redução Z', icon: 'mdi-hexagon', to: '/ReportNFDepartureDayByRedZ' },
    ]
  }),
  components: {
    SidebarLayoutVue
  },
  methods: {
    retornaRota () {
      this.$router.go(-1)
    }
  }
}

</script>
