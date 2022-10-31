<template>
  <sidebar-layout-vue>
    <v-container class="fill-height flex-column">
        <v-card :width="1200"
          height="100%"
          class="mx-auto"
          :max-height="maxHeight"
          outilined
          elevation="3">
          <v-container>
            
            <!--CABEÇALHO-->
            <v-container>
              <v-row dense>
              <v-col cols="1">
                <v-toolbar flat
                        rounded
                        dense
                        class="blue-grey lighten-4">
                  <v-tooltip bottom color="primary">
                        <template v-slot:activator="{on, attrs}">
                          <v-btn icon
                              color="primary"
                              v-bind="attrs"
                              v-on="on"
                              :disabled="false"
                              @click="retornaRota">
                              <v-icon>mdi-arrow-left-circle-outline</v-icon>
                          </v-btn>
                        </template>
                        <span>Retornar</span>
                  </v-tooltip>
                </v-toolbar>
              </v-col>
              <v-col cols="11">
                <v-toolbar flat
                        rounded
                        dense
                        class="blue-grey lighten-4">
                  <v-toolbar-title class="font-weight-medium">{{title}}</v-toolbar-title> 
                </v-toolbar>
              </v-col>
            </v-row>
              <v-row dense>
                <v-col  cols="2"
                        style="display: inline-block">
                    <v-menu
                      ref="menu1"
                      v-model="this.menu1"
                      :close-on-content-click="false"
                      transition="scale-transition"
                      offset-y
                      max-width="290px"
                      min-width="auto">
                      <template v-slot:activator="{ on, attrs }">
                        <v-text-field
                          v-model="dateFormatted"
                          label="Data Inicial"
                          readonly
                          persistent-hint
                          prepend-icon="mdi-calendar"
                          v-bind="attrs"
                          @blur="date = validation.parseDate(dateFormatted)"
                          v-on="on"
                        ></v-text-field>
                      </template>
                      <v-date-picker
                        v-model="date"
                        no-title
                        @input="menu1 = false"
                      ></v-date-picker>
                    </v-menu>
                </v-col>
                <v-col  cols="2" 
                        style="display: inline-block">
                    <v-menu
                      ref="menu2"
                      v-model="this.menu2"
                      :close-on-content-click="false"
                      transition="scale-transition"
                      offset-y
                      max-width="290px"
                      min-width="auto">
                      <template v-slot:activator="{ on, attrs }">
                        <v-text-field
                          v-model="dateFormatted2"
                          label="Data Final"
                          persistent-hint
                          readonly
                          prepend-icon="mdi-calendar"
                          v-bind="attrs"
                          @blur="date2 = validation.parseDate(dateFormatted2)"
                          v-on="on"
                        ></v-text-field>
                      </template>
                      <v-date-picker
                        v-model="date2"
                        no-title
                        @input="menu2 = false"
                      ></v-date-picker>
                    </v-menu>
                </v-col>
                <v-col cols="2" 
                      v-show="show_ind" 
                      style="display: inline-block"> 
                      <v-select label="Ind. Operação"
                                v-model="field_ind"
                                item-value="id"
                                item-text="name"
                                :items="ind_operacao"
                                return-object>
                      </v-select>
                </v-col>
                <v-col cols="1" 
                      v-show="show_mod" 
                      style="display: inline-block"> 
                      <v-select label="Ind. Operação"
                                v-model="field_mod"
                                item-value="mod"
                                item-text="mod"
                                :items="[{mod:55},{mod:65}, {mod:'2D'},{mod:'SE'}]"
                                >
                      </v-select>
                </v-col>
                <v-col cols="2"
                  v-show="show_numdoc"
                  style="display: inline-block" >
                  <v-text-field
                      v-model="field_num_doc"
                      label="Núm. Doc."
                      :value="this.field_num_doc = (this.field_num_doc== null) ? 0 : this.field_num_doc"
                      type="Number"
                      @blur="onFieldNumDoc"
                      
                    ></v-text-field>
                </v-col>
                <v-col cols="2"
                  v-show="show_numred"
                  style="display: inline-block" >
                  <v-text-field
                      v-model="field_num_red"
                      label="Núm. Redução Z"
                      :value="this.field_num_red = (this.field_num_red== null) ? 0 : this.field_num_red"
                      type="Number"
                      @blur="onFieldNumRed"
                      
                    ></v-text-field>
                </v-col>
                <v-col cols="1"
                      style="display: inline-block"> 
                  <v-tooltip bottom color="primary">
                          <template v-slot:activator="{on, attrs}">
                            <v-btn icon
                                color="primary"
                                v-bind="attrs"
                                v-on="on"
                                :disabled="false"
                                @click="botaoFiltrar">
                                <v-icon>mdi-magnify</v-icon>
                            </v-btn>
                          </template>
                          <span>Filtrar</span>
                    </v-tooltip>
                </v-col>
              </v-row>
              <template>
                  <v-container fluid v-show="show_campos">
                    <v-row
                      align="center"
                    >
                      <v-col
                        cols="12"
                        SM="6"
                      >
                      <v-select
                        label="Campos Notas"
                        v-model="camposSelecNotas"
                        item-value="id"
                        item-text="name"
                        :items="camposNotas"
                        chips
                        multiple
                        outlined
                        return-object
                      ></v-select>
                      </v-col>
                  </v-row>
                </v-container>
              </template>
              <template>
                  <v-container fluid v-show="show_campos_items">
                    <v-row
                      align="center"
                    >
                      <v-col
                        cols="12"
                        SM="6"
                      >
                      <v-select
                        label="Campos Items"
                        v-model="camposSelecItems"
                        item-value="id"
                        item-text="name"
                        :items="camposItems"
                        chips
                        multiple
                        outlined
                        return-object
                      ></v-select>
                      </v-col>
                  </v-row>
                </v-container>
              </template>
            </v-container>

            <!--CHART-->
            <v-container v-show="show_chart_bar">
              <v-row dense>
                <v-col cols="8">
                  <v-toolbar flat
                          rounded
                          dense
                          class="blue-grey lighten-4">
                    <v-toolbar-title class="font-weight-medium">{{title_grafico}}</v-toolbar-title> 
                  </v-toolbar>
                </v-col>
                <v-col cols="4">
                  <v-toolbar flat
                          rounded
                          dense
                          class="blue-grey lighten-4">
                    <v-toolbar-title class="font-weight-medium">{{total_grafico_bar}}</v-toolbar-title> 
                  </v-toolbar>
                </v-col>
                <v-col cols="12">
                <Bar
                  :chart-options="chartOptions"
                  :chart-data="dadosGraficoBar"
                  :chart-id="chartId"
                  :dataset-id-key="datasetIdKey"
                  :plugins="plugins"
                  :css-classes="cssClasses"
                  :styles="styles"
                  :width="width"
                  :height="height"
                  />
                </v-col>
              </v-row>
            </v-container>

            <!--CURVA ABC-->
            <v-container v-show="show_chart_pie">
              <v-row dense>
                  <v-col cols="12">
                    <v-toolbar flat
                            rounded
                            dense
                            class="blue-grey lighten-4">
                      <v-toolbar-title class="font-weight-medium">{{title_curva}}</v-toolbar-title> 
                    </v-toolbar>
                  </v-col>
                  <v-col cols="8">
                  <Pie
                      :chart-options="chartABCOptions"
                      :chart-data="dadosGraficoPie"
                      :chart-id="chartIdPIE"
                      :dataset-id-key="datasetIdKeyPIE"
                      :plugins="pluginsPIE"
                      :css-classes="cssClassesPIE"
                      :styles="stylesPIE"
                      :width="widthPIE"
                      :height="heightPIE"
                    />
                    
                  </v-col>
                    <v-col cols="1"
                        style="display: inline-block">
                    <v-text-field
                      v-model="curva_A"
                      label="A (%)"
                      :value="this.curva_A = (this.curva_A== null) ? 0 : this.curva_A"
                      type="Number"
                      required
                    ></v-text-field>
                  </v-col>
                  <br />
                  <br />

                  <v-col cols="1"
                        style="display: inline-block">
                    <v-text-field
                      v-model="curva_B"
                      label="B (%)"
                      :value="this.curva_B = (this.curva_B== null) ? 0 : this.curva_B"
                      type="Number"
                      required
                    ></v-text-field>
                  </v-col>

                  <v-col cols="1"
                        style="display: inline-block">
                    <v-text-field
                      v-model="curva_C"
                      label="C (%)"
                      :value="this.curva_C = (this.curva_C== null) ? 0 : this.curva_C"
                      type="Number"
                      required
                    ></v-text-field>
                  </v-col>

                  <v-col cols="1"
                      style="display: inline-block"> 
                  <v-tooltip bottom color="primary">
                          <template v-slot:activator="{on, attrs}">
                            <v-btn icon
                                color="primary"
                                v-bind="attrs"
                                v-on="on"
                                :disabled="false"
                                @click="botaoFiltrarABC">
                                <v-icon>mdi-refresh</v-icon>
                            </v-btn>
                          </template>
                          <span>Atualiza Gráfico ABC</span>
                    </v-tooltip>
                </v-col>
                  
              </v-row>
            </v-container>

             <!--NOTAS-->
             <v-container v-show="show_table_notas">
              <v-row dense>
                <v-col cols="8">
                  <v-toolbar flat
                          rounded
                          dense
                          class="blue-grey lighten-4">
                    <v-toolbar-title class="font-weight-medium">{{title_notas}}</v-toolbar-title> 
                  </v-toolbar>
                </v-col>
                <v-col cols="4">
                    <v-toolbar flat
                            rounded
                            dense
                            class="blue-grey lighten-4">
                      <v-toolbar-title class="font-weight-medium">{{total_notas}}</v-toolbar-title> 
                    </v-toolbar>
                  </v-col>
              </v-row>

            
              <v-row fluid>
                <v-col>
                  <v-data-table no-data-text="Nenhum Registro Disponível"
                                no-results-text="Nenhum Registro Encontrado"
                                dense
                                fixed-header
                                :height=250
                                class="elevation-0 list-data-table"
                                single-select
                                hide-default-footer
                                calculate-widths
                                :headers="tableHeaderNotas"
                                :items="tableItemsNotas"
                                :items-per-page="-1">
                  </v-data-table>
                </v-col>
              </v-row>
            </v-container>

            <!--ITEMS-->
            <v-container v-show="show_table">
              <v-row dense>
                <v-col cols="8">
                  <v-toolbar flat
                          rounded
                          dense
                          class="blue-grey lighten-4">
                    <v-toolbar-title class="font-weight-medium">{{title_itens}}</v-toolbar-title> 
                  </v-toolbar>
                </v-col>
                <v-col cols="4">
                    <v-toolbar flat
                            rounded
                            dense
                            class="blue-grey lighten-4">
                      <v-toolbar-title class="font-weight-medium">{{total_itens}}</v-toolbar-title> 
                    </v-toolbar>
                  </v-col>
              </v-row>

            
              <v-row fluid>
                <v-col>
                  <v-data-table no-data-text="Nenhum Registro Disponível"
                                no-results-text="Nenhum Registro Encontrado"
                                dense
                                fixed-header
                                :height=352
                                class="elevation-0 list-data-table"
                                single-select
                                hide-default-footer
                                calculate-widths
                                :headers="tableHeader"
                                :items="tableItems"
                                :items-per-page="10"
                                :page.sync="pageNumber">
                    <template v-slot:item.actions="{item}">
                    </template>
                  </v-data-table>
                </v-col>
              </v-row>
              <v-row dense justify="center">
                <v-pagination v-if="pageCount>0"
                      v-model="pageNumber"
                      circle
                      prev-icon="mdi-menu-left"
                      next-icon="mdi-menu-right"
                      :length="pageCount"></v-pagination>
              </v-row>
            </v-container>

          </v-container>
        </v-card>
    </v-container>
  </sidebar-layout-vue>
</template>

<script>

import SidebarLayoutVue from "@/layouts/SidebarLayout.vue";
import { Bar, Pie } from 'vue-chartjs/legacy'
import ReportCFDay from '../reports/ReportCFDay.vue';
import validation from '../services/validation.ts';


import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  ArcElement,
  BarElement,
  CategoryScale,
  LinearScale
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)
ChartJS.register(Title, Tooltip, Legend, ArcElement, CategoryScale)

//export const EventBus = new Vue();
export default {
  name:"FormList",
  components: {
    SidebarLayoutVue,
    ReportCFDay,
    Bar,
    Pie
  },
  props: {
      title: "",
      title_notas: "",
      title_itens: "",
      title_curva: "",
      title_grafico:"",
      total_grafico_bar: 0,
      total_notas:0,
      total_itens:0,
      show_ind: false,
      show_mod: false,
      show_numdoc: false,
      show_numred: false,
      show_chart_bar: false,
      show_chart_pie: false,
      show_table: false,
      show_table_notas: false,
      show_campos: false,
      show_campos_items: false,
      tableHeaderNotas: [],
      tableItemsNotas: [],
      tableHeader: [],
      tableItems: [],
      maxHeight:"",
      dadosGraficoBar: {
        colunas: [],
        dados: []
      },
      dadosGraficoPie: {
        colunas: [],
        dados: []
      },
      chartId: {
        type: String,
        default: 'bar-chart'
      },
      datasetIdKey: {
        type: String,
        default: 'label'
      },
      width: {
        type: Number,
        default: 750
      },
      height: {
        type: Number,
        default: 400
      },
      cssClasses: {
        default: '',
        type: String
      },
      styles: {
        type: Object,
        default: () => {}
      },
      plugins: {
        type: Array,
        default: () => []
      },
      chartIdPIE: {
        type: String,
        default: 'pie-chart'
      },
      datasetIdKeyPIE: {
        type: String,
        default: 'label'
      },
      widthPIE: {
        type: Number,
        default: 400
      },
      heightPIE: {
        type: Number,
        default: 200
      },
      cssClassesPIE: {
        default: '',
        type: String
      },
      stylesPIE: {
        type: Object,
        default: () => {}
      },
      pluginsPIE: {
        type: Array,
        default: () => []
      },
      camposNotas:[],
      camposItems:[],
  },
  data: vm => ({
      field_ind: [],
      field_mod:"",
      field_num_doc:  0,
      field_num_red:  0,
      ind_operacao: [{id: 0, name: "Entrada"},{id: 1, name: "Saida"}],
      dadosFiltro:[],
      date: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
      date2: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
      dateFormatted: validation.formatDate((new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)),
      dateFormatted2: validation.formatDate((new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)),
      menu1: false,
      menu2: false,
      pageCount: 0,
      pageNumber: 0,
      pageCountNotas: 0,
      pageNumberNotas: 0,
      curva_A: 80,
      curva_B: 15,
      curva_C: 5,
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
              backgroundColor: '#EF5350',
              data: [40, 20, 12, 39, 10, 40, 39, 80, 40, 20, 12, 11]
            }
          ]
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false,
        onClick: function (event, chartElements) {
            if(chartElements.length > 0){
                vm.getColGraficoBar(event.chart.data.labels[chartElements[0].index]);
            }
        },
      },
      chartABC: {
                labels: ['A', 'B', 'C'],
                datasets: [
                  {
                    backgroundColor: ['#41B883', '#00D8FF', '##FFD54F'],
                    data: [75,20,20]
                  }
                ]
          },
      camposSelecNotas:[],
      camposSelecItems:[],
      chartABCOptions: {
        responsive: true,
        maintainAspectRatio: false,
        onClick: function (event, chartElements) {
            if(chartElements.length > 0){
                vm.getColGraficoPie(event.chart.data.labels[chartElements[0].index]);
            }
        },
      }
  }),
  watch: {
    date () {
      this.dateFormatted = validation.formatDate(this.date)
    },
    date2 () {
      this.dateFormatted2 = validation.formatDate(this.date2)
    },
    tableItems(){
      this.setValuesPagination();
    },
    field_ind(){
      //ENTRADA
      if(this.field_ind.id == 0){
        this.dadosFiltro = [ 
            { dataInicial: validation.parseDate(this.dateFormatted), dataFinal: validation.parseDate(this.dateFormatted2), ind: this.field_ind.id, curvaA: this.curva_A, curvaB: this.curva_B, curvaC:this.curva_C, color:'#489999', mod: this.field_mod, doc: this.field_num_doc, campos: this.camposSelecNotas, red: this.field_num_red  },
        ];
        this.$emit("color",this.dadosFiltro);
      }
      //SAIDA
      else if(this.field_ind.id == 1){
        this.dadosFiltro = [ 
            { dataInicial: validation.parseDate(this.dateFormatted), dataFinal: validation.parseDate(this.dateFormatted2), ind: this.field_ind.id, curvaA: this.curva_A, curvaB: this.curva_B, curvaC:this.curva_C, color:'#EF5350', mod: this.field_mod, doc: this.field_num_doc, campos: this.camposSelecNotas, red: this.field_num_red   },
        ];
        this.$emit("color",this.dadosFiltro);
      }
    },
    camposSelecNotas(){
      this.$emit("campos",this.camposSelecNotas);
    },
    camposSelecItems(){
      this.$emit("camposItems",this.camposSelecItems);
    }
  },
  methods: {
      retornaRota() {
        this.$router.go(-1);
      },
      setValuesPagination(){
        this.pageNumber=1;
        if(this.tableItems!= null && this.tableItems.length > 10) {
          this.pageCount = Math.ceil((Number(this.tableItems.length/10)));
        }
        else{
          this.pageCount = 0;
        }
      },
      getColGraficoBar(value){
          this.dadosFiltro = [ 
                    { dataInicial: validation.parseDate(this.dateFormatted), dataFinal: validation.parseDate(this.dateFormatted2), ind: this.field_ind.id, curvaA: this.curva_A, curvaB: this.curva_B, curvaC:this.curva_C, mod: this.field_mod, doc: this.field_num_doc, campos: this.camposSelecNotas, red: this.field_num_red  },
          ];
          this.$emit("colunaGraficoBar",value);
      },
      getColGraficoPie(value){
          this.$emit("colunaGraficoPie",value);
      },
      botaoFiltrar(){
          if(this.validacoes()==true){
              this.dadosFiltro = [ 
                        { dataInicial: validation.parseDate(this.dateFormatted), dataFinal: validation.parseDate(this.dateFormatted2), ind: this.field_ind.id, curvaA: this.curva_A, curvaB: this.curva_B, curvaC:this.curva_C, mod: this.field_mod, doc: this.field_num_doc, campos: this.camposSelecNotas, red: this.field_num_red  },
              ];
              this.$emit("botaoFiltrar",this.dadosFiltro);
          }
      },
      botaoFiltrarABC(){
          if((parseFloat(this.curva_A) + parseFloat(this.curva_B) + parseFloat(this.curva_C)) != 100 || parseFloat(this.curva_A) < 0 || parseFloat(this.curva_B) < 0 || parseFloat(this.curva_C) < 0)
          {
            this.curva_A = 80; 
            this.curva_B = 15;
            this.curva_C = 5;
          }
          this.dadosFiltro = [ 
                    { dataInicial: validation.parseDate(this.dateFormatted), dataFinal: validation.parseDate(this.dateFormatted2), ind: this.field_ind.id, curvaA: this.curva_A, curvaB: this.curva_B, curvaC:this.curva_C, mod: this.field_mod, doc: this.field_num_doc, campos: this.camposSelecNotas, red: this.field_num_red  },
          ];
          this.$emit("botaoFiltrarABC",this.dadosFiltro);
      },
      validacoes(){
        let retorno = true;
        if(this.field_ind.id == undefined && this.show_ind == true){
            retorno=false;
        }
        return retorno;
      },
      onFieldNumDoc(){
          if(this.field_num_doc < 0 || this.field_num_doc == ''){
              this.field_num_doc = 0;
          }
      },
      onFieldNumRed(){
          if(this.field_num_red < 0 || this.field_num_red == ''){
              this.field_num_red = 0;
          }
      }
  },
  created () {
      //data teste
      this.date = "2021-06-01";
      this.date2 = "2021-06-30";

      this.setValuesPagination();

      //Inicia como Primeiro Index
      this.field_ind = {id: 0, name: "Entrada"};
      this.field_mod =  55;
      
      if(this.width == null || this.width < 0){
        this.width = 800;
      } 
      //this.dadosGraficoBar = this.chartData;
      //this.dadosGraficoPie = this.chartABC;
  }
}
</script>

<style scoped>

</style>