<template>
  <FormUpdate 
      title="Inserir Usuário">
      <template slot="body">
         <!--CONTEUDO-->
        <v-row>
          <v-col>
            <v-form ref="formUsuarios"
                    @submit.prevent="onSave"
                    autocomplete="off">
              <v-container>
                <v-row>
                  <v-col cols="4"
                        style="display: inline-block">
                    <v-text-field label="Login do Usuários"
                                  v-model="field_login"
                                  counter="10"
                                  max-length="10"
                                  autofocus>

                    </v-text-field>
                  </v-col>
                </v-row>  
              </v-container>
            </v-form>
          </v-col>
        </v-row>
      </template>
  </FormUpdate>
</template>

<script>
import FormUpdate from "@/components/FormUpdate.vue";
import api from '@/services/api.ts';

export default {
  name:"users",
  components: {
    FormUpdate
  },
  data() {
    return {
    }
  },
  mounted(){
  },
  methods:{
    }
}
</script>
