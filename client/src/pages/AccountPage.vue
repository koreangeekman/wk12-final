<template>
  <div class="container-fluid" v-if="activeProfile != null">
    <section class="row justify-content-center pt-5">
      <div class="col-12 col-md-9 px-1 px-md-5">
        <div class="cover-image">
          <img :src="activeProfile.coverImg ?? defaultImg" :alt="activeProfile.name" class="cover-image rounded shadow">
        </div>
        <div class="position-relative d-flex justify-content-center">
          <div class="position-absolute d-block text-center user-profile">
            <img :src="activeProfile.picture" :alt="activeProfile.name" class="user-image">
            <p class="fs-1 mb-0"> {{ activeProfile.name }} </p>
            <p class="mb-0"> {{ }} </p>
          </div>
          <div class="position-absolute user-edit" v-if="route.name == 'Account'">
            <i class="fs-1 mdi mdi-dots-horizontal btn selectable py-0 my-2" @click="editAccount()"></i>
          </div>
        </div>
      </div>
    </section>
    <section class="row justify-content-center mt-5">
      <div class="col-12 col-md-8 mt-5">
        <hr>
        <p class="fs-1 fw-bold">Vaults</p>
        <section class="row">
          <div class="col-12 p-2">
            <VaultContainer :profileId="activeProfile.id" />
          </div>
        </section>
        <hr>
      </div>
      <div class="col-12 col-md-8">
        <p class="fs-1 fw-bold">Keeps</p>
        <section class="row">
          <div class="col-12 p-2">
            <KeepContainer :profileId="activeProfile.id" />
          </div>
        </section>
        <hr>
      </div>
    </section>
  </div>
</template>

<script>
import Pop from "../utils/Pop.js";
import { Modal } from "bootstrap";
import { useRoute } from "vue-router";
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import KeepContainer from "../components/KeepContainer.vue";
import VaultContainer from "../components/VaultContainer.vue";
import { accountService } from "../services/AccountService.js";

export default {
  setup() {
    const route = useRoute();
    async function _setProfile() {
      try {
        if (route.name == 'Account') {
          await accountService.setProfile(AppState.account.id);
        }
        else if (route.name == 'Profile') {
          await accountService.setProfile(route.params.profileId);
        }
      }
      catch (error) { Pop.error(error); }
    }
    onMounted(() => {
      _setProfile();
    })
    return {
      route,
      defaultImg: 'https://images.unsplash.com/photo-1663947718652-fa32fb546da2?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw0MHx8fGVufDB8fHx8fA%3D%3D',
      activeProfile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      editAccount() {
        Modal.getOrCreateInstance('#editAccount').show();
      }
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

.user-profile {
  bottom: -5rem;
}

.user-edit {
  right: .25rem;
}

.user-image {
  height: 5rem;
  width: 5rem;
  aspect-ratio: 1/1;
  border-radius: 50%;
  border: 1px solid white;
  box-shadow: 0 .125rem .75rem .25rem var(--color-6);
}

.cover-image {
  width: 100%;
  max-height: 25rem;
  object-fit: cover;
  object-position: center;
}
</style>
