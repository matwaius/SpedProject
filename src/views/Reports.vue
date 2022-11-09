<template>
    <sidebar-layout-vue>
    <v-container class="fill-height flex-column">
        <v-card :width="800"
          height="100%"
          :max-height="maxHeight"
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
    items: [],
    maxHeight: 390
  }),
  components: {
    SidebarLayoutVue
  },
  methods: {
    retornaRota () {
      this.$router.go(-1)
    }
  },
  mounted(){
    const data = JSON.parse(localStorage.getItem('user'));
    let cont = 0;
    if(data.Nivel == 1 || data.RelNF == 1 )
    {
        this.items.push({ text: 'Relatório de Nota Fiscal', icon: 'mdi-hexagon', to: '/ReportNFDay' })
        cont+=1
    }
    if(data.Nivel == 1 || data.RelNFICMS == 1 )
    {
        this.items.push({ text: 'Relatório de Nota Fiscal por ICMS', icon: 'mdi-hexagon', to: '/ReportNFDayByICMS' })
        cont+=1
    }
    if(data.Nivel == 1 || data.RelNFICMSST == 1 )
    {
        this.items.push({ text: 'Relatório de Nota Fiscal por ICMS ST', icon: 'mdi-hexagon', to: '/ReportNFDayByICMSST' })
        cont+=1
    }
    if(data.Nivel == 1 || data.RelNFUF == 1 )
    {
        this.items.push({ text: 'Relatório de Nota Fiscal por UF', icon: 'mdi-hexagon', to: '/ReportNFDayByUF' })
        cont+=1
    }
    if(data.Nivel == 1 || data.RelNFQTD == 1 )
    {
        this.items.push({ text: 'Relatório Nota Fiscal por Quantidade', icon: 'mdi-hexagon', to: '/ReportNFQtdDay' })
        cont+=1
    }
    if(data.Nivel == 1 || data.RelNFQTDUF == 1 )
    {
        this.items.push({ text: 'Relatório Nota Fiscal por Quantidade por UF', icon: 'mdi-hexagon', to: '/ReportNFQtdDayByUF' })
        cont+=1
    }
    if(data.Nivel == 1 || data.RelCF == 1 )
    {
        this.items.push({ text: 'Relatório de Cupom Fiscal', icon: 'mdi-hexagon', to: '/ReportCFDay' })
        cont+=1
    }
    if(data.Nivel == 1 || data.RelCFTOT == 1 )
    {
        this.items.push({ text: 'Relatório de Totalizadores por Dia', icon: 'mdi-hexagon', to: '/ReportTotalizersDay' })
        cont+=1
    }
    this.maxHeight = 48*cont;
  }
}

</script>
