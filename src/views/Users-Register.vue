<template>
  <div>
  <FormUpdate
      :title="this.title"
      :width="800"
      :maxHeight="maxHeight"
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
                <v-row>
                    <v-col cols="12">
                        <v-combobox
                          v-model="select"
                          item-text="value"
                          item-value="value"
                          :items="items"
                          label="Acessos Relatórios"
                          @change="onChangeSelectAcess"
                          multiple
                          chips
                        >
                      </v-combobox>
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
import FormUpdate from '@/components/FormUpdate.vue'
import api from '@/services/api.ts'
import * as validations from '@/services/validation.ts'
import md5 from "md5"

export default {
  name: 'usersPage',
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
    title: '',
    msg: '',
    editing: false,
    show1: false,
    show2: false,
    ConfirmPass: '',
    formData: {
      Id: 0,
      Login: '',
      Password: '',
      Email: '',
      Nivel:0,
      RelNF:0,
      RelNFICMS:0,
      RelNFICMSST:0,
      RelNFUF:0,
      RelNFQTD:0,
      RelNFQTDUF:0,
      RelCF:0,
      RelCFTOT:0,
    },
    maxHeight:530,
    select: [],
    items:[
        { value: 'Rel. Nota Fiscal' },
        { value: 'Rel. Nota Fiscal por ICMS' },
        { value: 'Rel. Nota Fiscal por ICMS ST' },
        { value: 'Rel. Nota Fiscal por UF' },
        { value: 'Rel. Nota Fiscal por Qtd' },
        { value: 'Rel. Nota Fiscal por Qtd/UF' },
        { value: 'Rel. Cupom Fiscais' },
        { value: 'Rel. Totalizadores Cupom Fiscal' }
    ],
    loginRules: [
      v => v.length >= 4 || 'Min. 4 Caracteres',
      v => !!v || 'Login é Obrigatório'
    ],
    rules: {
      required: value => !!value || 'Senha é Obrigatória.',
      min: v => v.length >= 4 || 'Min. 4 Caracteres'
      // equal: value => value != this.formData.Password || 'Senha é Obrigatória.',
    },
    emailRules: [
      v => !!v || 'E-mail é Obrigatório.',
      v => /.+@.+\..+/.test(v) || 'E-mail Invalido'
    ]
  }),
  created () {
    this.id = this.$route.params.id
    this.title = 'Inserir Usuário'
    this.maxHeight = 440;
    if (this.id > 0) {
      this.title = 'Alterar Usuário'
      this.editing = true
      this.formData.Id = this.id
      this.getUser()
    }
  },
  methods: {
    cleanForm () {
      this.formData.Login = ''
      this.formData.Password = ''
      this.formData.Email = ''
    },
    validacao () {
      let retorno = true
      if (this.formData.Login.length == 0 ||
          this.formData.Password.length == 0 ||
          this.formData.Email.length == 0) {
        retorno = false
        this.msg = 'Dados Incorretos!'
        setTimeout(() => this.msg = '', 3000)
      }
      return retorno
    },
    async onSave () {
      if (this.validacao() == true) {
        
        const regexExp = /^[a-f0-9]{32}$/gi;
        const str = this.formData.Password;
        if(regexExp.test(str) == false) 
        {
          this.formData.Password  = await md5(this.formData.Password);
        } 
        //const encryptedPassword = await md5(this.formData.Password);

        this.formData.RelNF = 0;
        this.formData.RelNFICMS =0;
        this.formData.RelNFICMSST = 0;
        this.formData.RelNFUF = 0;
        this.formData.RelNFQTD = 0;
        this.formData.RelNFQTDUF = 0;
        this.formData.RelCF = 0;
        this.formData.RelCFTOT = 0;
        if(this.select != null && this.select != undefined)
        {
            this.formData.RelNF = this.select.find(x=> x.value == 'Rel. Nota Fiscal') != undefined ? 1 : 0;
            this.formData.RelNFICMS = this.select.find(x=> x.value == 'Rel. Nota Fiscal por ICMS') != undefined ? 1 : 0;
            this.formData.RelNFICMSST = this.select.find(x=> x.value == 'Rel. Nota Fiscal por ICMS ST') != undefined ? 1 : 0;
            this.formData.RelNFUF = this.select.find(x=> x.value == 'Rel. Nota Fiscal por UF') != undefined ? 1 : 0;
            this.formData.RelNFQTD = this.select.find(x=> x.value == 'Rel. Nota Fiscal por Qtd') != undefined ? 1 : 0;
            this.formData.RelNFQTDUF = this.select.find(x=> x.value == 'Rel. Nota Fiscal por Qtd/UF') >= undefined ? 1 : 0;
            this.formData.RelCF = this.select.find(x=> x.value == 'Rel. Cupom Fiscais') != undefined ? 1 : 0;
            this.formData.RelCFTOT = this.select.find(x=> x.value == 'Rel. Totalizadores Cupom Fiscal') != undefined ? 1 : 0;
        }
        const dataObject = {
          ...this.formData
        };
        if (this.editing == true) {
          await api.put(`/Users/${this.id}`, dataObject)
            .then((response) => {
              this.msg = response.data
            })
            .catch((error) => {
              console.log(error.response)
            })
        } else {
          await api.post(`/Users`, dataObject)
            .then((response) => {
              this.msg = response.data
            })
            .catch((error) => {
              console.log(error.response)
            })
        }
        setTimeout(() => this.msg = '', 3000)
        this.$router.go(-1)
      }
    },
    async onChangeSelectAcess(){
      if(this.select.length == 8)
      {
        this.select = null;
        this.maxHeight = 440;
      }
      if(this.select!= null && this.select != undefined)
      {
        this.maxHeight = 440 +(this.select.length*10);
      }
    },
    async getUser () {
      let cont = 0;
      await api.get('/Users/' + this.id)
        .then((response) => {
          this.formData.Id = response.data.Id
          this.formData.Login = response.data.Login
          this.formData.Password = response.data.Password
          this.ConfirmPass = response.data.Password
          this.formData.Email = response.data.Email
          this.formData.Nivel = response.data.Nivel
          this.formData.RelNF = response.data.RelNF
          this.formData.RelNFICMS = response.data.RelNFICMS
          this.formData.RelNFICMSST = response.data.RelNFICMSST
          this.formData.RelNFUF = response.data.RelNFUF
          this.formData.RelNFQTD = response.data.RelNFQTD
          this.formData.RelNFQTDUF = response.data.RelNFQTDUF
          this.formData.RelCF = response.data.RelCF
          this.formData.RelCFTOT = response.data.RelCFTOT

          if(this.formData.Nivel == 1 || this.formData.RelNF == 1 )
          {
              this.select.push({value: 'Rel. Nota Fiscal' })
              cont+=1;
          }
          if(this.formData.Nivel == 1 || this.formData.RelNFICMS == 1 )
          {
              this.select.push({value: 'Rel. Nota Fiscal por ICMS' })
              cont+=1;
          }
          if(this.formData.Nivel == 1 || this.formData.RelNFICMSST == 1 )
          {
              this.select.push({value: 'Rel. Nota Fiscal por ICMS ST' })
              cont+=1;
          }
          if(this.formData.Nivel == 1 || this.formData.RelNFUF == 1 )
          {
              this.select.push({value: 'Rel. Nota Fiscal por UF' })
              cont+=1;
          }
          if(this.formData.Nivel == 1 || this.formData.RelNFQTD == 1 )
          {
              this.select.push({value: 'Rel. Nota Fiscal por Qtd' })
              cont+=1;
          }
          if(this.formData.Nivel == 1 || this.formData.RelNFQTDUF == 1 )
          {
              this.select.push({value: 'Rel. Nota Fiscal por Qtd/UF' })
              cont+=1;
          }
          if(this.formData.Nivel == 1 || this.formData.RelCF == 1 )
          {
              this.select.push({value: 'Rel. Cupom Fiscais' })
              cont+=1;
          }
          if(this.formData.Nivel == 1 || this.formData.RelCFTOT == 1 )
          {
              this.select.push({value: 'Rel. Totalizadores Cupom Fiscal' })
              cont+=1;
          }
        })
        .catch((error) => {
          console.log(error.response)
        })

        this.maxHeight = 440 + (cont*10);
    }
  }
}
</script>
