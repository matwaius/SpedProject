<template>
<div>
  <Dashboard
      title="Relatório de Cupom Fiscal por Dia"
      title_grafico = "Gráfico"
      title_notas = "Cupons Fiscais"
      title_itens = "Itens"
      title_curva = "Curva ABC"
      :total_grafico_bar="this.total_grafico_bar"
      :total_notas="this.total_notas"
      :total_itens="this.total_itens"
      :show_ind=false
      :show_chart_bar="true"
      :show_chart_pie="true"
      :show_table_notas="true"
      :show_table="true"
      :show_numdoc="true"
      :show_numred="true"
      :tableHeaderNotas="tableheaderNotas"
      :tableItemsNotas="tableitemsNotas"
      :tableHeader="tableheader"
      :tableItems="tableitems"
      :dadosGraficoBar="chartData"
      :dadosGraficoPie="chartABC"

      @botaoFiltrar ="setaFiltros"
      @botaoFiltrarABC ="setaFiltrosABC"
      
      @colunaGraficoBar="getNotas" 
      @colunaGraficoPie="getItemsABC"

      @color="changeColor">
  </Dashboard>
</div>
</template>

<script>
import api from '@/services/api.ts';
import validation from '../services/validation.ts';

export default {
  name: 'ReportCFFDay',
  data: vm => ({
      dateInicial: "",
      loading: false,
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
      Dashboard :() => import('@/components/Dashboard.vue')
  },
  methods: { 
    async getRel () {
        if(this.loading == false){
          this.loading = true;
          this.limpaDados();
            try{
              await api.post('/ReportCFDay?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal+'&doc='+this.filtros[0].doc+'&red='+this.filtros[0].red)
                .then(response => {
                    this.chartData.datasets[0].label="Valor";
                    for (let i = 0; i < response.data.length; i++) {
                      this.chartData.labels.push(response.data[i].DT_DOC);
                      this.chartData.datasets[0].data.push(response.data[i].VL_DOC);
                      this.total_grafico_bar = Math.round(this.total_grafico_bar* 100) / 100  + Math.round(response.data[i].VL_DOC* 100) / 100 ; 
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
      if(this.loading == false){
            this.loading = true;
            this.tableheaderNotas=[
                  { text: "Cód.", value: "CPF_CNPJ", align:"start", width: 100, divider:false, sortable: true },
                  { text: "Nome", value: "NOM_ADQ", align:"start", width: 250, divider:false, sortable: true },
                  { text: "Mod.", value: "COD_MOD", align:"end", width: 100, divider:false, sortable: true },
                  { text: "Num Doc.", value: "NUM_DOC",align:"end", divider:false, width: 150,sortable: true },
                  { text: "Valor", value: "VL_DOC",align:"end",width: 150, divider:false, sortable: true },
                  { text: "Valor PIS", value: "VL_PIS",align:"end", width: 150, divider:false,  sortable: true },
                  { text: "Valor COFINS", value: "VL_COFINS",align:"end", divider:false, width: 100, sortable: true },
              ];
            this.tableitemsNotas=[];
            this.total_notas=0;
            this.tableheader=[
                  { text: "Cód. Item", value: "COD_ITEM", align:"start", divider:false, sortable: true },
                  { text: "Descrição", value: "DESCR_ITEM", align:"start", width: 300, divider:false, sortable: true },
                  { text: "Qtd.", value: "QTD",align:"end", divider:false, width: 100,sortable: true },
                  { text: "Qtd. Canc", value: "QTD_CANC",align:"end", divider:false, width: 150,sortable: true },
                  { text: "Preço Total", value: "VL_ITEM",align:"end",width: 150, divider:false, sortable: true },
                  { text: "UN", value: "UNID",align:"end", divider:false, width: 100, sortable: true },
                  { text: "CST", value: "CST_ICMS",align:"end", divider:false,width: 100,  sortable: true },
                  { text: "CFOP", value: "CFOP",align:"end", divider:false,width: 100,  sortable: true },
                  { text: "Aliq. ICMS", value: "ALIQ_ICMS",align:"end", width: 150, divider:false, sortable: true },
                  { text: "Valor PIS", value: "VL_PIS",align:"end", width: 150,divider:false, sortable: true },
                  { text: "Valor COFINS", value: "VL_COFINS",align:"end", width: 150, divider:false,  sortable: true },
              ];
            this.tableitems=[];
            this.total_itens=0;
              try{
                await api.post('/ReportCFDayItems?date=' + validation.parseDate(e)+'&doc='+this.filtros[0].doc+'&red='+this.filtros[0].red)
                    .then(response => {
                      for (let i = 0; i < response.data.length; i++) {
                            this.tableitemsNotas.push(response.data[i]);
                            this.total_notas = Math.round(this.total_notas* 100) / 100  + Math.round((response.data[i].VL_DOC)* 100) / 100 ; 
                            for (let c = 0; c < response.data[i].Itens.length; c++) {
                                this.tableitems.push(response.data[i].Itens[c]);
                                this.total_itens = Math.round(this.total_itens* 100) / 100  + Math.round((response.data[i].Itens[c].VL_ITEM)* 100) / 100 ; 
                            }
                        }
                        this.total_notas = this.total_notas.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
                        this.total_itens = this.total_itens.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
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
        try{
            await api.post('/ReportCFDayItemsABC?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal +'&A=' +this.filtros[0].curvaA +'&B=' + this.filtros[0].curvaB +'&C=' + this.filtros[0].curvaC + '&CurvaSel=' + e)
                .then(response => {
                    for (let c = 0; c < response.data.length; c++) {
                        this.tableitems.push(response.data[c]);
                    }
                    this.total_itens = ((e == "A") ? this.filtros[0].curvaA : (e == "B") ?  this.filtros[0].curvaB  : this.filtros[0].curvaC) + "%";

                })
                .catch(error => console.log(error));
        } catch (error) {
          this.loading = false;
          error => console.log(error)
        }
        finally{
          this.loading = false;
        }
    },
    async changeColor(e){
        this.filtros =e;
        this.chartData.datasets[0].backgroundColor =  ['#EF5350'];//this.filtros[0].color;
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
