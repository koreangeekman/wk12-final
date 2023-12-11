<template>
  <div class="px-0 px-xl-5">
    <p class="mb-0 mt-3 py-2 fs-3 text-center app-font"></p>
    <span v-if="route.name == 'VaultDetails' || route.name == 'Vaults'" class="">
      <p v-if="route.name == 'VaultDetails'" class="mb-0 mt-3 py-2 fs-3 text-center app-font">VAULT KEEPS</p>
      <p v-else class="mb-0 mt-3 py-2 fs-3 text-center app-font">ALL KEEPS</p>
      <p v-if="keeps.length > 0" class="mb-0fs-6 text-center app-font">
        {{ keeps.length }} Keep{{ keeps.length > 1 ? 's' : '' }}
      </p>
    </span>
    <section v-if="keeps.length > 0" class="masonry px-1 px-lg-5">
      <div class="w-100 py-3" v-for="keep in keeps" :key="keep.id">
        <KeepCard :keep="keep" />
      </div>
    </section>
    <section v-else-if="route.name == 'VaultDetails'" class="py-5 px-1 px-lg-5 text-center">
      <p class="fs-5 mb-5">(No Keeps yet)</p>
      <router-link :to="{ name: 'Keeps' }">
        <button class="btn btn-primary fs-4">Add Keeps</button>
      </router-link>
    </section>
    <section v-else class="">
      <div class="d-flex justify-content-center py-5">
        <p class="fs-1"> Loading </p>
        <i class="mdi mdi-loading mdi-spin fs-1"></i>
      </div>
    </section>
  </div>

  <span>
    <ModalComponent :modalId="'keepDetail'" :modalSize="'modal-xl'" :showHeader="false">
      <template #modalBody>
        <KeepDetails />
      </template>
    </ModalComponent>
  </span>
</template>


<script>
import Pop from "../utils/Pop.js";
import { useRoute } from "vue-router";
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { keepsService } from "../services/KeepsService.js";
import ModalComponent from "../components/ModalComponent.vue";
import KeepDetails from "../components/KeepDetails.vue";
import KeepCard from "./KeepCard.vue";

export default {
  setup() {
    const route = useRoute();
    async function _getKeeps() {
      try { await keepsService.getKeeps(); }
      catch (error) { Pop.error(error); }
    }
    async function _getKeepsByVaultId(vaultId) {
      try { await keepsService.getKeepsByVaultId(vaultId); }
      catch (error) { Pop.error(error); }
    }
    async function _getKeepsByProfileId(profileId) {
      try { await keepsService.getKeepsByProfileId(profileId); }
      catch (error) { Pop.error(error); }
    }
    function _routeGetKeeps() {
      if (route.name == 'Profile') { _getKeepsByProfileId(route.params.profileId); }
      else if (route.name == 'Account') { _getKeepsByProfileId(AppState.account.id); }
      else if (route.name == 'VaultDetails') { _getKeepsByVaultId(route.params.vaultId); }
      else { _getKeeps(); }
    }
    onMounted(() => { _routeGetKeeps(); })
    return {
      route,
      keeps: computed(() => AppState.keeps),

    }
  },
  components: { KeepCard, ModalComponent, KeepDetails }
};
</script>


<style lang="scss" scoped>
.masonry {
  columns: 15rem 2;
}

@media screen and (min-width: 768px) {
  .masonry {
    columns: 18rem;
  }
}
</style>