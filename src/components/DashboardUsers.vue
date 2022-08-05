<template>
    <div id="users-table">
        <Message :msg="msg" v-show="msg"/>
        <div>
            <div id="users-table-heading">
                <div class="order-id">#:</div>
                <div>Login:</div>
                <div>Email:</div>
            </div>
        </div>
        <div id="users-table-rows">
            <div class="users-table-row" v-for="user in users" :key="user.id_user">
                <div class="order-number">{{ user.id_user}}</div>
                <div>{{ user.user_login }}</div> 
                <div>{{ user.email }}</div> 
                <div>
                    <button class="update-btn" @click="updateUser()">Alterar</button>
                </div>
                <div>
                    <button class="delete-btn" @click="deleteUser()">Delete</button>
                </div>
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

        },
        async updateUser(){
        },

        async deleteUser(){
        }
    },
    mounted() {
        this.getUsers();   
    }
}
</script>

<style scoped>

</style>