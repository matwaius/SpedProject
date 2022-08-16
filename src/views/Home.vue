<template>
    <v-container class="fill-height" fluid>
            <v-row justify="center">
              <v-col cols="12" sm="8" md="4">
                <v-card class="ml-12 mr-12" ref="form">
                  <v-card-text>
                    <template>
                      <input type="file" id="file" ref="file" v-on:change="onChangeFileUpload()"/>
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
</template>

<script>
import api from '@/services/api.ts';
export default{
  name:"Home",
  data(){
    return{
      msg: null,
      file:''
    }
  },
  components: {
  },
  methods:{
    submitForm(){
      console.log(this.file);
            let formData = new FormData();
            formData.append('file', this.file);
  
            api.post("/File",
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