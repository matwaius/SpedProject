<template>
  <FormList title="Cadastro de Usuários">
  </FormList>
</template>

<script>
import FormList from "@/components/FormList.vue";

export default {
  name:"users",
  components: {
    FormList
  },
  data() {
    return {
      tableheader: [],
      tableitems:[]
    }
  },
  created(){
    
  }
}
</script>
