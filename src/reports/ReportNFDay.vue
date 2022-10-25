<template>
  <div>
    <Dashboard
        title="Relatório de Nota Fiscal por Dia"
        title_grafico = "Gráfico"
        title_notas = "Notas Fiscais"
        title_itens = "Itens"
        title_curva = "Curva ABC"
        :total_grafico_bar="this.total_grafico_bar"
        :total_notas="this.total_notas"
        :total_itens="this.total_itens"
        :show_ind="true"
        :show_mod="true"
        :show_numdoc="true"
        :show_chart_bar="true"
        :show_chart_pie="true"
        :show_table_notas="true"
        :show_table="true"
        :show_campos="true"
        :show_campos_items="true"
        :tableHeaderNotas="tableheaderNotas"
        :tableItemsNotas="tableitemsNotas"
        :tableHeader="tableheader"
        :tableItems="tableitems"
        :dadosGraficoBar="chartData"
        :dadosGraficoPie="chartABC"
        :camposNotas="camposNotas"
        :camposItems="camposItems"
  
        @botaoFiltrar ="setaFiltros"
        @botaoFiltrarABC ="setaFiltrosABC"
        
        @colunaGraficoBar="getNotas" 
        @colunaGraficoPie="getItemsABC"

        @color="changeColor"
        @campos="insertFieldsNotas"
        @camposItems="insertFieldsItems"
        >
    </Dashboard>
  </div>
  </template>
  
  <script>
  import Dashboard from '@/components/Dashboard.vue'
  import api from '@/services/api.ts';
  import validation from '../services/validation.ts';
  
  export default {
    name: 'ReportNFDay',
    data: vm => ({
        dateInicial: "",
        dateFinal: "",
        loading: false,
        headerCurvaABC: false,
        indOperacao: "",
        modelo: "",
        total_grafico_bar: 0,
        total_notas:0,
        total_itens:0,
        tableheaderNotas: [],
        tableitemsNotas:[],
        tableheader: [],
        tableitems:[],
        filtros:[],
        camposNotas:[{id:'IND_OPER', name:"Ind. Operação"},{id:'IND_EMIT', name:"Ind. Emitente"},{id:'COD_SIT', name:"Sit. Doc"},{id:'SER', name:"Série Doc."},{id:'CHV_NFE', name:"Chave Doc."},
                    {id:'IND_PGTO', name:"Ind. Pagto"}, {id:'VL_ABAT_NT', name:"Abatimento Não Trib."}, {id:'IND_FRT', name:"Ind. Frete"}, {id:'VL_SEG', name:"Valor Seguro"}, 
                    {id:'VL_OUT_DA', name:"Valor Outr. Desp."},{id:'VL_BC_ICMS', name:"Base Calc. ICMS"},{id:'VL_ICMS', name:"Valor ICMS"},{id:'VL_BC_ICMS_ST', name:"Base Calc. ICMS ST"},
                    {id:'VL_ICMS_ST', name:"Valor ICMS ST"},{id:'VL_PIS', name:"Valor PIS"},{id:'VL_COFINS', name:"Valor COFINS"},{id:'VL_IPI', name:"Valor IPI"}],

        camposItems:[{id:'NUM_ITEM', name:"Num. Item"},{id:'IND_MOV', name:"Ind. Movimento"},{id:'CST_ICMS', name:"CST"},{id:'COD_NAT', name:"Cód. Nat."},{id:'VL_BC_ICMS', name:"Base Calc. ICMS"},{id:'VL_ICMS', name:"Valor ICMS"},
                    {id:'VL_BC_ICMS_ST', name:"Base Calc. ICMS ST"},{id:'VL_ICMS_ST', name:"Valor ICMS ST"},{id:'IND_APUR', name:"Ind. Apuração"}, {id:'CST_IPI', name:"CST IPI"}, {id:'COD_ENQ', name:"Cód. Enquadramento"}, 
                    {id:'VL_BC_IPI', name:"Base Calc. IPI"}, {id:'VL_IPI', name:"Valor IPI"}, {id:'ALIQ_IPI', name:"Aliq. IPI"}, {id:'CST_PIS', name:"CST PIS"}, {id:'VL_BC_PIS', name:"Base Calc. PIS"},{id:'VL_PIS', name:"Valor PIS"},{id:'ALIQ_PIS_PERC', name:"Aliq. PIS"},
                    {id:'VL_BC_COFINS', name:"Base Calc. COFINS"},{id:'VL_COFINS', name:"Valor COFINS"},{id:'ALIQ_COFINS_PERC', name:"Aliq. COFINS"},{id:'COD_CTA', name:"Cód. Conta Analitica"},{id:'VL_ABAT_NT', name:"Abatimento Não Trib."}],
        chartData: {
            labels: [
               //teste
              '01/06/2021','02/06/2021','03/06/2021','04/06/2021','05/06/2021','06/06/2021','07/06/2021','08/06/2021','09/06/2021','10/06/2021'
            ],
            datasets: [
              {
                label: 'NF Dia',
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
        if(this.loading == false){
            this.loading = true;
            this.limpaDados();
            try {
              await api.post('/ReportNFDay?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal+'&indOperacao='+this.filtros[0].ind+'&mod='+this.filtros[0].mod+'&doc='+this.filtros[0].doc)
                .then(response => {
                    this.chartData.datasets[0].label="Valor";
                    for (let i = 0; i < response.data.length; i++) {
                      this.chartData.labels.push(response.data[i].DT_DOC);
                      this.chartData.datasets[0].data.push(response.data[i].VL_DOC );
                      this.total_grafico_bar = Math.round(this.total_grafico_bar* 100) / 100  + Math.round((response.data[i].VL_DOC)* 100) / 100 ; 
                    }
                    this.total_grafico_bar = this.total_grafico_bar.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
                    this.loading = false;
                })
                .catch(error => console.log(error))
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
          this.tableheaderNotasPadrao();
          this.tableheaderItemsPadrao()
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
            if(this.tableheaderNotas == null || this.tableheaderNotas.length == 0 ) {
                this.tableheaderNotasPadrao();
            }
            if(this.tableheader == null || this.tableheader.length == 0 || this.headerCurvaABC == true) {
                this.tableheaderItemsPadrao();
                this.headerCurvaABC = false;
            }
            this.tableitemsNotas=[];
            this.total_notas=0;;
            this.tableitems=[];
            this.total_itens=0;
            await api.post('/ReportNFDayItems?date=' + validation.parseDate(e) +'&indOperacao='+this.filtros[0].ind+'&mod='+this.filtros[0].mod+'&doc='+this.filtros[0].doc)
                .then(response => {
                  for (let i = 0; i < response.data.length; i++) {
                        this.tableitemsNotas.push(response.data[i]);
                        this.total_notas = Math.round(this.total_notas* 100) / 100  + Math.round((response.data[i].VL_DOC)* 100) / 100 ; 
                        for (let c = 0; c < response.data[i].Itens.length; c++) {
                            this.tableitems.push(response.data[i].Itens[c]);
                            this.total_itens = Math.round(this.total_itens* 100) / 100  + Math.round((response.data[i].Itens[c].VL_ITEM - response.data[i].Itens[c].VL_DESC)* 100) / 100 ; 
                        }
                    }
                    this.total_notas = this.total_notas.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
                    this.total_itens = this.total_itens.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
                    this.loading = false;
                })
                .catch(error => console.log(error));
          }
      },
      async getItemsABC(e){
        if(this.loading == false){
            this.loading = true;
            this.tableheaderItemsABCPadrao();
            this.headerCurvaABC = true;
            this.tableitems=[];
            this.total_itens=0;
            try{
              await api.post('/ReportNFDayItemsABC?dateStart=' + this.filtros[0].dataInicial + '&dateEnd=' + this.filtros[0].dataFinal +'&indOperacao='+this.filtros[0].ind +'&A=' +this.filtros[0].curvaA +'&B=' + this.filtros[0].curvaB +'&C=' + this.filtros[0].curvaC + '&CurvaSel=' + e+'&mod='+this.filtros[0].mod+'&doc='+this.filtros[0].doc)
                  .then(response => {
                      for (let c = 0; c < response.data.length; c++) {
                          this.tableitems.push(response.data[c]);
                      }
                      this.total_itens = ((e == "A") ? this.filtros[0].curvaA : (e == "B") ?  this.filtros[0].curvaB  : this.filtros[0].curvaC) + "%";
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
      async changeColor(e){
          this.filtros =e;
          this.chartData.datasets[0].backgroundColor=this.filtros[0].color;
          this.getRel();
      },
      insertFieldsNotas(e){
          this.itemsNotas =e;
          this.tableheaderNotasPadrao();
          if(this.itemsNotas != null && this.itemsNotas != undefined)
          { 
            for (let i = 0; i < this.itemsNotas.length; i++) {
              this.tableheaderNotas.push( { text: this.itemsNotas[i].name, value: this.itemsNotas[i].id,align:"end", divider:false, width: 200, sortable: true })
            }
          }
      },
      insertFieldsItems(e){
          this.items =e;
          this.tableheaderItemsPadrao();
          if(this.items != null && this.items != undefined)
          { 
            for (let i = 0; i < this.items.length; i++) {
              this.tableheader.push( { text: this.items[i].name, value: this.items[i].id,align:"end", divider:false, width: 200, sortable: true })
            }
          }
      },
      tableheaderNotasPadrao(){
        this.tableheaderNotas=[
                  { text: "Cód.", value: "COD_PART", align:"start", width: 100, divider:false, sortable: true },
                  { text: "Nome", value: "NOME", align:"start", width: 250, divider:false, sortable: true },
                  { text: "Mod.", value: "COD_MOD", align:"end", width: 100, divider:false, sortable: true },
                  { text: "Num Doc.", value: "NUM_DOC",align:"end", divider:false, width: 150,sortable: true },
                  { text: "Valor", value: "VL_MERC",align:"end",width: 125, divider:false, sortable: true },
                  { text: "Desconto", value: "VL_DESC",align:"end", width: 125, divider:false,  sortable: true },
                  { text: "Frete", value: "VL_FRT",align:"end", divider:false, width: 125, sortable: true },
                  { text: "Total", value: "VL_DOC",align:"end",width: 125, divider:false, sortable: true },
              ];
      },
      tableheaderItemsPadrao(){
        this.tableheader=[
                  { text: "Cód. Item", value: "COD_ITEM", align:"start", divider:false, sortable: true },
                  { text: "Descrição", value: "DESCR_COMPL", align:"start", width: 300, divider:false, sortable: true },
                  { text: "Qtd.", value: "QTD",align:"end", divider:false, width: 100,sortable: true },
                  { text: "Preço Total", value: "VL_ITEM",align:"end",width: 150, divider:false, sortable: true },
                  { text: "Desconto", value: "VL_DESC",align:"end", width: 150, divider:false,  sortable: true },
                  { text: "UN", value: "UNID",align:"end", divider:false, width: 100, sortable: true },
                  { text: "CFOP", value: "CFOP",align:"end", divider:false,width: 100,  sortable: true },
              ];
      },
      tableheaderItemsABCPadrao(){
        this.tableheader = [ 
              { text: "Cód. Item", value: "COD_ITEM", align:"start", divider:false, width: "14%", sortable: true },
              { text: "Descrição", value: "DESCR_COMPL", align:"start", divider:false, width: "25%", sortable: true },
              { text: "Qtd.", value: "QTD",align:"end", divider:false, width: "11%", sortable: true },
              { text: "Preco Total", value: "VL_ITEM",align:"end", divider:false, width: "14%", sortable: true },
              { text: "Perc. %", value: "PERC",align:"end", divider:false, width: "24%", sortable: true },
              { text: "Curva", value: "CURVA",align:"end", divider:false, width: "12%", sortable: true },
            ];
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
  