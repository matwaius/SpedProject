<template>
  <app name="dashboard">
    <sidebar-layout-vue>

<template>
  <v-container>
    <v-row>
      <v-col
        cols="12"
        lg="6"
      >
        <v-menu
          ref="menu1"
          v-model="menu1"
          :close-on-content-click="false"
          transition="scale-transition"
          offset-y
          max-width="290px"
          min-width="auto"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              v-model="dateFormatted"
              label="Date"
              hint="YYYY-MM-DD format"
              persistent-hint
              prepend-icon="mdi-calendar"
              v-bind="attrs"
              @blur="date = parseDate(dateFormatted)"
              v-on="on"
            ></v-text-field>
          </template>
          <v-date-picker
            v-model="date"
            no-title
            @input="menu1 = false"
          ></v-date-picker>
        </v-menu>
        <p>Date in ISO format: <strong>{{ date }}</strong></p>
      </v-col>

      <v-col
        cols="12"
        lg="6"
      >
        <v-menu
          ref="menu2"
          v-model="menu2"
          :close-on-content-click="false"
          transition="scale-transition"
          offset-y
          max-width="290px"
          min-width="auto"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              v-model="dateFormatted2"
              label="Date"
              hint="YYYY-MM-DD format"
              persistent-hint
              prepend-icon="mdi-calendar"
              v-bind="attrs"
              @blur="date2 = parseDate(dateFormatted2)"
              v-on="on"
            ></v-text-field>
          </template>
          <v-date-picker
            v-model="date2"
            no-title
            @input="menu2 = false"
          ></v-date-picker>
        </v-menu>
        <p>Date in ISO format: <strong>{{ date2 }}</strong></p>
      </v-col>
    </v-row>
  </v-container>
</template>

  <v-btn color="warning" dark @click="Filter">Filtrar</v-btn>

      <div class="d-flex" align-right>
        <FaturamentoTeste/>
      </div>

<table>
<thead>
  <tr>
    <th>NOME</th>
    <th>VALOR</th>
  </tr>
</thead>
<tbody>
  <tr v-for="uf of UFS" :key="uf.UF">
    <td>{{ uf.UF }}</td>
    <td>{{ uf.VL_DOC }}</td>
  </tr>
</tbody>
</table>

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

import reports from '@/services/reports.ts'

export default {
  name: 'Dashboard',
  data: vm => ({
    uf: {
      UF: '',
      VL_DOC: ''
    },
    UFS: [{ UF: 'SC', VL_DOC: 787973.13 },
      { UF: 'RS', VL_DOC: 3486.05 },
      { UF: 'PR', VL_DOC: 1700 },
      { UF: 'MG', VL_DOC: 54.43 }
    ],
    date: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
    date2: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
    dateFormatted: vm.formatDate((new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)),
    dateFormatted2: vm.formatDate((new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)),
    menu1: false,
    menu2: false
  }),
  components: {
    SidebarLayoutVue,
    FooterLayoutVue,
    FaturamentoPorDia,
    VendasPorDiatesteVue,
    FaturamentoTeste
  },
  computed: {
    computedDateFormatted () {
      return this.formatDate(this.date)
    }
  },
  watch: {
    date (val) {
      this.dateFormatted = this.formatDate(this.date)
    },
    date2 (val) {
      this.dateFormatted = this.formatDate(this.date)
    }
  },
  methods: {
    formatDate (date) {
      if (!date) return null

      const [year, month, day] = date.split('-')
      return `${day}/${month}/${year}`
    },
    parseDate (date) {
      if (!date) return null

      const [day, month, year] = date.split('/')
      return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
    },
    Filter () {
      reports.ReportNFDepartureDayByUF(this.date, this.date2).then(response => {
        console.log(response.data)
        this.Ufs = response.data
          .catch(error => console.log(error))
      })
        .catch(error => console.log(error))
    }
  },
  mounted () {
/*    reports.Files().then(response => {
      console.log(response)
    })
      .catch(error => console.log(error))*/
  }
}

</script>
