<template>
  <sidebar-layout-vue>
    <v-container class="fill-height" fluid>
            <v-row justify="center">
              <v-col cols="12" sm="8" md="4">
                <v-card class="ml-12 mr-12" ref="form">
                  <v-card-text>
                    <h1 class="bold">Carregar Arquivo</h1>
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
  </sidebar-layout-vue>
</template>

<script>
import api from '@/services/api.ts';
import SidebarLayoutVue from '@/layouts/SidebarLayout.vue';

export default{
  name:"Home",
  data(){
    return{
      msg: null,
      file:''
    }
  },
  components: {
    SidebarLayoutVue,
    Footer
  },
  methods:{
    submitForm(){
      console.log(this.file);
            let formData = new FormData();
            formData.append('file', this.file);

            api.post("/Files",
                formData,
                {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
              }
            ).then(function(data){
              console.log(data.data);
            })
            .catch(function(){
              console.log('Falha ao Carregar o Arquivo!');
            });
      },

      onChangeFileUpload(){
        this.file = this.$refs.file.files[0];
      }
  }
}
</script>

<style scoped>

</style>
