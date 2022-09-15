<template>
  <div>
    <h1>Relatório - Quantidade de Notas Por Estado</h1>
    <v-btn color="warning" dark @click="Filter">Filtrar</v-btn>
    <table border = '1px'>
      <tr>
        <td>Data</td>
        <td>Valor</td>
      </tr>
      <tr v-for="item in this.list" v-bind:key="item.UF">
        <td>{{item.UF}}</td>
        <td>{{item.QTD}}</td>
      </tr>
    </table>
  </div>
</template>

<script>

import reports from '@/services/reports.ts'
import axios from 'axios'

export default {
  name: 'ReportCFDayList',
  data () {
    return {
      list: undefined,
      dateStart: '2021-06-01',
      dateEnd: '2021-06-30'
    }
  },
  methods: {
    Filter () {
      /*reports.ReportCFDay('2021-06-01', '2021-06-30').then(response => {
        console.log(response.data)
        this.list = response.data
          .catch(error => console.log(error))
      })
        .catch(error => console.log(error))*/
      axios
        .post('https://localhost:7258/api/ReportNFQtdDayByUF?dateStart=' + this.dateStart + '&dateEnd=' + this.dateEnd).then(response => {
          this.list = response.data
          console.log(response.data) })
    }
  },
  mounted () {
  }
}
</script>
