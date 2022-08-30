<template>
    <v-container class="fill-height" fluid>
        <v-row justify="center">
          <v-col cols="12" sm="8" md="4">
            <Message :msg="msg" v-show="msg"/>
            <v-card class="ml-12 mr-12" ref="form">
              <v-card-text>

                <h1 class="bold">Registrar</h1>
                <br/>

                <v-form
                    ref="form"
                    v-model="valid"
                    @submit.prevent="createUser()"
                    autocomplete="off"
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
                      @click:append="show1 = !show1"
                      hint="At least 8 characters"
                      label="Senha"
                      required
                    ></v-text-field>

                    <v-text-field
                      v-model="formData.ConfirmPassword"
                      outlined
                      :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
                      :type="show2 ? 'text' : 'password'"
                      :rules="[rules.required, rules.min]"
                      hint="At least 8 characters"
                      @click:append="show2 = !show2"
                      label="Repetir Senha"
                      required
                    ></v-text-field>

                    <v-text-field
                      v-model="formData.Email"
                      outlined
                      :rules="emailRules"
                      label="E-mail"
                      required
                    ></v-text-field>
                    <v-row justify='end'>
                      <v-col class="text-right">
                        <v-btn
                            color="primary"
                            class="mr-3"
                            @click="createUser">
                            Cadastrar</v-btn>
                        <v-btn
                            color="error"
                            class="mr-0"
                            @click="cleanForm">
                            Limpar</v-btn>
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
import Message from '@/components/Message.vue';
export default {

  name: 'Register',

  data: () => ({
    valid: true,
    msg: "",
    formData: {
      Login: '',
      Password: '',
      ConfirmPassword: '',
      Email: ''
    },
    show1: false,
    show2: false,
    password: 'Password',
    confirmPassword: 'ConfirmPassword',
    rules: {
      required: value => !!value || 'Required.',
      min: v => v.length >= 4 || 'Min 4 characters'
    },
    loginRules: [
      v => v.length >= 4 || 'Min 4 characters',
      v => !!v || 'Login is required',
      v => (v && v.length <= 10) || 'Login must be less than 10 characters'
    ],
    emailRules: [
      v => !!v || 'E-mail is required',
      v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
    ]
  }),
  components: {
    Message
  },
  methods: {
    cleanForm () {
      this.formData.Login = "";
      this.formData.Password = "";
      this.formData.ConfirmPassword = "";
      this.formData.Email = "";
    },
    async createUser () {
      api.post("/Users",this.formData)
        .then((response) => {
          this.msg = response.data
        })
        .catch((error) => {
          console.log(error.response)
        })

      setTimeout(() => this.msg="", 3000);
      // quando criado, rotear para /login
    }
  }
}
</script>

<style scoped>

bold {
  font-weight: bold;
}
</style>
