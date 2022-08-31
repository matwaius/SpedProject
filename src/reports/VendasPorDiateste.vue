<template>
  <v-card class="mx-auto text-center" color="green" dark height="300px" width="300px">
  <v-card-text>
    <v-sheet color="rgba(0, 0, 0, .12)">
      <v-sparkline
        :value="value"
        :labels="labels"
        color="rgba(255, 255, 255, .7)"
        height="100"
        padding="24"
        stroke-linecap="round"
        smooth>
        <template v-slot:label="item">
          {{ item.value }}
        </template>
      </v-sparkline>
    </v-sheet>
  </v-card-text>
  <v-card-text>
    <div class="text-h4 font-weight-thin">
      <bold>Vendas por dia</bold>
    </div>
  </v-card-text>
  <v-divider></v-divider>
  </v-card>
</template>

<script>
export default {
  name: 'VendasPorDiaTesteVue',
  data: () => ({
    value: [
      150423,
      180460,
      90675,
      55510,
      136590,
      127610,
      25760
    ],
    labels: [
      '01',
      '02',
      '03',
      '04',
      '05',
      '06',
      '07',
      '08',
      '09',
      '10'
    ]
  })
}
</script>
