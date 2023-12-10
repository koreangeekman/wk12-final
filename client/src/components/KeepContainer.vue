<template>
  <div class="container-fluid px-0 px-lg-5">
    <section class="row justify-content-center px-1 px-lg-5">
      <div class="col-6 col-lg-4 col-xxl-3 py-3" v-for="keep in keeps" :key="keep.id">
        <KeepCard :keep="keep" />
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
      keeps: computed(() => AppState.keeps),

    }
  },
  components: { KeepCard, ModalComponent, KeepDetails }
};
</script>


<style lang="scss" scoped></style>