<template>
  <div class="vault">
    <h1 class="bg-secondary">VAULT VIEW</h1>
    <h1>Vault Name: {{vault.name}}</h1>
    <h3>Description: {{vault.description}}</h3>
    <h1 class="bg-secondary">Keeps in this Vault:</h1>
    <div class="m-3" v-for="vaultKeep in vaultKeeps" :key="vaultKeep.id">
      <h4>{{vaultKeep.name}}</h4>
      <p>{{vaultKeep.description}}</p>
      <button class="btn btn-primary">Remove from this vault</button>
    </div>
  </div>
</template>


<script>
export default {
  name: "vault",
  props: ["vaultId", "vaultKeep"],
  data() {
    return {};
  },
  mounted() {
    this.$store.dispatch("getActiveVault", this.vaultId);
    this.$store.dispatch("getVaultKeeps", this.vaultId);
  },
  computed: {
    vault() {
      return this.$store.state.activeVault;
    },
    vaultKeeps() {
      return this.$store.state.vaultKeeps;
    }
  },
  methods: {},
  components: {}
};
</script>


<style scoped>
</style>