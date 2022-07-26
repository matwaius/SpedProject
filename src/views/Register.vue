<template>
      <div class="container">
        <div class="content"> 
          <Message :msg="msg" v-show="msg"/> 
          <div id="header">
            <div class="formheadertext">
                <label>Cadastrar</label>
            </div>
          </div>
          <div class="form">
                <form id="login-form">
                  <div class="input-container">
                    <input class="form-control" name="usuario" placeholder="Usuário" type="text" autofocus="" required="" v-model="formData.Login">
                  </div>
                  <div class="input-container">
                    <input class="form-control" name="senha" placeholder="Senha" type="password" required="" v-model="formData.Senha">
                  </div>
                  <div class="input-container">
                    <input class="form-control" name="email" placeholder="email@email.com" type="text" required="" v-model="formData.Email">
                  </div>
                  <div class="input-container">
                    <input @click="createUser()" class="submit-btn" type="submit" value="Criar">
                  </div>
                </form> 
          </div>	
        </div> 
        <Footer></Footer>
      </div>   
</template>

<script>
import Footer from '../components/Footer.vue';
import Message from '../components/Message.vue';
import axios from 'axios';
export default{
  name:"Register",
  data(){
    return{
      msg: null,
      formData:{
        Login: '',
        Senha: '',
        Email: ''
      },
    }
  },
  components: {
    Footer,
    Message
  },
  methods:{
    createUser(){
      console.log(this.formData)
        axios.post("https://localhost:7258/api/Users",this.formData)
        .then(() => {
            console.log('Usuário Cadastrado com Sucesso')
        })
        .catch((error) => {
            console.log(error.response);
        });
      

        //COLOCAR UMA MENSAGEM DE SISTEMA
        this.msg = `Usuário Cadastrado com Sucesso!`;
        setTimeout(() => this.msg="", 3000);
        this.formData.Login = "";
        this.formData.Senha ="";
        this.formData.Email="";

        event.preventDefault();
    }
  }
}
</script>

<style scoped>

  #header{
    align-items: center;
    justify-content: left;
    width: 100%;
    height: 1%;
  }
  #header label{
    background: linear-gradient(to bottom, #0099DA 5%, #0087C1 100%);
    background-color: #0099DA;
    border: 1px solid #0087C1;
    color: #fff;
    padding:1.5%;
  }
  #header .formheadertext{
    max-width: 400px;
    margin-bottom: 5px;
  }
  #login-form{
      max-width: 400px;
      margin: 0 auto;
      margin-bottom: 413px;
  }
  .container{
    width: 100vw;
    min-height: 100vh;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    margin: 0;
    margin-right: 0;
    padding: 0;

  }

  .content{
    display: flex;
    flex-direction: column;
    flex: 1;
    align-items: center;
    justify-content: center;
  }

    @media (min-width: 1400px){
    .container, .container-lg, .container-md, .container-sm, .container-xl, .container-xxl {
        max-width: 100% !important;
    }
  }

  @media (min-width: 576px){
    .container, .container-lg, .container-md, .container-sm, .container-xl, .container-xxl {
        max-width: 100% !important;
    }
  }
  
  .input-container{
        display:flex;
        justify-content: flex-end;
        align-items: flex-end;
        margin-bottom: 10px;
    }

  .form .input-container input[type="text"]{
    background: #FFF url(../../public/img/img_login_usuario.png) no-repeat left;
    padding: 5px 12px 5px 49px;
    position: relative;
  }
  .form .input-container input[type="password"]{
    background: #FFF url(../../public/img/img_login_senha.png) no-repeat left;
    padding: 5px 12px 5px 49px;
    position: relative;
  }
 
  .input-container .submit-btn{ 
    margin-right: 1px;
    }
  input, select{
        padding: 5px 10px;
        width: 370px;
  }
  .submit-btn{
    background: linear-gradient(to bottom, #0099DA 5%, #0087C1 100%);
    background-color: #0099DA;
    border: 1px solid #0087C1;
    color: #fff;
    margin-left: auto;
    max-width: 100px;
    }
</style>