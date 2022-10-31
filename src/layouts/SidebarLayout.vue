<template>
  <v-app id="side-bar" name="side-bar">
    <v-navigation-drawer class="menu"  v-model="drawer"
      app>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title class="text-h6">
              SPED
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
            <v-list-item-icon class="icon">
              <v-icon color="#fff">{{ item.icon }}</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
    </v-navigation-drawer>

    <v-app-bar app class="menu">
        <v-app-bar-nav-icon color="#fff" @click="drawer = !drawer"></v-app-bar-nav-icon>

        <v-toolbar-title>SPED</v-toolbar-title>
        <v-col class="text-right">
          <div class="my-2">
            <v-icon left color="error" @click="Disconnect">
              mdi-power
            </v-icon>
          </div>
        </v-col>
    </v-app-bar>

    <v-main></v-main>
    <slot></slot>
  </v-app>
</template>

<script>
export default {
  name: 'side-bar',
  data: () => ({
    drawer: false,
    items: [
      { title: 'Carregar Arquivo', icon: 'mdi-archive', to: '/home' },
      { title: 'Relatórios', icon: 'mdi-monitor-dashboard', to: '/reports' },
      { title: 'Usuários', icon: 'mdi-account-box-multiple', to: '/Users' },
      { title: 'Sobre', icon: 'mdi-information-outline', to: '/sobre' }
    ]
  }),
  methods: {
    Disconnect () {
      localStorage.removeItem("user");
      this.$router.push('/login');
    }
  }
}
</script>

<style scoped>
  #side-bar{
    background: #eeeeee;
  }
  .menu {
    background: #1e293b !important;
  }
  .menu, .v-list-item__content{
    color: #fff
  }
</style>