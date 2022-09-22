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
      @dataInicial ="setaDataInicial($event)"
      >
  </Dashboard>
</div>
</template>

<script>
import Dashboard from '@/components/Dashboard.vue'
import {ref} from 'vue';

export default {
  name: 'ReportCFDay',
  props:{
    dataInicial: "",
    dataFinal: "",
  },
  data() {
    return {
      dateInicial: "",
      dateFinal: "",
      tableheader: [],
      tableitems:[],
      chartData: {
          labels: [
            'January',
            'February',
            'March',
            'April',
            'May',
            'June',
            'July',
            'August',
            'September',
            'October',
            'November',
            'December'
          ],
          datasets: [
            {
              label: 'Data One',
              backgroundColor: '#f87979',
              data: [40, 20, 12, 39, 10, 40, 39, 80, 40, 20, 12, 11]
            }
          ]
        }
    }
  },
  components: {
      Dashboard
  },
  setup() {
    function setaDataInicial(e){
      this.dateInicial= e;
      console.log(e);
    }
    return{
      setaDataInicial
    }
  },
  methods: { 
    GetRel () {
        axios
          .post('https://localhost:7258/api/ReportCFDay?dateStart=' + this.dateInicial + '&dateEnd=' + this.dateFinal).then(response => {
            this.list = response.data
            this.loaded = true
            console.log(response.data)
          }
          ).catch(error => console.log(error))
    }
  },
  onMounted(){

  }
}

</script>
