<template>
  <sidebar-layout-vue>
    <v-container class="fill-height" fluid>
            <v-card :width="800"
                height="100%"
                class="mx-auto"
                :max-height="225"
                outilined
                elevation="3">
                <v-container>
            <!--CABEÇALHO-->
            <v-progress-linear v-if="showProgress"
              indeterminate
              color="cyan"
            ></v-progress-linear>
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
                  <v-toolbar-title class="font-weight-medium">Carregar Arquivo</v-toolbar-title>
                </v-toolbar>
              </v-col>
            </v-row>
            <v-row dense>
              <v-col cols="12">
                <v-card ref="form">
                  <v-card-text>
                    <br/>
                    <br/>
                    <template>
                      <input type="file" id="file" ref="file" accept=".txt" v-on:change="onChangeFileUpload()"/>
                    </template>

                      <v-row justify='end'>
                      <v-col class="text-right">
                        <v-btn color="primary" class="mr-0" @click="submitForm">Carregar</v-btn>
                      </v-col>
                    </v-row>
                  </v-card-text>
                </v-card>
              </v-col>
            </v-row>
            </v-container>
          </v-card>
        </v-container>
      </sidebar-layout-vue>
</template>

<script>
import api from '@/services/api.ts'
import SidebarLayoutVue from '@/layouts/SidebarLayout.vue'

export default {
  name: 'HomePage',
  data () {
    return {
      msg: null,
      file: '',
      showProgress: false,
    }
  },
  components: {
    SidebarLayoutVue
  },
  methods: {
    async submitForm () {
      let upload = false;
      let formData = new FormData();
      formData.append('files', this.file)
      this.showProgress = true;
      await api.post('/Files',
          formData,
          {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          }
        ).then(function (data) {
          //console.log(data.data)
          upload = true;
        })
        .catch(function () {
          console.log('Falha ao Carregar o Arquivo!')
          this.showProgress = false;
        })
        if (upload == true){
            this.showProgress = false;
            this.toReports();
        }
    },
    retornaRota () {
      this.$router.go(-1)
    },
    toReports () {
      this.$router.push("/reports");
    },
    onChangeFileUpload () {
      this.file = this.$refs.file.files[0]
    }
  }
}
</script>

<style scoped>

</style>
