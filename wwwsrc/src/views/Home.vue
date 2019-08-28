<template>
  <div class="home">
    <h1>Welcome Home {{user.username}}</h1>
    <button class="btn btn-primary" v-if="user.id" @click="logout">logout</button>
    <router-link class="btn-primary btn" v-else :to="{name: 'login'}">Login</router-link>
  </div>
</template>

<script>
export default {
  name: "home",
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  }
};
</script>