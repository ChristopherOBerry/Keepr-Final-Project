<template>
  <div class="keeps">
    <form @submit.prevent="addKeep">
      <h4 class="mt-2">Create a Keep:</h4>
      <input
        class="m-2"
        type="text"
        placeholder="Enter a Keep name"
        v-model="newKeep.name"
        required
      />
      <input
        class="m-2"
        type="text"
        placeholder="Enter a Keep description"
        style="width: 40vw;"
        v-model="newKeep.description"
      />
      <button class="btn btn-primary m-2" type="submit">Create Keep</button>
    </form>
    <h1 class="bg-secondary">My Keeps</h1>
    <div class="d-flex">
      <div class="row">
        <div class="card-body" v-for="keep in keeps" :key="keep.Id">
          <div v-if="keep.userId == user.id">
            <h4 class="card-title">{{keep.name.toUpperCase()}}</h4>
            <p>{{keep.description}}</p>
            <p>Views: {{keep.views}}| Shares: {{keep.shares}}| Keeps: {{keep.keeps}}</p>
            <button
              v-if="keep.userId == user.id"
              class="btn btn-sm btn-danger m-2"
              @click="deleteKeep(keep.id)"
            >Delete</button>
            <div class="dropdown">
              <button
                style="display: inline-block;"
                class="btn btn-primary dropdown-toggle"
                type="button"
                data-toggle="dropdown"
              >Add to vault:</button>
              <div class="dropdown-menu">
                <p
                  class="dropdown-item"
                  :key="vault.Id"
                  v-for="vault in vaults"
                  @click="addKeepToVault(vault.id,keep.id,user.id)"
                >
                  Move To:
                  {{vault.name}} {{vault.id}}
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <h1 class="bg-secondary">Public Keeps</h1>
    <!-- #region --Keep Cards-- -->
    <div class="d-flex">
      <div class="row">
        <div v-for="keep in keeps" :key="keep.Id" class="card bg-light m-4" style="width: 25vw;">
          <div class="card-body">
            <h4 class="card-title">{{keep.name.toUpperCase()}}</h4>
            <p>{{keep.description}}</p>
            <p>Views: {{keep.views}}| Shares: {{keep.shares}}| Keeps: {{keep.keeps}}</p>
            <button
              v-if="keep.userId == user.id"
              class="btn btn-sm btn-danger m-2"
              @click="deleteKeep(keep.id)"
            >Delete</button>
            <div class="dropdown">
              <button
                style="display: inline-block;"
                class="btn btn-primary dropdown-toggle"
                type="button"
                data-toggle="dropdown"
              >Add to vault:</button>
              <div class="dropdown-menu">
                <p
                  class="dropdown-item"
                  :key="vault.Id"
                  v-for="vault in vaults"
                  @click="addKeepToVault(vault.id,keep.id,user.id)"
                >
                  Move To:
                  {{vault.name}} {{vault.id}}
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- #endregion -->
  </div>
</template>


<script>
export default {
  name: "keeps",
  mounted() {
    this.$store.dispatch("getKeeps");
    this.$store.dispatch("getVaults");
  },
  data() {
    return {
      newKeep: {
        name: "",
        description: "",
        userId: this.userId
      }
    };
  },
  computed: {
    keeps() {
      return this.$store.state.keeps;
    },
    user() {
      return this.$store.state.user;
    },
    vaults() {
      return this.$store.state.vaults;
    }
  },
  methods: {
    addKeep() {
      this.$store.dispatch("addKeep", this.newKeep);
      this.newKeep = { name: "", description: "", userId: this.userId };
    },
    deleteKeep(delId) {
      this.$store.dispatch("deleteKeep", delId);
    },
    addKeepToVault(vaultid, keepid, userid) {
      let payload = {
        keepId: keepid,
        vaultId: vaultid,
        userId: userid
      };
      this.$store.dispatch("addKeepToVault", payload);
    }
  },
  components: {}
};
</script>


<style scoped>
</style>