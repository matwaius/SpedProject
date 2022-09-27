<template>
<v-container class="fill-height" fluid>
  <v-row class="title">
    <h1 class="title-h1">Projeto</h1>
  </v-row>
  <v-row justify="center" >
    <v-col cols="12" sm="8" md="4" class="left-login">
      <v-img
        src ="../assets/Metrics.gif">
      </v-img>
    </v-col>
    <v-col cols="12" sm="8" md="4" align-self="center" class="right-login" >
      <h1 class = "h1-login">Faça o Login e <br/> entre para o nosso time</h1>
      <v-card class="ml-12 mr-12" ref="form">
        <v-card-text>
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
            :value="formData.Login = (formData.Login == null) ? formData.Login : formData.Login.toUpperCase()"
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
          :value="formData.Password = (formData.Password == null) ? formData.Password : formData.Password.toUpperCase()"
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
/* import api from '@/services/api.ts' */
export default {
  name: 'SignIn',

  data: () => ({
    valid: true,
    formData: {
      Login: '',
      Password: ''
    },
    show1: false,
    password: 'Password',
    loginRules: [
      v => v.length >= 4 || 'Min 4 characters',
      v => !!v || 'Login is required',
      v => (v && v.length <= 10) || 'Login must be less than 10 characters'
    ],
    rules: {
      required: value => !!value || 'Required.',
      min: v => v.length >= 4 || 'Min 4 characters'
    }
  }),
  methods: {
    cleanForm () {
      this.formData.Login = ''
      this.formData.Password = ''
    },
    async getUser () {
      this.$router.push('/home')
    },
    SignUp () {
      this.$router.push('/registrar')
    }
  }
}
</script>

<style scoped>

.left-login {
  margin-right: 20%;
}

.fill-height {
  background-color: #87CEFA;
  color:black
}

.h1-login {
  margin-left: 10%;
  margin-bottom: 10%;
}

.title {
  margin-left: 15%;
  margin-top: 5%;
}
.title-h1{
  font-size: 50px;
}
</style>
