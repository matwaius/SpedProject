<template>
  <div>
    <Dashboard
        title="Relatório de Redução Z por Dia"
        title_grafico = "Gráfico"
        :total_grafico_bar="this.total_grafico_bar"
        :show_ind=false
        :show_chart_bar="true"
        :show_chart_pie="false"
        :show_table="false"
        :tableHeader="tableheader"
        :dadosGraficoBar="chartData"
  
        @botaoFiltrar ="setaFiltros"
        >
    </Dashboard>
  </div>
  </template>
  
  <script>
  import Dashboard from '@/components/Dashboard.vue'
  import api from '@/services/api.ts';
  import validation from '../services/validation.ts';
  
  export default {
    name: 'ReportRZDay',
    data: vm => ({
        dateInicial: "",
        dateFinal: "",
        total_grafico_bar: 0,
        tableheader: [],
        chartData: {
            labels: [
               //teste
              '01/06/2021','02/06/2021','03/06/2021','04/06/2021','05/06/2021','06/06/2021','07/06/2021','08/06/2021','09/06/2021','10/06/2021'
            ],
            datasets: [
              {
                label: 'RZ Dia',
                backgroundColor: '#EF5350',
                data: [
                  //teste
                  100.22,500.08,650.29,590.00,998.00,1500.65,1900.89,2150.98,3500.99,4888.88
                ]
              }
            ]
          },
    }),
    components: {
        Dashboard
    },
    methods: { 
      async getRel () {
          this.limpaDados();
          await api.post('/ReportNFDepartureDayByRedZ?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal)
            .then(response => {
                this.chartData.datasets[0].label="Valor";
                for (let i = 0; i < response.data.length; i++) {
                  this.chartData.labels.push(response.data[i].DT_DOC);
                  this.chartData.datasets[0].data.push(response.data[i].VL_BRT);
                  this.total_grafico_bar = Math.round(this.total_grafico_bar* 100) / 100  + Math.round(response.data[i].VL_BRT* 100) / 100 ; 
                }
                this.total_grafico_bar = this.total_grafico_bar.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
            })
            .catch(error => console.log(error));
      },
      setaFiltros(e){
        this.filtros = e;
        this.chartABC= {
          labels: ['A', 'B', 'C'],
          datasets: [
            {
              backgroundColor: ['#41B883', '#00D8FF', '#FFD54F'],
              data: [this.filtros[0].curvaA, this.filtros[0].curvaB, this.filtros[0].curvaC]
            }
          ]};
          this.getRel();
      },
      limpaDados(){
          this.chartData.labels=[];
          this.chartData.datasets[0].data=[];
          this.total_grafico_bar =0;
      }
    },
    created(){
    }
  }
  
  </script>
  