<template>
  <div class="px-0 px-xl-5">
    <p class="mb-0 mt-3 py-2 fs-3 text-center app-font">ALL ACCESSIBLE VAULTS</p>
    <section class="masonry px-1 px-xl-5">
      <div class="w-100 py-3" v-for="vault in vaults" :key="vault.id">
        <VaultCard :vault="vault" />
      </div>
    </section>
  </div>
</template>


<script>
import Pop from "../utils/Pop.js";
import { useRoute } from "vue-router";
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";
import VaultCard from "./VaultCard.vue";

export default {
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
    return {
      vaults: computed(() => AppState.vaults),

    }
  },
  components: { VaultCard }
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