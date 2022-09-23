<template>
<div>
  <Dashboard
      title="Relatório de Cupom Fiscal por Dia"
      :show_ind=false
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
      chartData: {
          labels: [
             //teste
            '01/06/2021','02/06/2021','03/06/2021','04/06/2021','05/06/2021','06/06/2021','07/06/2021','08/06/2021','09/06/2021','10/06/2021'
          ],
          datasets: [
            {
              label: 'CF Dia',
              backgroundColor: '#489999',
              data: [
                //teste
                100.22,500.08,650.29,590.00,998.00,1500.65,1900.89,2150.98,3500.99,4888.88
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
        this.chartData.labels=[];
        this.chartData.datasets[0].data=[];
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
