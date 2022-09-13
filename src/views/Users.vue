<template>
  <FormList 
      title="Cadastro de Usuários"
      routerInsert="usuarios-registrar"
      :tableHeader="tableheader"
      :tableItems="tableitems"
      >
  </FormList>
</template>

<script>
import FormList from "@/components/FormList.vue";
import Register from "./Register.vue";
import api from '@/services/api.ts';

export default {
  name:"users",
  components: {
    FormList,
    Register
  },
  data() {
    return {
      tableheader: [
        { text: "Cód.", value: "Id", align:"start", divider:false, width: "15%", sortable: true },
        { text: "Login", value: "Login",align:"start", divider:false, width: "20%", sortable: true },
        { text: "Email", value: "Email",align:"start", divider:false, width: "50%", sortable: true },

        { text: "", value: "actions",align:"end", divider:false, width: "15%", sortable: true },
      ],
      tableitems:[]
    }
  },
  mounted(){
    this.GetUser();
  },
  methods:{
    async GetUser () {
        await api.get("/Users")
          .then((response) => {
            this.tableitems = response.data;
          })
          .catch((error) => {
            console.log(error.response)
          });
      }
    }
}
</script>
