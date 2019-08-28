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
    <h1>Public Keeps</h1>
    <!-- #region --Keep Cards-- -->
    <div class="d-flex">
      <div class="row">
        <div v-for="keep in keeps" :key="keep.Id" class="card bg-light m-4" style="width: 25vw;">
          <div class="card-body">
            <h4 class="card-title">{{keep.name.toUpperCase()}}</h4>
            <p>{{keep.description}}</p>

            <button class="btn btn-sm btn-danger m-2" @click="deleteKeep(keep.id)">Delete</button>
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
    }
  },
  methods: {
    addKeep() {
      this.$store.dispatch("addKeep", this.newKeep);
      this.newKeep = { name: "", description: "", userId: this.userId };
    },
    deleteKeep(delId) {
      this.$store.dispatch("deleteKeep", delId);
    }
  },
  components: {}
};
</script>


<style scoped>
</style>