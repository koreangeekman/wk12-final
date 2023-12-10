<template>
  <div class="container-fluid px-0 px-lg-5">
    <section class="row justify-content-center px-1 px-lg-5">
      <div class="col-6 col-lg-4 col-xxl-3 py-3" v-for="vault in vaults" :key="vault.id">
        <VaultCard :vault="vault" />
      </div>
    </section>
  </div>
</template>


<script>
import Pop from "../utils/Pop.js";
import { useRoute } from "vue-router";
import { computed, onMounted, watch } from 'vue';
import { AppState } from '../AppState.js';
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";
import VaultCard from "./VaultCard.vue";

export default {
  props: {
    profileId: { type: String, default: null },
  },
  setup() {
    const route = useRoute();
    async function _getVaults() {
      try { await vaultsService.getVaults(); }
      catch (error) { Pop.error(error); }
    }
    async function _getVaultsByProfileId(profileId) {
      try {
        logger.log('profileId', profileId);
        await vaultsService.getVaultsByProfileId(profileId);
      }
      catch (error) { Pop.error(error); }
    }
    function _routeGetVaults() {
      if (route.name == 'Profile') { _getVaultsByProfileId(route.params.profileId); }
      else if (route.name == 'Account') { _getVaultsByProfileId(AppState.account.id); }
      else { _getVaults(); }
    }
    onMounted(() => { _routeGetVaults(); })
    watch(route.name, _routeGetVaults)
    return {
      vaults: computed(() => AppState.vaults),

    }
  },
  components: { VaultCard }
};
</script>


<style lang="scss" scoped></style>