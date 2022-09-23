<template>
  <sidebar-layout-vue>
    <v-container class="fill-height flex-column">
        <v-card :width="800"
          height="100%"
          class="mx-auto"
          :max-height="maxHeight"
          outilined
          elevation="3">
          <v-container>
            
            <!--CABEÇALHO-->
            <v-row dense>
              <v-col cols="12">
                <v-toolbar flat
                        rounded
                        dense
                        class="blue-grey lighten-4">
                  <v-toolbar-title class="font-weight-medium">{{title}}</v-toolbar-title> 
                </v-toolbar>
              </v-col>
            </v-row>

            <v-container>
              <v-row dense>
                <v-col  cols="4"
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
                          persistent-hint
                          prepend-icon="mdi-calendar"
                          v-bind="attrs"
                          @blur="date = parseDate(dateFormatted)"
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
                <v-col  cols="4" 
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
                          prepend-icon="mdi-calendar"
                          v-bind="attrs"
                          @blur="date2 = parseDate(dateFormatted2)"
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
                <v-col cols="3" 
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
                      style="display: inline-block"> 
                  <v-tooltip bottom color="primary">
                          <template v-slot:activator="{on, attrs}">
                            <v-btn icon
                                color="primary"
                                v-bind="attrs"
                                v-on="on"
                                :disabled="false"
                                @click="filtros">
                                <v-icon>mdi-magnify</v-icon>
                            </v-btn>
                          </template>
                          <span>Filtrar</span>
                    </v-tooltip>
                </v-col>
              </v-row>
            </v-container>

            <!--CHART-->
            <v-container>
              <v-row dense>
                <Bar
                  :chart-options="chartOptions"
                  :chart-data="dadosGrafico"
                  :chart-id="chartId"
                  :dataset-id-key="datasetIdKey"
                  :plugins="plugins"
                  :css-classes="cssClasses"
                  :styles="styles"
                  :width="width"
                  :height="height"
                  />
              </v-row>
            </v-container>

            <!--ITEMS-->
            <v-container v-show="show_table">
              <v-row dense>
                <v-col>
                  <v-data-table no-data-text="Nenhum Registro Disponível"
                                no-results-text="Nenhum Registro Encontrado"
                                dense
                                fixed-header
                                height="352px"
                                class="elevation-3"
                                item-key="Id"
                                single-select
                                hide-default-footer
                                :headers="tableHeader"
                                :items="tableItems"
                                :items-per-page="10">
                    <template v-slot:item.actions="{item}">
                        <v-tooltip bottom color="primary">
                          <template v-slot:activator="{on, attrs}">
                            <v-icon small
                                color="primary"
                                v-bind="attrs"
                                v-on="on"
                                class="mr-2"
                                :disabled="false"
                                @click="onEditItem(item)">
                                mdi-pencil
                            </v-icon>
                            <!--<router-link :to="`/Users-Editar/${item.id}`"> alterar</router-link>-->
                          </template>
                          <span>Alterar</span>
                        </v-tooltip>
                        <v-tooltip bottom color="primary">
                          <template v-slot:activator="{on, attrs}">
                            <v-icon small
                                color="primary"
                                v-bind="attrs"
                                v-on="on"
                                class="mr-2"
                                :disabled="false"
                                @click="onDeleteItem(item)">
                                mdi-delete
                            </v-icon>
                          </template>
                          <span>Excluir</span>
                        </v-tooltip>
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
import api from '@/services/api.ts';
import axios from 'axios'
import moment from 'moment'
import { Bar } from 'vue-chartjs/legacy'
import ReportCFDay from '../reports/ReportCFDay.vue';
import { ref } from 'vue';
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

export default {
  name:"FormList",
  components: {
    SidebarLayoutVue,
    ReportCFDay,
    Bar
  },
  props: {
      title: "",
      show_ind: true,
      show_table: true,
      tableHeader: [],
      tableItems: [],
      maxHeight:"",
      dadosGrafico: {
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
      }
  },
  data: vm => ({
      field_ind: [],
      ind_operacao: [{id: 0, name: "Entrada"},{id: 1, name: "Saida"}],
      list: undefined,
      list2: [],
      dadosFiltro:[],
      loaded: false,
      date: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
      date2: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
      dateFormatted: vm.formatDate((new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)),
      dateFormatted2: vm.formatDate((new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)),
      menu1: false,
      menu2: false,
      pageCount: 0,
      pageNumber: 0,
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
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false
      }
  }),
  computed: {
    computedDateFormatted () {
      return this.formatDate(this.date)
    }
  },
  watch: {
    date (val) {
      this.dateFormatted = this.formatDate(this.date)
    },
    date2 (val) {
      this.dateFormatted2 = this.formatDate(this.date2)
    }
  },
  methods: {
      formatDate (date) {
        if (!date) return null

        const [year, month, day] = date.split('-')
        return `${day}/${month}/${year}`
      },
      parseDate (date) {
        if (!date) return null

        const [day, month, year] = date.split('/')
        return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
      },
      return () {
        this.$route.go(-1);
      },
      filtros(){
        
          if(this.validacoes()==true){
            this.dadosFiltro = [ 
                      { dataInicial: this.date, dataFinal: this.date2, ind: this.field_ind.id },
            ];
            this.$emit("filtros",this.dadosFiltro);
          }
      },
      validacoes(){
        let retorno = true;
        return retorno;
      }
  },
  created () {
      this.loaded = false;
      //data teste
      this.date = "2021-06-01";
      this.date2 = "2021-06-30";
      //this.dadosGrafico = this.chartData;
  }
}
</script>
