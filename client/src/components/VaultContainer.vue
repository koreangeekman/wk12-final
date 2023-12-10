<template>
  <div class="container-fluid px-0 px-lg-5">
    <section class="row justify-content-center px-1 px-lg-5 py-0 py-lg-4">
      <div class="col-6 col-lg-4 col-xxl-3 py-3" v-for="vault in vaults" :key="vault.id">
        <VaultCard :vault="vault" />
      </div>
    </section>
  </div>
</template>


<script>
import Pop from "../utils/Pop.js";
import { AppState } from '../AppState.js';
import { computed, onMounted } from 'vue';
import { vaultsService } from "../services/VaultsService.js";
import { Profile } from "../models/Profile.js";
import VaultCard from "./VaultCard.vue";
import { Account } from "../models/Account.js";

export default {
  props: {
    account: { type: Account, default: null },
    profile: { type: Profile, default: null },
  },
  setup(props) {
    async function _getVaults() {
      try { vaultsService.getVaults(); }
      catch (error) { Pop.error(error); }
    }
    async function _getVaultsByProfile(option) {
      try { vaultsService.getVaults(option); }
      catch (error) { Pop.error(error); }
    }
    async function _getVaultsByAccount(option) {
      try { vaultsService.getVaults(option); }
      catch (error) { Pop.error(error); }
    }
    onMounted(() => {
      if (props.profile) { _getVaultsByProfile(); }
      else if (props.account) { _getVaultsByAccount(); }
      else { _getVaults(); }
    })
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),

    }
  },
  components: { VaultCard }
};
</script>


<style lang="scss" scoped></style>