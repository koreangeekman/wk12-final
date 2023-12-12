<template>
  <div class="container-fluid overflow-hidden" v-if="activeKeep">
    <section class="row justify-content-center">
      <div class="col-12 col-md-6 p-0 d-flex">
        <img :src="activeKeep.img" :alt="activeKeep.name" class="keep-img">
      </div>
      <div class="col-12 col-md-6 py-4">
        <section class="row justify-content-center align-content-between h-100">
          <div class="col-12 d-flex justify-content-center counts">
            <div class="mx-2 d-flex align-items-center view-count">
              <i class="fs-5 mdi mdi-eye-outline"></i>
              <p class="mb-0 mx-2">{{ activeKeep.views }}</p>
            </div>
            <div class="mx-3 d-flex align-items-center kept-count">
              <p class="mb-0 app-font ">k</p>
              <p class="mb-0 mx-2">{{ activeKeep.kept }}</p>
            </div>
          </div>
          <div class="col-12 p-5">
            <p class="fs-1 pb-3 text-center app-font">{{ activeKeep.name }}</p>
            <p class="fs-6 description">{{ activeKeep.description }}</p>
          </div>
          <div class="col-12 d-flex justify-content-between align-items-center px-4 py-1">
            <button v-if="route.name == 'VaultDetails' && account?.id == activeVault?.creatorId"
              @click="removeFromVault()" class="btn btn-secondary" type="button">
              <i class="mdi mdi-cancel mdi-rotate-90"></i> Remove from vault
            </button>
            <form v-else-if="(account?.id && myVaults?.length > 0)" @submit.prevent="addKeepToVault()"
              class="d-flex mx-3">
              <select v-model="selectedVault.vaultId" type="text" class="form-select shadow" id="vault">
                <option v-for="vault in myVaults" :value="vault.id">
                  {{ vault.name }} {{ vault.isPrivate ? '🔒' : '' }}
                </option>
              </select>
              <button class="btn btn-success shadow text-light px-2 mx-1" type="submit" title="Add Keep to Vault"
                aria-label="Add Keep to Vault">save</button>
            </form>
            <button v-else-if="account?.id" @click="createVault()" class="btn btn-secondary" type="button"
              title="Create a vault" aria-label="Create a vault">create a vault</button>
            <span class="d-flex align-items-center" @click.stop="openProfile(activeKeep.creatorId)" type="button">
              <img :src="activeKeep.creator.picture" :alt="activeKeep.creator.name" :title="activeKeep.creator.name"
                class="creator-img shadow mx-2">
              <p class="mb-0 fw-bold d-none d-md-inline">{{ activeKeep.creator.name.split('@')[0] }}</p>
            </span>
          </div>
        </section>
      </div>
    </section>
  </div>
</template>


<script>
import Pop from "../utils/Pop";
import { Modal } from "bootstrap";
import { AppState } from '../AppState.js';
import { computed, ref } from 'vue';
import { useRoute, useRouter } from "vue-router";
import { vaultKeepService } from "../services/VaultKeepService.js";

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();

    const selectedVault = ref({});

    // watchEffect(() => {
    //   _checkVaults(AppState.myVaultKeeps)
    // })

    return {
      route,
      selectedVault,
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      activeKeep: computed(() => AppState.activeKeep),
      activeVault: computed(() => AppState.activeVault),

      createVault() {
        Modal.getOrCreateInstance('#keepDetail').hide();
        Modal.getOrCreateInstance('#createVault').show();
      },
      openProfile(profileId) {
        Modal.getOrCreateInstance('#keepDetail').hide();
        router.push({ name: 'Profile', params: { profileId } })
      },
      async addKeepToVault() {
        try {
          selectedVault.value.keepId = this.activeKeep.id;
          await vaultKeepService.createVaultKeep(selectedVault.value);
          this.activeKeep.kept++;
          Pop.success('Added keep to vault!')
        }
        catch (error) {
          if (error.response.data.includes('Duplicate')) {
            Pop.error('Already saved to this vault')
          }
          Pop.error(error);
        }
      },
      async removeFromVault() {
        try {
          const yes = await Pop.confirm('Remove this keep from the current vault?');
          if (!yes) { return }
          await vaultKeepService.deleteVaultKeep(this.activeKeep.vaultKeepId)
          Modal.getInstance('#keepDetail').hide();
        }
        catch (error) { Pop.error(error); }
      }
    }
  }
};
</script>


<style lang="scss" scoped>
.image {
  height: 50%;
}

.keep-img {
  object-fit: cover;
  object-position: center;
  border-radius: .4rem .4rem 0 0;
  max-height: 50dvh;
  width: 100%;
}

.creator-img {
  border-radius: 50%;
  aspect-ratio: 1/1;
  height: 2.2rem;
  transition: .25s;
}

.counts {
  opacity: .7;
}

.kept-count>p:nth-child(2),
.view-count>p {
  font-size: small;
}

.kept-count>p:nth-child(1) {
  border: 1px solid var(--color-5);
  border-radius: .25rem;
  padding: 0 .25rem;
  font-size: small;
}

.description {
  line-height: 1.75rem;
}

.form-select {
  min-width: 8rem;
}

@media screen and (min-width: 768px) {
  .keep-img {
    border-radius: .4rem 0 0 .4rem;
    max-height: 90dvh;
  }
}
</style>