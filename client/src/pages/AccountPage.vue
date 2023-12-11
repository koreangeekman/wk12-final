<template>
  <div class="container-fluid" v-if="activeVault">
    <section class="row justify-content-center pt-5">
      <div class="col-12 col-md-9 px-1 px-md-5">
        <div class="cover-image">
          <img :src="activeVault.coverImg ?? defaultImg" :alt="activeVault.name" class="cover-image rounded shadow">
        </div>
        <div class="position-relative d-flex justify-content-center">
          <div class="position-absolute d-block text-center user-profile">
            <img :src="activeVault.picture" :alt="activeVault.name" class="user-image">
            <p class="fs-1 mb-0"> {{ activeVault.name }} </p>
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
            <VaultContainer />
          </div>
        </section>
        <hr>
      </div>
      <div class="col-12 col-md-8">
        <p class="fs-1 fw-bold">Keeps</p>
        <section class="row">
          <div class="col-12 p-2">
            <KeepContainer />
          </div>
        </section>
        <hr>
      </div>
    </section>
  </div>
  <span>
    <ModalComponent :modalId="'editAccount'" :modalSize="'modal-xl'" :showHeader="false">
      <template #modalBody>
      </template>
    </ModalComponent>
  </span>
</template>

<script>
import Pop from "../utils/Pop.js";
import { Modal } from "bootstrap";
import { useRoute } from "vue-router";
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { accountService } from "../services/AccountService.js";
import KeepContainer from "../components/KeepContainer.vue";
import VaultContainer from "../components/VaultContainer.vue";
import ModalComponent from "../components/ModalComponent.vue";

export default {
  setup() {
    const route = useRoute();
    async function _setProfile() {
      try {
        if (route.name == 'Profile') {
          await accountService.setProfile(route.params.profileId);
        }
        else if (route.name == 'Account') {
          await accountService.setProfile(AppState.account.id);
        }
      }
      catch (error) { Pop.error(error); }
    }
    onMounted(() => { _setProfile(); })
    return {
      route,
      defaultImg: 'https://images.unsplash.com/photo-1663947718652-fa32fb546da2?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw0MHx8fGVufDB8fHx8fA%3D%3D',
      activeVault: computed(() => AppState.activeVault),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      editAccount() {
        Modal.getOrCreateInstance('#editAccount').show();
      }
    };
  },
  components: { VaultContainer, KeepContainer, ModalComponent }
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
