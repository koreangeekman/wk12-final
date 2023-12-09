<template>
  <nav class="navbar navbar-expand-sm px-3">
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto align-items-center">
        <li class="nav-item">
          <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
            <button class="btn selectable px-3 py-1 mx-1 bg-primary" type="button">Keeps</button>
          </router-link>
        </li>
        <li class="nav-item">
          <router-link class="navbar-brand d-flex" :to="{ name: 'Vaults' }">
            <button class="btn selectable px-3 py-1 mx-1 bg-primary" type="button">Vaults</button>
          </router-link>
        </li>
        <li class="nav-item dropdown">
          <button class="nav-link dropdown-toggle btn selectable px-3 py-1" data-bs-toggle="dropdown" role="button"
            aria-haspopup="true" aria-expanded="false">Create <small>▼</small></button>
          <div class="dropdown-menu">
            <button class="dropdown-item" type="button" @click="openCreateKeep()">Create Keep</button>
            <div class="dropdown-divider"></div>
            <button class="dropdown-item" type="button" @click="openCreateVault()">Create Vault</button>
          </div>
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <div>
        <button class="btn" @click="toggleTheme">
          <i class="mdi" :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i>
        </button>
      </div>
      <Login />
    </div>
  </nav>
</template>

<script>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
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
      }
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

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}
</style>
