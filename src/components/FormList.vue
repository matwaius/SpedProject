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
            <v-row dense>
              <v-col cols="12">
                <v-toolbar flat
                        rounded
                        dense
                        class="blue-grey lighten-4">
                  <v-toolbar-title class="font-weight-medium">{{title}}</v-toolbar-title>
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
            <v-row dense>
              <v-col>
                <v-data-table no-data-text="Nenhum Registro Disponível"
                              no-results-text="Nenhum Registro Encontrado"
                              dense
                              fixed-header
                              height="352px"
                              class="elevation-3"
                              item-key="id"
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
                              class="mr-2">
                              mdi-pencil
                          </v-icon>
                        </template>
                        <span>Alterar</span>
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
import SidebarLayoutVue from "@/layouts/SidebarLayout.vue";


export default {
  name:"FormList",
  components: {
    SidebarLayoutVue
  },
  props: {
      title: String,
      tableHeader: [],
      tableItems: [],
      fieldSearch: ""
  },
  data() {
    return {
      pageCount: 0,
      pageNumber: 0,
      searchlabel:"Pesquisar",
    }
  },
}

</script>
