<template>
  <div>
    <Dashboard
        title="Relatório de Totalizadores por Dia"
        title_grafico = "Gráfico"
        title_notas = "Totalizadores"
        :total_grafico_bar="this.total_grafico_bar"
        :total_notas="this.total_notas"
        :show_ind=false
        :show_chart_bar="true"
        :show_chart_pie="false"
        :show_table_notas="true"
        :show_table="false"
        :tableHeaderNotas="tableheaderNotas"
        :tableItemsNotas="tableitemsNotas"
        :dadosGraficoBar="chartData"
  
        @botaoFiltrar ="setaFiltros"
        
        @colunaGraficoBar="getNotas" 

        >
    </Dashboard>
  </div>
  </template>
  
  <script>
  import Dashboard from '@/components/Dashboard.vue'
  import api from '@/services/api.ts';
  import validation from '../services/validation.ts';
  
  export default {
    name: 'ReportNFDayByUF',
    data: vm => ({
        dateInicial: "",
        dateFinal: "",
        loading: false,
        indOperacao: "",
        total_grafico_bar: 0,
        total_notas:0,
        total_itens:0,
        tableheaderNotas: [],
        tableitemsNotas:[],
        tableheader: [],
        tableitems:[],
        chartData: {
            labels: [
               //teste
              '01/06/2021','02/06/2021','03/06/2021','04/06/2021','05/06/2021','06/06/2021','07/06/2021','08/06/2021','09/06/2021','10/06/2021'
            ],
            datasets: [
              {
                label: 'UF',
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
        if(this.loading == false){
            this.loading = true;
            this.limpaDados();
              try{
                await api.post('/ReportTotalizersDay?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal)
                  .then(response => {
                      this.chartData.datasets[0].label="Valor Tot.";
                      for (let i = 0; i < response.data.length; i++) {
                        this.chartData.labels.push(response.data[i].DT_DOC);
                        this.chartData.datasets[0].data.push(response.data[i].VL_BRT );
                        this.total_grafico_bar = Math.round(this.total_grafico_bar* 100) / 100  + Math.round((response.data[i].VL_BRT)* 100) / 100 ; 
                      }
                      this.total_grafico_bar = this.total_grafico_bar.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
                      this.loading = false;
                  })
                  .catch(error => console.log(error));
              } catch (error) {
                this.loading = false;
                error => console.log(error)
              }
              finally{
                this.loading = false;
              }
          }
      },
      setaFiltros(e){
        this.filtros = e;
        this.getRel();
      },
      async getNotas(e){
        if(this.loading == false){
            this.loading = true;
            this.tableheaderNotas=[
                  { text: "Totalizador", value: "COD_TOT_PAR", align:"start", width: 200, divider:false, sortable: true },
                  { text: "Valor", value: "VLR_ACUM_TOT", align:"end", width: 100, divider:false, sortable: true },
              ];
            this.tableitemsNotas=[];
            this.total_notas=0;
              try{
                await api.post('/ReportTotalizersItemsDay?date=' + validation.parseDate(e))
                    .then(response => {
                      for (let i = 0; i < response.data.length; i++) {
                            
                            for (let c = 0; c < response.data[i].Itens.length; c++) {
                                this.tableitemsNotas.push(response.data[i].Itens[c]);
                                this.total_notas = Math.round(this.total_notas* 100) / 100  + Math.round((response.data[i].Itens[c].VLR_ACUM_TOT)* 100) / 100 ; 
                            }
                        }
                        this.total_notas = this.total_notas.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
                        this.loading = false;
                    })
                    .catch(error => console.log(error));
              } catch (error) {
                this.loading = false;
                error => console.log(error)
              }
              finally{
                this.loading = false;
              }
          }
      },
      limpaDados(){
          this.chartData.labels=[];
          this.chartData.datasets[0].data=[];
          this.tableitemsNotas=[];
          this.total_grafico_bar =0;
          this.total_notas=0;
      }
    },
    created(){
    }
  }
  
  </script>
  