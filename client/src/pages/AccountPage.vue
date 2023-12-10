<template>
  <div class="container-fluid">
    <section class="row justify-content-center pt-5">
      <div class="col-12 col-md-8 px-1 px-md-5">
        <img :src="account.coverImg" :alt="account.name" class="cover-image rounded shadow">
        <div class="position-relative d-flex justify-content-center">
          <div class="position-absolute d-block text-center">
            <img :src="account.picture" :alt="account.name" class="user-image shadow">
            <p class="fs-1 mb-0"> {{ account.name }} </p>
            <p class="mb-0"> {{ }} </p>
          </div>
        </div>
      </div>
    </section>
    <section class="row justify-content-center">
      <div class="col-12 col-md-8">
        <p class="fs-1 fw-bold">Vaults</p>
        <section class="row">
          <div class="col-6 col-md-4 p-2">
            <VaultContainer :profileId="account.id" />
          </div>
        </section>
      </div>
      <div class="col-12 col-md-8">
        <p class="fs-1 fw-bold">Keeps</p>
        <section class="row">
          <div class="col-6 col-md-4 p-2">
            <KeepContainer :profileId="account.id" />
          </div>
        </section>
      </div>
    </section>
  </div>
</template>

<script>
import { AppState } from '../AppState.js';
import { computed } from 'vue';
import KeepContainer from "../components/KeepContainer.vue";
import VaultContainer from "../components/VaultContainer.vue";

export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { VaultContainer, KeepContainer }
}
</script>

<style scoped>
img {
  object-fit: cover;
  object-position: center;
}

.user-image {
  height: 5rem;
  width: 5rem;
  aspect-ratio: 1/1;
  border-radius: 50%;
}

.cover-image {
  width: 100%;
  height: 20rem;
}
</style>
