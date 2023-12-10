<template>
  <div class="vault-card p-2 ps-3 d-flex flex-column justify-content-between">
    <span><i v-if="vault.isPrivate" class="fs-1 text-dark mdi mdi-lock"></i></span>
    <span class="d-flex align-items-center h-100 w-100">
      <p class="fs-4 mb-0 me-2 p-2 rounded app-font light-shadow">{{ vault.name.toUpperCase() }}</p>
      <img :src="vault.creator.picture" :alt="vault.creator.name" :title="vault.creator.name" class="creator-img ms-auto">
    </span>
  </div>
</template>


<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
import { Vault } from "../models/Vault.js";

export default {
  props: { vault: { type: Vault, required: true } },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      vaultImg: computed(() => `url('${props.vault.img}')`),

    }
  }
};
</script>


<style lang="scss" scoped>
p {
  color: var(--color-1);
}

.mdi-lock {
  text-shadow: 0 0 1rem var(--color-1);
}

.vault-card {
  min-height: 15rem;
  background-color: var(--color-3);
  background-image: v-bind(vaultImg);
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  border-radius: .5rem;
  box-shadow: 0 .25rem .5rem var(--color-5);
  transition: .25s;
}

.vault-card:hover {
  background-size: 105%;
}

.light-shadow {
  background-color: #80808080;
  backdrop-filter: blur(2px);
}

.creator-img {
  border-radius: 50%;
  aspect-ratio: 1/1;
  height: 3rem;
  transition: .25s;
}

.creator-img:hover {
  transform: scale(108%);
  box-shadow: 0 0 .25rem .25rem var(--color-4);
}
</style>