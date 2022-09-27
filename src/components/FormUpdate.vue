<template>
  <sidebar-layout-vue>
    <v-container class="fill-height flex-column">
        <v-card :width="width"
          height="100%"
          :max-height="maxHeight"
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
              <v-col cols="11">
                <v-toolbar flat
                        rounded
                        dense
                        class="blue-grey lighten-4">
                  <v-toolbar-title class="font-weight-medium">
                    {{title}}
                  </v-toolbar-title> 
                  <!--slot CABEÇALHO-->
                  <slot name="header"></slot>
                </v-toolbar>
              </v-col>
            </v-row>
          
            
          </v-container>

          <!--CAMPOS-->
          <v-container dense>
            <slot name="body"></slot>
          </v-container>

          <!--RODAPE-->

          <v-container>
            <v-row dense
                  justify="end">
                  <slot name="footer"></slot>

                  <v-btn color="sucess"
                        small
                        elevation="5"
                        style="margin:0 15px"
                        @click="onSave"
                        >
                        <v-icon left>
                          mdi-checkbox-marked-circle
                        </v-icon>
                        Salvar
                  </v-btn>

                  <v-btn color="error"
                        small
                        elevation="5"
                        @click="retornaRota"
                        >
                        <v-icon left>
                          mdi-close-circle
                        </v-icon>
                        Cancelar
                  </v-btn>
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
      width: Number,
      maxHeight: Number,
  },
  data() {
    return {
    }
  },
  methods:{
    onSave(){
      this.$emit('onSave')
    },
    retornaRota () {
      this.$router.go(-1);
    },
    
  }
}

</script>
