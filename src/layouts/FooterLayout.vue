<template>
  <v-card>
    <v-footer
      v-bind="localAttrs"
    >
      <v-card
        flat
        tile
        width="100%"
        class="blue lighten-1 text-center"
      >
        <v-divider></v-divider>

        <v-card-text class="white--text">
          {{ new Date().getFullYear() }} — <strong>SPED</strong> — <v-btn class="ma-2" text icon color="black" href="https://github.com/matwaius/Projeto/tree/master" target="_blank"><v-icon>mdi-github</v-icon></v-btn>
        </v-card-text>
      </v-card>
    </v-footer>
  </v-card>
</template>

<script>
export default {
  data: () => ({
    variant: 'fixed'
  }),
  computed: {
    localAttrs () {
      const attrs = {}
      attrs[this.variant] = true
      return attrs
    }
  }
}
</script>

<style scoped>

.blue lighten-1 text-center{
  margin: 10 auto;
}
</style>
