<template>
  <v-app id="inspire">
    <v-navigation-drawer  v-model="drawer" 
      app>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title class="text-h6">
              Menu
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-divider></v-divider>

        <v-list
          dense
          nav
        >
          <v-list-item
            v-for="item in items"
            :key="item.title"
            :to="item.to"
            link
          >
            <v-list-item-icon>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
    </v-navigation-drawer>

    <v-app-bar app>
        <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

        <v-toolbar-title>SPED</v-toolbar-title>
    </v-app-bar>

    <v-main></v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
  import api from '@/services/api.ts';
  export default {
    data: () => ({ 
      drawer: false, 
      items: [
          { title: 'Login', icon: 'mdi-view-dashboard', to:'/Login' },
          { title: 'Cadastrar', icon: 'mdi-view-dashboard', to:'/Registrar' },
          { title: 'Sobre', icon: 'mdi-help-box', to: '/' },
        ]
      }),
      mounted(){
        this.$router.push('/Login')
      }
        
  }
</script>

<style>
  .main{
    height: 100vh;
    width: 100%;
    max-height: calc(100%-64px);
    overflow: hidden;
    overflow-y: auto;
    top: 64px;
    padding-top: 0px !important;
  }
</style>
