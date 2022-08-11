<template>
    <v-container class="fill-height" fluid>
            <v-row justify="center">
              <v-col cols="12" sm="8" md="4">
                <v-card class="ml-12 mr-12" ref="form">
                  <v-card-text>
                    <template>
                      <v-file-input
                        label="File input"
                        outlined
                        dense
                        accept=".txt"
                      ></v-file-input>
                    </template>

                      <v-row justify='end'>
                      <v-col class="text-right">
                        <v-btn color="primary" class="mr-0">Carregar</v-btn>
                      </v-col>
                    </v-row>
                  </v-card-text>
                </v-card>
              </v-col>
            </v-row>
        </v-container>
</template>

<script>

export default{
  name:"Home",
  data(){
    return{
      msg: null
    }
  },
  components: {
  },
  methods:{
  }
}
</script>

<style scoped>
  
</style>