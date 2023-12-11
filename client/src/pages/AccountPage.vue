<template>
  <div class="container-fluid" v-if="activeProfile">
    <section class="row justify-content-center pt-5">
      <div class="col-12 col-md-9 px-1 px-md-5">
        <div class="cover-image">
          <img :src="activeProfile.coverImg ?? defaultImg" :alt="activeProfile.name" class="cover-image rounded shadow">
        </div>
        <div class="position-relative d-flex justify-content-center">
          <div class="position-absolute d-block text-center user-profile">
            <span class="hidden">
              <p class="pt-3" v-if="activeProfile.bio">{{ activeProfile.bio }}</p>
              <div class="fs-1 socials d-flex justify-content-center p-3 mb-3">
                <a :href="activeProfile.github" v-if="activeProfile.github" target="_blank">
                  <i class="p-3 mdi mdi-github" title="icon for github"></i></a>
                <a :href="activeProfile.linkedin" v-if="activeProfile.linkedin" target="_blank">
                  <i class="p-3 mdi mdi-linkedin" title="icon for linked"></i></a>
                <a :href="activeProfile.website" v-if="activeProfile.website" target="_blank">
                  <i class="p-3 mdi mdi-web" title="icon for the user's website"></i></a>
              </div>
            </span>
            <img :src="activeProfile.picture" :alt="activeProfile.name" class="user-image">
            <p class="fs-1 mb-0"> {{ activeProfile.name }} </p>
            <span class="d-flex justify-content-center w-100">
              <p class="mb-0">{{ vaults.length }} Vault{{ vaults.length > 1 ? 's' : '' }}</p>
              <p class="mb-0 mx-2"> | </p>
              <p class="mb-0">{{ keeps.length }} Keep{{ keeps.length > 1 ? 's' : '' }}</p>
            </span>
          </div>
          <div class="position-absolute d-block text-center user-profile">
          </div>
          <div class="position-absolute user-edit" v-if="route.name == 'Account' || account.id == activeProfile.id">
            <i class="fs-1 mdi mdi-dots-horizontal btn selectable py-0 my-2" @click="editAccount()"></i>
          </div>
        </div>
      </div>
    </section>
    <section class="row justify-content-center mt-5">
      <div class="col-12 col-md-8 mt-5">
        <hr class="pb-3">
        <p v-if="account.id == activeProfile.id" class="fs-1 fw-bold app-font">My Vaults</p>
        <p v-else class="fs-1 fw-bold app-font">{{ activeProfile.name.split('@')[0] }}'s Vaults</p>
        <section class="row">
          <div class="col-12 p-2">
            <VaultContainer />
          </div>
        </section>
        <hr>
      </div>
      <div class="col-12 col-md-8 pt-3">
        <p v-if="account.id == activeProfile.id" class="fs-1 fw-bold app-font">My Keeps</p>
        <p v-else class="fs-1 fw-bold app-font">{{ activeProfile.name.split('@')[0] }}'s Keeps</p>
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
    <ModalComponent :modalId="'editAccount'" :modalSize="'modal-lg'" :showHeader="false">
      <template #modalBody>
        <AccountForm />
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
import AccountForm from "../components/AccountForm.vue";

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
      account: computed(() => AppState.account),
      activeProfile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      editAccount() {
        Modal.getOrCreateInstance('#editAccount').show();
      }
    };
  },
  components: { VaultContainer, KeepContainer, ModalComponent, AccountForm }
}
</script>

<style scoped>
img {
  object-fit: cover;
  object-position: center;
}

.user-profile {
  bottom: -6.4rem;
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

.hidden {
  opacity: 0;
  transition: .25s;
  text-shadow: 0 0 1rem var(--color-1);
}

.hidden:hover {
  opacity: 1;
}
</style>
