<template>
  <span class="navbar-text">
    <button class="btn selectable lighten-30 text-uppercase my-2 my-lg-0" @click="login" v-if="!user.isAuthenticated"
      type="button" title="Log-in" aria-label="Log-in">
      Login
    </button>
    <div v-else>
      <div class="dropdown d-none d-md-block">
        <div type="button" class="border-0 selectable rounded-circle no-select p-1" data-bs-toggle="dropdown"
          title="Account menu" aria-label="Account menu">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" title="Account photo"
              aria-label="Account Photo" height="64" class="rounded-circle" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-sm-end dropdown-menu-start p-0" aria-labelledby="authDropdown">
          <div class="list-group">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </router-link>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
      <div class="dropup d-block d-md-none my-2 my-lg-0">
        <div type="button" class="border-0 selectable rounded-circle no-select p-1" data-bs-toggle="dropdown"
          title="Account menu" aria-label="Account menu">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" title="Account photo"
              aria-label="Account Photo" height="64" class="rounded-circle" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-sm-end dropdown-menu-start p-0" aria-labelledby="authDropdown">
          <div class="list-group">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </router-link>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
img {
  background-color: var(--color-1);
}
</style>
