<template>
  <app name="ReportNFQtdDay">
    <div>
      <h1>Relatório - AJUSTAR</h1>

    <v-container   fluid >
      <v-btn color="error" dark @click="Return">Retornar</v-btn>
      <br/>
      <br/>
      <v-row>
        <v-col
          cols="3"
          lg="3"
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
                label="Data Inicial"
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
        </v-col>
        <v-col
          cols="6"
          lg="3"
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
                label="Data Final"
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
        </v-col>
      </v-row>
    </v-container>
  </div>

  <v-container fluid>
    <v-btn color="warning" dark @click="Filter">Filtrar</v-btn>
  </v-container>

  <v-container fluid>
    <br/>
    <table border = '1px'>
      <tr>
        <td>Data</td>
        <td>Valor</td>
      </tr>
      <tr v-for="item in this.list2" v-bind:key="item.DT_DOC">
        <td>{{item.DT_DOC}}</td>
        <td>{{item.QTD}}</td>
      </tr>
    </table>
  </v-container>

  <v-container>
    <Bar :chart-data="chartData" />
  </v-container>

  <footer-layout-vue></footer-layout-vue>
  </app>
</template>

<script>
import FooterLayoutVue from '@/layouts/FooterLayout.vue'
import axios from 'axios'
import moment from 'moment'

export default {
  name: 'ReportNFQtdDay',
  data: vm => ({
    list: undefined,
    list2: [],
    loaded: false,
    date: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
    date2: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
    dateFormatted: vm.formatDate((new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)),
    dateFormatted2: vm.formatDate((new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)),
    menu1: false,
    menu2: false,
    chartData: {
      labels: ['Janeiro', 'Fevereiro', 'Março'],
      datasets: [
        {
          label: 'Data One',
          backgroundColor: '#f87979',
          data: [40, 62, 12]
        }
      ]
    }
  }),
  components: {
    FooterLayoutVue
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
    FilterBKP () {/*OK - Sem o moment para corrigir a data que está vindo datetime do backend */
      axios
        .post('https://localhost:7258/api/ReportNFQtdDay?dateStart=' + this.date + '&dateEnd=' + this.date2).then(response => {
          this.list = response.data
          this.loaded = true
          console.log(response.data)
        }
        ).catch(error => console.log(error))
    },
    Filter () {/*Com o moment para corrigir a data que está vindo datetime do backend */
      axios
        .post('https://localhost:7258/api/ReportNFQtdDay?dateStart=' + this.date + '&dateEnd=' + this.date2).then(response => {
          this.list = response.data

          this.list.forEach(d => {
            const DT_DOC = moment(d.DT_DOC, 'DD/MM/YYYY 00:00:00').format('DD/MM/YYYY')
            const {
              QTD
            } = d
            this.list2.push({ DT_DOC, QTD })
          })
          this.loaded = true
          console.log(response.data)
          console.log('teste')
          console.log(this.list2)
        }
        )
    },
    Return () {
      this.$router.push('/reports')
    },
    mounted () {
      this.loaded = false
    }
  }
}

</script>
