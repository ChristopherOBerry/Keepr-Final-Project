<template>
  <div class="vault">
    <h1>VAULT VIEW</h1>
    <h1>{{vault.name}}</h1>
    <h3>{{vault.description}}</h3>
  </div>
</template>


<script>
export default {
  name: "vault",
  props: ["vaultId"],
  data() {
    return {};
  },
  mounted() {
    this.$store.dispatch("getActiveVault", this.vaultId);
  },
  computed: {
    vault() {
      return this.$store.state.activeVault;
    }
  },
  methods: {},
  components: {}
};
</script>


<style scoped>
</style>