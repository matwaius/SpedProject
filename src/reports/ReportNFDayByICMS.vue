<template>
  <div>
    <Dashboard
        title="Relatório de Nota Fiscal por ICMS"
        title_grafico = "Gráfico"
        title_itens = "Itens"
        title_curva = "Curva ABC"
        :total_grafico_bar="this.total_grafico_bar"
        :total_itens="this.total_itens"
        :show_ind=true
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

        @color="changeColor"
        >
    </Dashboard>
  </div>
  </template>
  
  <script>
  import Dashboard from '@/components/Dashboard.vue'
  import api from '@/services/api.ts';
  import validation from '../services/validation.ts';
  
  export default {
    name: 'ReportNFDayByICMS',
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
                label: 'ICMS',
                backgroundColor: '#489999',
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
          await api.post('/ReportNFDayByICMS?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal+'&indOperacao='+this.filtros[0].ind)
            .then(response => {
                this.chartData.datasets[0].label="ICMS";
                
                for (let i = 0; i < response.data.length; i++) {
                  this.chartData.labels.push(response.data[i].DT_DOC);
                  this.chartData.datasets[0].data.push(response.data[i].VL_ICMS );
                  this.total_grafico_bar = Math.round(this.total_grafico_bar* 100) / 100  + Math.round((response.data[i].VL_ICMS)* 100) / 100 ; 
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
              { text: "Cód. Item", value: "COD_ITEM", align:"start", divider:false, sortable: true },
              { text: "Descrição", value: "DESCR_COMPL", align:"start", width: 300, divider:false, sortable: true },
              { text: "UN", value: "UNID",align:"end", divider:false, width: 100, sortable: true },
              { text: "CFOP", value: "CFOP",align:"end", divider:false,width: 100,  sortable: true },
              { text: "BC ICMS", value: "VL_BC_ICMS",align:"end", width: 200,divider:false, sortable: true },
              { text: "ICMS", value: "ALIQ_ICMS",align:"end", width: 100, divider:false, sortable: true },
              { text: "Valor ICMS", value: "VL_ICMS",align:"end", width: 150, divider:false,  sortable: true },
          ];
        this.tableitems=[];
        this.total_itens=0;
        await api.post('/ReportNFDayByICMSItems?date=' + validation.parseDate(e)+'&indOperacao='+this.filtros[0].ind)
            .then(response => {
                for (let c = 0; c < response.data.length; c++) {
                    this.tableitems.push(response.data[c]);
                    this.total_itens = Math.round(this.total_itens* 100) / 100  + Math.round((response.data[c].VL_ICMS)* 100) / 100 ; 
                }
                this.total_itens = this.total_itens.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
            })
            .catch(error => console.log(error));
      },
      async getItemsABC(e){
        this.tableheader = [ 
          { text: "Cód. Item", value: "COD_ITEM", align:"start", divider:false, width: 100, sortable: true },
          { text: "Descrição", value: "DESCR_COMPL", align:"start", divider:false, width: 200, sortable: true },
          { text: "BC ICMS", value: "VL_BC_ICMS",align:"end", width: 200,divider:false, sortable: true },
          { text: "ICMS", value: "ALIQ_ICMS",align:"end", width: 100, divider:false, sortable: true },
          { text: "Valor ICMS", value: "VL_ICMS",align:"end", divider:false, width: 100, sortable: true },
          { text: "Perc. %", value: "PERC",align:"end", divider:false, width: 100, sortable: true },
          { text: "Curva", value: "CURVA",align:"end", divider:false, width: 100, sortable: true },
        ];
        this.tableitems=[];
        this.total_itens=0;
        await api.post('/ReportNFDayByICMSItemsABC?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal +'&indOperacao='+this.filtros[0].ind +'&A=' +this.filtros[0].curvaA +'&B=' + this.filtros[0].curvaB +'&C=' + this.filtros[0].curvaC + '&CurvaSel=' + e)
            .then(response => {
                for (let c = 0; c < response.data.length; c++) {
                    this.tableitems.push(response.data[c]);
                }
                this.total_itens = ((e == "A") ? this.filtros[0].curvaA : (e == "B") ?  this.filtros[0].curvaB  : this.filtros[0].curvaC) + "%";
  
            })
            .catch(error => console.log(error));
      },
      async changeColor(e){
          this.filtros =e;
          this.chartData.datasets[0].backgroundColor=this.filtros[0].color;
          this.getRel();
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
  