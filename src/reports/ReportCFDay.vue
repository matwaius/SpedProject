<template>
<div>
  <Dashboard
      title="Relatório de Cupom Fiscal por Dia"
      title_grafico = "Gráfico"
      title_itens = "Itens"
      title_curva = "Curva ABC"
      :total_grafico_bar="this.total_grafico_bar"
      :total_itens="this.total_itens"
      :show_ind=false
      :show_chart_bar="true"
      :show_chart_pie="true"
      :show_table="true"
      :tableHeader="tableheader"
      :tableItems="tableitems"
      :dadosGraficoBar="chartData"
      :dadosGraficoPie="chartABC"

      @botaoFiltrar ="setaFiltros"
      @botaoFiltrarABC ="setaFiltrosABC"
      
      @colunaGraficoBar="getItems" 
      @colunaGraficoPie="getItemsABC"
      >
  </Dashboard>
</div>
</template>

<script>
import Dashboard from '@/components/Dashboard.vue'
import api from '@/services/api.ts';
import validation from '../services/validation.ts';

export default {
  name: 'ReportCFDay',
  data: vm => ({
      dateInicial: "",
      dateFinal: "",
      indOperacao: "",
      total_grafico_bar: 0,
      total_itens:0,
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
              backgroundColor: '#EF5350',
              data: [
                //teste
                100.22,500.08,650.29,590.00,998.00,1500.65,1900.89,2150.98,3500.99,4888.88
              ]
            }
          ]
        },
        chartABC: {
        labels: ['A', 'B', 'C'],
        datasets: [
          {
            backgroundColor: ['#41B883', '#00D8FF', '#FFD54F'],
            data: [80, 15, 5]
          }
        ]
      }
  }),
  components: {
      Dashboard
  },
  methods: { 
    async getRel () {
        this.limpaDados();
        await api.post('/ReportCFDay?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal)
          .then(response => {
              this.chartData.datasets[0].label="Dias";
              for (let i = 0; i < response.data.length; i++) {
                this.chartData.labels.push(response.data[i].DT_DOC);
                this.chartData.datasets[0].data.push(response.data[i].VL_DOC);
                this.total_grafico_bar = Math.round(this.total_grafico_bar* 100) / 100  + Math.round(response.data[i].VL_DOC* 100) / 100 ; 
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
    setaFiltrosABC(e){
      this.filtros = e;
      this.chartABC= {
        labels: ['A', 'B', 'C'],
        datasets: [
          {
            backgroundColor: ['#41B883', '#00D8FF', '#FFD54F'],
            data: [this.filtros[0].curvaA, this.filtros[0].curvaB, this.filtros[0].curvaC]
          }
        ]};
    },
    async getItems(e){
      this.tableheader=[
            { text: "Cód. Item", value: "COD_ITEM", align:"start", divider:false, width: "14%", sortable: true },
            { text: "Descrição", value: "DESCR_ITEM", align:"start", divider:false, width: "25%", sortable: true },
            { text: "Qtd.", value: "QTD",align:"end", divider:false, width: "11%", sortable: true },
            { text: "Total", value: "VL_ITEM",align:"end", divider:false, width: "14%", sortable: true },
            { text: "UN", value: "UNID",align:"end", divider:false, width: "10%", sortable: true },
            { text: "CFOP", value: "CFOP",align:"end", divider:false, width: "12%", sortable: true },
            { text: "ICMS", value: "ALIQ_ICMS",align:"end", divider:false, width: "14%", sortable: true },
        ];
      this.tableitems=[];
      this.total_itens=0;
      await api.post('/ReportCFDayItems?date=' + validation.parseDate(e))
          .then(response => {
            for (let i = 0; i < response.data.length; i++) {
                  for (let c = 0; c < response.data[i].Itens.length; c++) {
                      this.tableitems.push(response.data[i].Itens[c]);
                      this.total_itens = Math.round(this.total_itens* 100) / 100  + Math.round(response.data[i].Itens[c].VL_ITEM* 100) / 100 ; 
                  }
              }
              this.total_itens = this.total_itens.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
          })
          .catch(error => console.log(error));
    },
    async getItemsABC(e){
      this.tableheader = [ 
        { text: "Cód. Item", value: "COD_ITEM", align:"start", divider:false, width: "14%", sortable: true },
        { text: "Descrição", value: "DESCR_ITEM", align:"start", divider:false, width: "25%", sortable: true },
        { text: "Qtd.", value: "QTD",align:"end", divider:false, width: "11%", sortable: true },
        { text: "Total", value: "VL_ITEM",align:"end", divider:false, width: "14%", sortable: true },
        { text: "Perc. %", value: "PERC",align:"end", divider:false, width: "24%", sortable: true },
        { text: "Curva", value: "CURVA",align:"end", divider:false, width: "12%", sortable: true },
      ];
      this.tableitems=[];
      this.total_itens=0;
      await api.post('/ReportCFDayItemsABC?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal +'&A=' +this.filtros[0].curvaA +'&B=' + this.filtros[0].curvaB +'&C=' + this.filtros[0].curvaC + '&CurvaSel=' + e)
          .then(response => {
              for (let c = 0; c < response.data.length; c++) {
                  this.tableitems.push(response.data[c]);
              }
              this.total_itens = ((e == "A") ? this.filtros[0].curvaA : (e == "B") ?  this.filtros[0].curvaB  : this.filtros[0].curvaC) + "%";

          })
          .catch(error => console.log(error));
    },
    limpaDados(){
        this.chartData.labels=[];
        this.chartData.datasets[0].data=[];
        this.tableitems=[];
        this.total_grafico_bar =0;
        this.total_itens=0;
    }
  },
  created(){
  }
}

</script>
