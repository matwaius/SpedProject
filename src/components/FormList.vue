<template>
  <sidebar-layout-vue>
    <v-container class="fill-height flex-column">
        <v-card :width="800"
          height="100%"
          max-height="480"
          class="mx-auto"
          outilined
          elevation="3">
          <v-container>
            <!--CABEÇALHO-->
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
              <v-col cols="10">
                <v-toolbar flat
                        rounded
                        dense
                        class="blue-grey lighten-4">
                  <v-toolbar-title class="font-weight-medium">{{title}}</v-toolbar-title>
                </v-toolbar>
              </v-col>
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
                              @click="onInsertItem">
                              <v-icon>mdi-file-plus</v-icon>
                          </v-btn>
                        </template>
                        <span>Incluir</span>
                  </v-tooltip>
                  </v-toolbar>
              </v-col>
              <v-col cols="12">
                  <v-text-field :label="searchlabel"
                                single-line
                                dense
                                outlined
                                prepend-inner-icon="mdi-magnify"
                                hide-details
                                v-model="fieldSearch"
                                :value="fieldSearch = (fieldSearch== null) ? fieldSearch : fieldSearch.toUpperCase()">

                  </v-text-field>
              </v-col>
            </v-row>
            <!--ITEMS-->
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
        </v-card>
    </v-container>
  </sidebar-layout-vue>
</template>

<script>
import SidebarLayoutVue from '@/layouts/SidebarLayout.vue'
import api from '@/services/api.ts'

export default {
  name: 'FormList',
  components: {
    SidebarLayoutVue
  },
  props: {
    title: String,
    routerInsert: '',
    routerUpdate: '',
    tabela: '',
    tableHeader: [],
    tableItems: [],
    itemDelete: null,
    fieldSearch: '',
    User: {
      props: ['id'],
      mounted () {
        console.log(this.$route.params.id) // this is returning the value
        console.log(this.id) // this is giving undefined
      }
    }
  },
  data () {
    return {
      pageCount: 0,
      pageNumber: 0,
      searchlabel: 'Pesquisar'
    }
  },
  methods: {
    onInsertItem () {
      this.$router.push('/' + this.routerInsert)
    },
    retornaRota () {
      this.$router.go(-1)
    },
    onEditItem (item) {
      this.$router.push({
        name: this.routerUpdate,
        params: {
          id: item.Id
        }
      })
    },
    async onDeleteItem (item) {
      await api.delete(`/${this.tabela}/${item.Id}`)
        .then((response) => {
        })
        .catch((error) => {
          console.log(error.response)
        })
      this.tableItems.splice(this.tableItems.findIndex(f => f == item), 1)
    }
  }
}

</script>
