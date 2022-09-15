<template>
  <app name="reports">
    <sidebar-layout-vue>
    <v-card
      class="mx-auto"
      max-width="500"
      tile
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
      { text: 'Relatório de ICMS', icon: 'mdi-clock', to: '/ReportCFDay' },
      { text: 'ReportNFDay', icon: 'mdi-account', to: '/ReportNFDay' },
      { text: 'ReportNFDayByICMS', icon: 'mdi-flag', to: '/ReportNFDayByICMS' },
      { text: 'ReportNFDayByICMSST', icon: 'mdi-flag', to: '/ReportNFDayByICMSST' },
      { text: 'ReportNFDepartureDayByRedZ', icon: 'mdi-flag', to: '/ReportNFDepartureDayByRedZ' },
      { text: 'ReportNFDepartureDayByUF', icon: 'mdi-flag', to: '/ReportNFDepartureDayByUF' },
      { text: 'ReportNFQtdDay', icon: 'mdi-flag', to: '/ReportNFQtdDay' },
      { text: 'ReportNFQtdDayByUF', icon: 'mdi-flag', to: '/ReportNFQtdDayByUF' },
      { text: 'ReportTotalizersDay', icon: 'mdi-flag', to: '/ReportTotalizersDay' }
    ]
  }),
  components: {
    SidebarLayoutVue,
    FooterLayoutVue
  }
}

</script>
