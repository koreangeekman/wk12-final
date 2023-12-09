<template>
  <div class="container-fluid">
    <section class="row justify-content-center px-1 px-md-5 py-0 py-md-4">
      <div class="col-6 col-md-4 " v-for="keep in keeps" :key="keep.id">
        <KeepCard :keep="keep" />
      </div>
    </section>
  </div>
</template>


<script>
import Pop from "../utils/Pop.js";
import { computed } from 'vue';
import { AppState } from '../AppState';
import { Vault } from "../models/Vault.js";
import KeepCard from "../components/KeepCard.vue";
import { Profile } from "../models/Profile.js";

export default {
  props: {
    vault: { type: Vault, default: null },
    profile: { type: Profile, default: null },
  },
  setup(props) {
    async function _getKeeps(options) {
      try { keepsService.getKeeps(options); }
      catch (error) { Pop.error(error); }
    }
    async function _getKeepsByProfile(options) {
      try { keepsService.getKeeps(options); }
      catch (error) { Pop.error(error); }
    }
    async function _getKeepsByAccount(options) {
      try { keepsService.getKeeps(options); }
      catch (error) { Pop.error(error); }
    }
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),

    }
  },
  components: { KeepCard }
};
</script>


<style lang="scss" scoped></style>