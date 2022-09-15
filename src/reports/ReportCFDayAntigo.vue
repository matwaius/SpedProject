<template>
  <div>
    <h1>Relatório - Relatório de ICMS</h1>

    <v-col cols="12" sm="6">
      <v-text-field v-model="dateStart" label="dateStart" outlined clearable></v-text-field>
    </v-col>

    <v-col cols="12" sm="6">
      <v-text-field v-model="dateEnd" label="dateEnd" outlined clearable></v-text-field>
    </v-col>
    <h2>{{dateStart}}</h2>
    <h2>{{dateEnd}}</h2>
    <v-btn color="warning" dark @click="Filter">Filtrar</v-btn>

    <table border = '1px'>
      <tr>
        <td>Data</td>
        <td>Valor</td>
      </tr>
      <tr v-for="item in this.list" v-bind:key="item.DT_DOC">
        <td>{{item.DT_DOC}}</td>
        <td>{{item.VL_DOC}}</td>
      </tr>
    </table>

  </div>
</template>

<script>

import reports from '@/services/reports.ts'
import axios from 'axios'

export default {
  name: 'ReportCFDay',
  data () {
    return {
      list: undefined,
      dateStart: '',
      dateEnd: ''
    }
  },
  components: {

  },
  methods: {
    Filter () {
      axios
        .post('https://localhost:7258/api/ReportCFDay?dateStart=' + this.dateStart + '&dateEnd=' + this.dateEnd).then(response => {
          this.list = response.data
          console.log(response.data) })
    }
  },
  mounted () {
  }
}
</script>
