<template>
  <div class="container-fluid px-0 px-lg-5">
    <section class="row justify-content-center px-1 px-lg-5">
      <div class="col-6 col-lg-4 col-xxl-3 py-3" v-for="keep in keeps" :key="keep.id">
        <KeepCard :keep="keep" />
      </div>
    </section>
  </div>
</template>


<script>
import Pop from "../utils/Pop.js";
import { AppState } from '../AppState.js';
import { computed, onMounted } from 'vue';
import { keepsService } from "../services/KeepsService.js";
import KeepCard from "./KeepCard.vue";

export default {
  props: {
    vaultId: { type: Number, default: null },
    profileId: { type: String, default: null },
  },
  setup(props) {
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
    onMounted(() => {
      if (props.profileId) { _getKeepsByProfileId(props.profileId); }
      else if (props.vaultId) { _getKeepsByVaultId(props.vaultId); }
      else { _getKeeps(); }
    })
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),

    }
  },
  components: { KeepCard }
};
</script>


<style lang="scss" scoped></style>