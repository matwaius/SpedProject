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
          max-width="300"
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
      { text: 'Relatório de ICMS', icon: 'mdi-hexagon', to: '/ReportCFDay' },
      { text: 'ReportNFDay', icon: 'mdi-hexagon', to: '/ReportNFDay' },
      { text: 'ReportNFDayByICMS', icon: 'mdi-hexagon', to: '/ReportNFDayByICMS' },
      { text: 'ReportNFDayByICMSST', icon: 'mdi-hexagon', to: '/ReportNFDayByICMSST' },
      { text: 'ReportNFDepartureDayByRedZ', icon: 'mdi-hexagon', to: '/ReportNFDepartureDayByRedZ' },
      { text: 'ReportNFDepartureDayByUF', icon: 'mdi-hexagon', to: '/ReportNFDepartureDayByUF' },
      { text: 'ReportNFQtdDay', icon: 'mdi-hexagon', to: '/ReportNFQtdDay' },
      { text: 'ReportNFQtdDayByUF', icon: 'mdi-hexagon', to: '/ReportNFQtdDayByUF' },
      { text: 'ReportTotalizersDay', icon: 'mdi-hexagon', to: '/ReportTotalizersDay' }
    ]
  }),
  components: {
    SidebarLayoutVue,
    FooterLayoutVue
  }
}

</script>
