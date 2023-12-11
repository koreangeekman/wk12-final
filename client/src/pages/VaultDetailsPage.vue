<template>
  <div class="container-fluid">
    <section v-if="activeVault" class="row justify-content-center">
      <div class="col-5 d-flex p-0 mt-3">
        <div class="position-relative d-flex justify-content-center w-100">
          <img :src="activeVault.img" :alt="activeVault?.name" class="cover-img rounded">
          <div class="position-absolute d-block text-center rounded p-2 m-2 fw-bold app-font vault-info">
            <p class="mb-0 fs-2 text-nowrap">{{ activeVault.name }}</p>
            <p class="mb-0 fs-5">by {{ activeVault.creator.name }}</p>
          </div>
          <div class="position-absolute vault-edit"
            v-if="route.name == 'VaultDetails' && activeVault.creatorId == account.id">
            <i class="fs-5 mdi mdi-dots-horizontal rounded selectable px-1" title="Edit Vault"></i>
          </div>
        </div>
      </div>
    </section>
    <section class="row justify-content-center">
      <div class="col-12 col-md-10">
        <KeepContainer />
      </div>
    </section>
  </div>
</template>

<script>
import Pop from "../utils/Pop.js";
import { useRoute, useRouter } from "vue-router";
import { computed, watch } from "vue";
import { AppState } from "../AppState.js";
import { vaultsService } from "../services/VaultsService.js";
import KeepContainer from "../components/KeepContainer.vue";

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    const watchVaultId = computed(() => route.params.vaultId);
    async function _getVaultById() {
      try { await vaultsService.getVaultById(route.params.vaultId); }
      catch (error) {
        if (error.response.data.includes('Id:')) { router.push({ name: 'Vaults' }); }
        Pop.error(error);
      }
    }
    watch(watchVaultId, () => {
      _getVaultById();
    }, { immediate: true });
    return {
      route,
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),
    };
  },
  components: { KeepContainer }
}
</script>

<style scoped lang="scss">
.app-font {
  line-height: 2.4rem;
}

.cover-img {
  width: 100%;
  max-height: 42dvh;
  object-fit: cover;
  object-position: center;
}

.vault-info {
  bottom: 0;
  color: white;
  text-shadow: 0 0 .5rem black;
  backdrop-filter: blur(2px);
  border: 1px solid white;
}

.vault-edit {
  bottom: -1.6rem;
  right: 0rem;
  line-height: 1rem;
}
</style>
