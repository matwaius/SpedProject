<template>
    <div id="users-table">
        <Message :msg="msg" v-show="msg"/>
        <div>
            <div id="users-table-heading">
                <div class="order-id">#:</div>
                <div>Cód Usuário:</div>
                <div>Login:</div>
                <div>Email:</div>
            </div>
        </div>
        <div id="users-table-rows">
            <div class="users-table-row" v-for="user in users" :key="user.id">
                <div class="order-number">{{ user.id}}</div>
                <div>{{ user.login }}</div> 
                <div>{{ user.email }}</div> 
            </div>
        </div>
        <div>
            <button class="insert-btn" @click="insertUser()">Cadastrar</button>
        </div>
    </div>
</template>

<script>

import api from '@/services/api.ts';
import Message from './Message.vue';

export default {
    name:"DashboardUsers",
    data(){
        return{
            users: null,
            status: [],
            msg: null
        }
    },
    components:{
        Message
    },
    methods:{
        async getUsers(){
            api.get("/Users")
            .then((response) => {
                    console.log(response.data)
                    this.users =  response.data;
                })
                .catch((error) => {
                    console.log(error.response);
                });

        }
    },
    mounted() {
        this.getUsers();   
    }
}
</script>

<style scoped>

</style>