<template>
  <div>
  <FormUpdate 
      :title="this.title"
      :width="800"
      :maxHeight="350"
      @onSave="onSave"
      >
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
                        <v-text-field
                        v-model="formData.Login"
                        :rules="loginRules"
                        label="Login"
                        :value="formData.Login= (formData.Login == null) ? formData.Login : formData.Login.toUpperCase()"
                        required
                      ></v-text-field>
                  </v-col>
                  <v-col cols="4"
                        style="display: inline-block">
                        <v-text-field
                          v-model="formData.Password"
                          :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                          :type="show1 ? 'text' : 'password'"
                          :rules="[rules.required, rules.min]"
                          @click:append="show1 = !show1"
                          hint="Min. 4 Caracteres"
                          label="Senha"
                          :value="formData.Password = (formData.Password == null) ? formData.Password : formData.Password.toUpperCase()"
                          required
                        ></v-text-field>
                  </v-col>
                  <v-col cols="4"
                        style="display: inline-block">
                        <v-text-field
                          v-model="this.ConfirmPass"
                          :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
                          :type="show2 ? 'text' : 'password'"
                          :rules="[rules.required, rules.min]"
                          @click:append="show2 = !show2"
                          hint="Min. 4 Caracteres"
                          label="Confirmar Senha"
                          :value="this.ConfirmPass  = (this.ConfirmPass  == null) ? this.ConfirmPass  : this.ConfirmPass.toUpperCase()"
                          required
                        ></v-text-field>
                  </v-col>
                </v-row> 
                <v-row>
                  <v-col cols="12"
                        style="display: inline-block">
                        <v-text-field
                      v-model="formData.Email"
                      :rules="emailRules"
                      label="E-mail"
                      :value="formData.Email = (formData.Email== null) ? formData.Email : formData.Email.toUpperCase()"
                      required
                    ></v-text-field>
                    
                  </v-col>
                  
                </v-row>
              </v-container>
            </v-form>
            
          </v-col>
          <!--<Message :msg="msg" v-show="msg"/>-->
        </v-row>
      </template>
  </FormUpdate>
  </div>
  
</template>

<script>
import FormUpdate from "@/components/FormUpdate.vue";
import api from '@/services/api.ts';
import * as validations from '@/services/validation.ts';

export default {
  name:"users",
  props: true,
  props: {
      id: { 
        type: Number
      }
  },
  components: {
    FormUpdate
  },
  data: () => ({
    title: "",
    msg: "",
    editing: false,
    show1: false,
    show2: false,
    ConfirmPass:"",
    formData: {
      Id: 0,
      Login: "",
      Password: "",
      Email: ""
    },
    loginRules: [
      v => v.length >= 4 || 'Min. 4 Caracteres',
      v => !!v || 'Login é Obrigatório'
    ],
    rules: {
      required: value => !!value || 'Senha é Obrigatória.',
      min: v => v.length >= 4 || 'Min. 4 Caracteres'
      //equal: value => value != this.formData.Password || 'Senha é Obrigatória.',
    },
    emailRules: [
      v => !!v || 'E-mail é Obrigatório.',
      v => /.+@.+\..+/.test(v) || 'E-mail Invalido',
    ]
  }),
 created(){
    this.id = this.$route.params.id;
    this.title = "Inserir Usuário";
    if(this.id > 0){
      this.title="Alterar Usuário";
      this.editing= true;
      this.formData.Id = this.id;
      this.getUser();
    }
  },
  methods:{
    cleanForm () {
      this.formData.Login = "";
      this.formData.Password = "";
      this.formData.Email = "";
    },
    validacao() {
      let retorno =true;
      if(this.formData.Login.length == 0 || 
          this.formData.Password.length == 0 || 
          this.formData.Email.length == 0) {
          retorno= false;
          this.msg = "Dados Incorretos!";
          setTimeout(() => this.msg="", 3000);
      }
      return retorno;
    },
    async onSave () {
      if(this.validacao() == true ){
        if(this.editing == true){
          await api.put(`/Users/${this.id}`,this.formData)
            .then((response) => {
              this.msg = response.data
            })
            .catch((error) => {
              console.log(error.response)
            })
        }else{
          await api.post(`/Users`,this.formData)
            .then((response) => {
              this.msg = response.data
            })
            .catch((error) => {
              console.log(error.response)
            })
        }
        console.log(this.msg);
        setTimeout(() => this.msg="", 3000);
        this.$router.go(-1);
      }
    },
    async getUser(){
      await api.get("/Users/"+this.id)
            .then((response) => {
              this.formData.Id = response.data.Id;
              this.formData.Login = response.data.Login;
              this.formData.Password = response.data.Password;
              this.ConfirmPass = response.data.Password;
              this.formData.Email = response.data.Email;
            })
            .catch((error) => {
              console.log(error.response)
            })
    }
  }
}
</script>
