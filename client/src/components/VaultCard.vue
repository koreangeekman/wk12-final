<template>
  <div class="position-relative">
    <div class="vault-card selectable" @click="openVault()">
      <img class="vault-img rounded" :src="vault.img" :alt="vault.name">
      <span class="vault-info d-flex align-items-center w-100 p-2 position-absolute">
        <p class="fs-4 mb-0 me-2 p-2 rounded app-font light-shadow">{{ vault.name.toUpperCase() }}</p>
        <img :src="vault.creator?.picture" :alt="vault.creator?.name" :title="vault.creator?.name"
          class="creator-img ms-auto" type="button" v-if="route.name == 'Vaults'"
          @click.stop="openProfile(vault.creatorId)">
      </span>
    </div>
    <span class="isPrivate position-absolute">
      <i v-if="vault.isPrivate" class="fs-1 text-dark mdi mdi-lock" title="Private Vault"></i>
    </span>
    <DeleteItem :vault="vault" :itemType="'vault'" />
  </div>
</template>


<script>
import { useRoute, useRouter } from "vue-router";
import { Vault } from "../models/Vault.js";
import DeleteItem from "./DeleteItem.vue";

export default {
  props: { vault: { type: Vault, required: true } },
  setup(props) {
    const route = useRoute();
    const router = useRouter();
    return {
      route,
      openVault() { router.push({ name: 'VaultDetails', params: { vaultId: props.vault.id } }); },
      openProfile(profileId) { router.push({ name: 'Profile', params: { profileId } }); },
    };
  },
  components: { DeleteItem }
};
</script>


<style lang="scss" scoped>
p {
  color: var(--color-1);
}

.isPrivate {
  top: .5rem;
  left: 1rem;
}

.mdi-lock {
  text-shadow: 0 0 1rem var(--color-1);
}

.vault-card {
  min-height: 6rem;
  width: 100%;
  border-radius: .5rem;
  box-shadow: 0 .25rem .5rem var(--color-5);
  background-color: var(--color-3);
  overflow: hidden;
}

.vault-img {
  object-position: center;
  object-fit: cover;
  width: 100%;
  transition: .25s;
}

.vault-img:hover {
  transform: scale(1.1);
}

.vault-info {
  left: 0;
  bottom: 0;
}


.light-shadow {
  background-color: #80808080;
  backdrop-filter: blur(2px);
}

.creator-img {
  border-radius: 3rem;
  height: 3rem;
  width: 3rem;
  transition: .25s;
}

.creator-img:hover {
  transform: scale(108%);
  box-shadow: 0 0 .25rem .25rem var(--color-4);
}
</style>