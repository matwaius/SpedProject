<template>
  <div>
    <Dashboard
        title="Relatório de Nota Fiscal por UF"
        title_grafico = "Gráfico"
        title_notas = "Notas"
        title_itens = "Itens"
        title_curva = "Curva ABC"
        :total_grafico_bar="this.total_grafico_bar"
        :total_notas="this.total_notas"
        :total_itens="this.total_itens"
        :show_ind=true
        :show_chart_bar="true"
        :show_chart_pie="false"
        :show_table_notas="true"
        :show_table="true"
        :tableHeaderNotas="tableheaderNotas"
        :tableItemsNotas="tableitemsNotas"
        :tableHeader="tableheader"
        :tableItems="tableitems"
        :dadosGraficoBar="chartData"
        :dadosGraficoPie="chartABC"
  
        @botaoFiltrar ="setaFiltros"
        @botaoFiltrarABC ="setaFiltrosABC"
        
        @colunaGraficoBar="getNotas" 

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
    name: 'ReportNFDayByUF',
    data: vm => ({
        dateInicial: "",
        dateFinal: "",
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
          await api.post('/ReportNFDayByUF?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal+'&indOperacao='+this.filtros[0].ind)
            .then(response => {
                this.chartData.datasets[0].label="Dias";
                for (let i = 0; i < response.data.length; i++) {
                  this.chartData.labels.push(response.data[i].UF);
                  this.chartData.datasets[0].data.push(response.data[i].VL_DOC );
                  this.total_grafico_bar = Math.round(this.total_grafico_bar* 100) / 100  + Math.round((response.data[i].VL_DOC)* 100) / 100 ; 
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
      async getNotas(e){
        this.tableheaderNotas=[
              { text: "Cód.", value: "COD_PART", align:"start", width: 100, divider:false, sortable: true },
              { text: "Nome", value: "NOME", align:"start", width: 250, divider:false, sortable: true },
              { text: "Mod.", value: "COD_MOD", align:"end", width: 100, divider:false, sortable: true },
              { text: "Num Doc.", value: "NUM_DOC",align:"end", divider:false, width: 150,sortable: true },
              { text: "Valor", value: "VL_DOC",align:"end",width: 150, divider:false, sortable: true },
              { text: "Desconto", value: "VL_DESC",align:"end", width: 150, divider:false,  sortable: true },
              { text: "Frete", value: "VL_FRT",align:"end", divider:false, width: 100, sortable: true },
          ];
        this.tableitemsNotas=[];
        this.total_notas=0;
        this.tableheader=[
              { text: "Cód. Item", value: "COD_ITEM", align:"start", divider:false, sortable: true },
              { text: "Descrição", value: "DESCR_COMPL", align:"start", width: 300, divider:false, sortable: true },
              { text: "Qtd.", value: "QTD",align:"end", divider:false, width: 100,sortable: true },
              { text: "Preço Total", value: "VL_ITEM",align:"end",width: 150, divider:false, sortable: true },
              { text: "Desconto", value: "VL_DESC",align:"end", width: 150, divider:false,  sortable: true },
              { text: "UN", value: "UNID",align:"end", divider:false, width: 100, sortable: true },
              { text: "CFOP", value: "CFOP",align:"end", divider:false,width: 100,  sortable: true },
              { text: "BC ICMS", value: "VL_BC_ICMS",align:"end", width: 200,divider:false, sortable: true },
              { text: "ICMS", value: "ALIQ_ICMS",align:"end", width: 100, divider:false, sortable: true },
              { text: "Valor ICMS", value: "VL_ICMS",align:"end", width: 150, divider:false,  sortable: true },
          ];
        this.tableitems=[];
        this.total_itens=0;
        await api.post('/ReportNFDayItemsByUF?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal+'&UF=' + e+'&indOperacao='+this.filtros[0].ind)
            .then(response => {
              for (let i = 0; i < response.data.length; i++) {
                    this.tableitemsNotas.push(response.data[i]);
                    this.total_notas = Math.round(this.total_notas* 100) / 100  + Math.round((response.data[i].VL_DOC - response.data[i].VL_FRT)* 100) / 100 ; 
                    for (let c = 0; c < response.data[i].Itens.length; c++) {
                        this.tableitems.push(response.data[i].Itens[c]);
                        this.total_itens = Math.round(this.total_itens* 100) / 100  + Math.round((response.data[i].Itens[c].VL_ITEM - response.data[i].Itens[c].VL_DESC)* 100) / 100 ; 
                    }
                }
                this.total_notas = this.total_notas.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
                this.total_itens = this.total_itens.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
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
          this.tableitemsNotas=[];
          this.tableitems=[];
          this.total_grafico_bar =0;
          this.total_notas=0;
          this.total_itens=0;
      }
    },
    created(){
    }
  }
  
  </script>
  