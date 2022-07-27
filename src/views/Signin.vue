<template>
      <div class="container">    
        <div class="content">
          <Message :msg="msg" v-show="msg"/> 
          <div id="header">
            <div class="formheadertext">
                <label>Login</label>
            </div>
          </div>
          <div class="form">
                <form id="login-form">
                  <div class="input-container">
                    <input class="form-control" name="user_login" placeholder="Usuário" type="text" autofocus="" required="" v-model="formData.user_login">
                  </div>
                  <div class="input-container">
                    <input class="form-control" name="password" placeholder="Senha" type="password" required="" v-model="formData.password" @click="getUser()">
                  </div>
                  <div class="input-container">
                    <input class="submit-btn" type="submit" value="Entrar"  >
                  </div>
                  <div class="register">
                    <router-link to="/register">Cadastrar</router-link>
                  </div>
                </form> 
          </div>	
        </div>
        <Footer></Footer>
      </div>  
</template>

<script>
import Footer from '../components/Footer.vue';
import Message from '@/components/Message.vue';
import api from '@/services/api.ts';
export default{
  name:"Signin",
  data(){
    return{
      msg: null,
      formData:{
        user_login: '',
        password: '',
        email:''
      },
      users: [],
      errors: []
    }
  },
  components: {
    Footer,
    Message
  },
  methods:{
      getUser(){
        console.log(this.formData)
      api.post("/Signin",this.formData)
      .then((response) => {
            console.log(response.data)
        })
        .catch((error) => {
            console.log(error.response);
        });

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
  
  input, select{
        padding: 5px 10px;
        width: 100%;
  }
  .submit-btn{
    background: linear-gradient(to bottom, #0099DA 5%, #0087C1 100%);
    background-color: #0099DA;
    border: 1px solid #0087C1;
    color: #fff;
    max-width: 100px;
    margin: 0;
    }

  .register a{
    display: block;
    color:black;
    font-size: 12px;
    text-align: right;
    text-decoration: none;

  }
  .register a:hover{
    color:#0087C1;
    transition:1s ease;
  }
</style>