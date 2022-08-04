<template>
    <div class="page">
        <aside>
            <nav id="menu">
              <div class="cadastrar-usuarios">
                  <a href="#">Cadastrar Usuários</a>
              </div>
              <div class="relatorios" >
                  <a href="#">Relatórios</a>
              </div>
              <div class="logout">
                  <router-link to="/login">Logout</router-link>
              </div>
            </nav>
        </aside>
        <main>
            <section>
                <label>Selecione o Arquivo</label>
                <input class="input-file" type="file" accept=".txt">
                <button>Alterar</button>
            </section>
            <section>
              <DashboardUsers></DashboardUsers>
            </section>
        </main>
    </div>
    <Footer></Footer>
</template>

<script>
import Footer from '../components/Footer.vue';
import Message from '../components/Message.vue';
import DashboardUsers from '../components/DashboardUsers.vue';

export default{
  name:"Home",
  data(){
    return{
      msg: null
    }
  },
  components: {
    Footer,
    Message,
    DashboardUsers
  },
  methods:{
    get(){
      Message
    }
  }
}
</script>

<style scoped>

  .page{
    min-height: 92vh;
    display: flex;
  }

  aside {
    display: flex;
    border: 2px solid green;
    width: 250px;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }


  main{
    display: flex;
    flex:1;
    flex-direction: column;
  }

  main section:nth-child(1){
    border: 2px solid red;
    flex: 0.5;
    display: flex;
    justify-content: center;
    align-items: center;
  }

  main section:nth-child(2){
    border: 2px solid rgb(61, 5, 126);
    flex: 5;
  }
  
</style>