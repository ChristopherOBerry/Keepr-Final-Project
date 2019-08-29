import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'
import AuthService from './AuthService'

Vue.use(Vuex)

let baseUrl = location.host.includes('localhost') ? '//localhost:5000/' : '/'

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    vaults: [],
    activeVault: {},
    keeps: [],
    vaultKeeps: []
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    resetState(state) {
      //clear the entire state object of user data
      state.user = {}
    },
    setVaults(state, vaults) {
      state.vaults = vaults
    },
    setActiveVault(state, vault) {
      state.activeVault = vault
    },
    setKeeps(state, keeps) {
      state.keeps = keeps
    },
    setVaultKeeps(state, vaultKeeps) {
      state.vaultKeeps = vaultKeeps
    },
    resetState(state, user) {
      state.user = {}
    }
  },
  actions: {
    async register({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Register(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async login({ commit, dispatch }, creds) {
      try {
        let user = await AuthService.Login(creds)
        commit('setUser', user)
        router.push({ name: "home" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    async logout({ commit, dispatch }) {
      try {
        let success = await AuthService.Logout()
        if (!success) { }
        commit('resetState')
        router.push({ name: "login" })
      } catch (e) {
        console.warn(e.message)
      }
    },
    //#region --VAULTS--
    getVaults({ commit, dispatch }) {
      api.get('vaults')
        .then(res => {
          commit('setVaults', res.data)
        })
    },
    getActiveVault({ commit, dispatch }, payload) {
      api.get('vaults/' + payload)
        .then(res => {
          commit("setActiveVault", res.data)
        })
    },
    addVault({ commit, dispatch }, vaultData) {
      api.post('vaults', vaultData)
        .then(serverVault => {
          dispatch('getVaults')
        })
    },
    deleteVault({ commit, dispatch }, delId) {
      api.delete('vaults/' + delId)
        .then(res => {
          dispatch('getVaults')
        })
    },
    //#endregion
    getKeeps({ commit, dispatch }) {
      api.get('keeps')
        .then(res => {
          commit('setKeeps', res.data)
        })
    },
    addKeep({ commit, dispatch }, keepData) {
      api.post('keeps', keepData)
        .then(serverKeep => {
          dispatch('getKeeps')
        })
    },
    deleteKeep({ commit, dispatch }, delId) {
      api.delete('keeps/' + delId)
        .then(res => {
          dispatch('getKeeps')
        })
    },
    getVaultKeeps({ commit, dispatch }, payload) {
      api.get('vaultkeeps/' + payload)
        .then(res => {
          commit('setVaultKeeps', res.data)
        })
    },
    addKeepToVault({ commit, dispatch }, payload) {

      api.post('vaultkeeps/', payload)
        .then(res => {
          dispatch('getVaultKeeps', payload.vaultId)

        })
    }



  }
})
