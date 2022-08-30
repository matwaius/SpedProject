<template>
<v-container class="fill-height" fluid>
        <v-row justify="center">
          <v-col cols="12" sm="8" md="4">
            <v-card class="ml-12 mr-12" ref="form">
              <v-card-text>

                <h1 class="bold">Login</h1>
                <br/>

                <v-form
                    ref="form"
                    v-model="valid"
                    lazy-validation
                  >

                    <v-text-field
                      v-model="formData.Login"
                      outlined
                      :rules="loginRules"
                      label="Login"
                      required
                    ></v-text-field>

                    <v-text-field
                      v-model="formData.Password"
                      outlined
                      :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                      :type="show1 ? 'text' : 'password'"
                      :rules="[rules.required, rules.min]"
                      hint="At least 8 characters"
                      @click:append="show1 = !show1"
                      label="Senha"
                      required
                    ></v-text-field>

                      <v-row justify='end'>

                        <v-col class="text-left">
                        <v-btn color="primary" class="mr-3" @click="SignUp">Registrar</v-btn>
                      </v-col>

                      <v-col class="text-right">
                        <v-btn color="primary" class="mr-3" @click="getUser">Entrar</v-btn>
                        <v-btn color="error" class="mr-0" @click="cleanForm">Limpar</v-btn>
                      </v-col>
                    </v-row>

                </v-form>
              </v-card-text>
            </v-card>
          </v-col>
        </v-row>
    </v-container>
</template>

<script>
import api from '@/services/api.ts';
export default {
  name: 'Signin',

  data: () => ({
      valid: true,
      formData:{
        Login: '',
        Password: ''
      },
        show1: false,
        password: 'Password',
        loginRules: [
          v => v.length >= 4 || 'Min 4 characters',
          v => !!v || 'Login is required',
          v => (v && v.length <= 10) || 'Login must be less than 10 characters',
        ],
        rules: {
          required: value => !!value || 'Required.',
          min: v => v.length >= 4 || 'Min 4 characters',
        },
  }),
  methods:{
    cleanForm() {
        this.formData.Login="";
        this.formData.Password="";
      },
      async getUser(){
        this.$router.push('/home');
        /*console.log(this.formData)
        api.post("/Signin",this.formData)
        .then((response) => {
              console.log(response.data)
              this.$router.push('/home');
          })
          .catch((error) => {
              console.log(error.response);
          });

          event.preventDefault();*/
    },
    SignUp(){
      this.$router.push('/registrar')
    }
  }
}
</script>

<style scoped>

bold {
  font-weight: bold;
}
</style>
