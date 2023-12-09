<template>
  <div class="container-fluid">
    <section class="row justify-content-center px-1 px-md-5 py-0 py-md-4">
      <div class="col-6 col-md-4 col-lg-3 py-3" v-for="keep in keeps" :key="keep.id">
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
import { Profile } from "../models/Profile.js";
import { Vault } from "../models/Vault.js";
import KeepCard from "./KeepCard.vue";

export default {
  props: {
    vault: { type: Vault, default: null },
    profile: { type: Profile, default: null },
  },
  setup(props) {
    async function _getKeeps() {
      try { keepsService.getKeeps(); }
      catch (error) { Pop.error(error); }
    }
    async function _getKeepsByProfile(option) {
      try { keepsService.getKeeps(option); }
      catch (error) { Pop.error(error); }
    }
    async function _getKeepsByAccount(option) {
      try { keepsService.getKeeps(option); }
      catch (error) { Pop.error(error); }
    }
    onMounted(() => {
      if (props.profile) { _getKeepsByProfile(); }
      else if (props.vault) { _getKeepsByAccount(); }
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