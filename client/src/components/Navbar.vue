<template>
  <nav class="navbar navbar-expand-sm px-3">
    <span class="d-flex align-items-center w-100">
      <div class="collapse navbar-collapse w-100 justify-content-md-end order-2 order-md-1" id="navbarText">
        <ul class="navbar-nav align-items-center w-100">
          <li class="nav-item">
            <router-link class="navbar-brand d-flex" :to="{ name: 'Keeps' }">
              <button class="btn selectable px-3 py-1 mx-1 bg-secondary fw-bold" type="button"
                aria-label="Show Keeps">Keeps</button>
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="navbar-brand d-flex" :to="{ name: 'Vaults' }">
              <button class="btn selectable px-3 py-1 mx-1 bg-secondary fw-bold" type="button"
                aria-label="Show Vaults">Vaults</button>
            </router-link>
          </li>
          <li v-if="account.id" class="nav-item dropdown d-none d-md-block">
            <button class="nav-link dropdown-toggle btn selectable px-3 py-1 fw-bold" data-bs-toggle="dropdown"
              role="button" aria-haspopup="true" aria-expanded="false">
              Create <small>▼</small>
            </button>
            <div class="dropdown-menu">
              <button class="dropdown-item" type="button" @click="createKeep()" aria-label="Create Keep">Create
                Keep</button>
              <div class="dropdown-divider"></div>
              <button class="dropdown-item" type="button" @click="createVault()" aria-label="Create Vault">Create
                Vault</button>
            </div>
          </li>
          <li class="nav-item dropup d-block d-md-none">
            <button class="nav-link dropdown-toggle btn selectable px-3 py-1 fw-bold" data-bs-toggle="dropdown"
              role="button" aria-haspopup="true" aria-expanded="false">
              Create <small>▲</small>
            </button>
            <div class="dropdown-menu">
              <button class="dropdown-item" type="button" @click="createKeep()" aria-label="Create Keep">Create
                Keep</button>
              <div class="dropdown-divider"></div>
              <button class="dropdown-item" type="button" @click="createVault()" aria-label="Create Vault">Create
                Vault</button>
            </div>
          </li>
        </ul>
      </div>

      <div class="w-100 d-flex justify-content-md-center order-1 order-md-2">
        <router-link class="navbar-brand d-flex" :to="{ name: 'Keeps' }">
          <p class="mb-0 px-1 rounded app-name app-logo app-font">
            the <br>
            keepr <br>
            co.
          </p>
        </router-link>
      </div>
      <div class="w-100 d-flex justify-content-end align-items-center order-3">
        <button class="btn mx-3" @click="toggleTheme">
          <i class="fs-3 mdi" :class="theme == 'light' ? 'mdi-weather-sunny text-dark' : 'mdi-weather-night'"></i>
        </button>
        <Login />
      </div>
      <button class="navbar-toggler order-4" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
        aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
    </span>
  </nav>
</template>

<script>
import Login from './Login.vue';
import { Modal } from "bootstrap";
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import { AppState } from "../AppState";

export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      },
      account: computed(() => AppState.account),
      createKeep() { Modal.getOrCreateInstance('#createKeep').show(); },
      createVault() { Modal.getOrCreateInstance('#createVault').show(); },
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.app-name {
  border: 2px solid;
  width: 4.5rem;
}

.bg-secondary {
  background-color: var(--color-3) !important;
}

@media screen and (min-width: 576px) {
  nav {
    height: 96px;
  }
}
</style>
