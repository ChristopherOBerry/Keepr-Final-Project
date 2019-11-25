<template>
  <div class="home">
    <h1 class="bg-secondary">Vault user: {{user.username}}</h1>
    <button class="btn btn-primary" v-if="user.id" @click="logout">logout</button>
    <router-link class="btn-primary btn" v-else :to="{name: 'login'}">Login</router-link>
    <form @submit.prevent="addVault">
      <h4 class="mt-2">Create a Vault:</h4>
      <input
        class="m-2"
        type="text"
        placeholder="Enter a Vault name"
        v-model="newVault.name"
        required
      />
      <input
        class="m-2"
        type="text"
        placeholder="Enter a vault description"
        style="width: 40vw;"
        v-model="newVault.description"
      />
      <button class="btn btn-primary m-2" type="submit">Create Vault</button>
    </form>
    <div>
      <h1 class="bg-secondary">My Vaults</h1>
      <!-- #region --Vault Cards-- -->
      <div class="d-flex justify-items-center">
        <div class="row">
          <div
            v-for="vault in vaults"
            :key="vault.Id"
            class="card bg-light m-4"
            style="width: 25vw;"
          >
            <div class="card-body">
              <router-link :to="{name: 'vault', params: {vaultId: vault.id}}">
                <h4 class="card-title">{{vault.name.toUpperCase()}}</h4>
              </router-link>

              <p>{{vault.description}}</p>
              <p>{{vault.id}}</p>
              <div>
                <p>Eventually, Keeps will go here.</p>
                <p>The keeps work now.</p>
                <button class="btn btn-sm btn-danger m-2" @click="deleteVault(vault.id)">Delete</button>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- #endregion -->
    </div>

    <keeps></keeps>
  </div>
</template>

<script>
import keeps from "../components/Keeps.vue";
export default {
  name: "home",
  data() {
    return {
      newVault: {
        name: "",
        description: "",
        userId: this.userId
      }
    };
  },
  mounted() {
    this.$store.dispatch("getVaults");
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    vaults() {
      return this.$store.state.vaults;
    },
    keeps() {
      return this.$store.state.keeps;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    addVault() {
      this.$store.dispatch("addVault", this.newVault);
      this.newVault = { name: "", description: "", userId: this.userId };
    },
    deleteVault(delId) {
      this.$store.dispatch("deleteVault", delId);
    }
  },
  components: {
    keeps
  }
};
</script>