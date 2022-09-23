<template>
<div>
  <Dashboard
      title="Relatório de Cupom Fiscal por Dia"
      :show_ind=true
      :show_table="false"
      :maxHeight = "570"
      :tableHeader="tableheader"
      :tableItems="tableitems"
      :dadosGrafico="chartData"
      @filtros ="setaFiltros"
      >
  </Dashboard>
</div>
</template>

<script>
import Dashboard from '@/components/Dashboard.vue'
import api from '@/services/api.ts';

export default {
  name: 'ReportCFDay',
  props:{
    filtro:[],
  },
  data() {
    return {
      dateInicial: "",
      dateFinal: "",
      indOperacao: "",
      tableheader: [],
      tableitems:[],
      colunas:[],
      dados:[],
      chartData: {
          labels: [
             //teste
            '01/06/2021','02/06/2021','04/06/2021'
          ],
          datasets: [
            {
              label: '',
              backgroundColor: '#489999',
              data: [
                //teste
                2098.22,30020.08,14279.29
              ]
            }
          ]
        }
    }
  },
  components: {
      Dashboard
  },
  methods: { 
    GetRel () {
      
        api.post('/ReportCFDay?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal)
          .then(response => {
              this.chartData.datasets[0].label="Dias";
              for (let i = 0; i < response.data.length; i++) {
                this.chartData.labels.push(response.data[i].DT_DOC);
                this.chartData.datasets[0].data.push(response.data[i].VL_DOC);
              }
          })
          .catch(error => console.log(error))
    },
    setaFiltros(e){
      this.filtros = e;
      this.GetRel();
    }
  },
  created(){

  }
}

</script>
