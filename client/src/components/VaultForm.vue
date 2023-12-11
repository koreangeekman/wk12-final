<template>
  <form @submit.prevent="createVault()" class="d-flex flex-column p-4">
    <div class="mb-3">
      <input v-model="formData.name" type="text" id="name" class="form-control" placeholder="Title..." required>
    </div>
    <div class="mb-3">
      <input v-model="formData.img" type="url" id="img" class="form-control" placeholder="Image URL..." required>
    </div>
    <div class="mb-3">
      <textarea v-model="formData.description" id="description" class="form-control" placeholder="Description..." required
        rows="5"></textarea>
    </div>
    <span class="d-flex justify-content-between align-items-center">
      <div class="form-check ms-3 my-2">
        <input v-model="formData.isPrivate" class="form-check-input" type="checkbox" for="isPrivate">
        <label class="form-check-label" name="isPrivate" id="isPrivate">Make Vault Private?</label>
        <p class="mb-0 tiny">Private Vaults can only be seen by you</p>
      </div>
      <button class="btn btn-secondary my-3 me-3 align-self-end" type="submit" title="Create the Vault"
        aria-label="Create the Vault">Create</button>
    </span>
  </form>
</template>


<script>
import Pop from "../utils/Pop.js";
import { computed, ref } from 'vue';
import { AppState } from '../AppState.js';
import { vaultsService } from "../services/VaultsService.js";
import { Modal } from "bootstrap";

export default {
  setup() {
    const formData = ref({ isPrivate: false });

    return {
      formData,
      account: computed(() => AppState.account),
      tags: computed(() => AppState.tags),

      async createVault() {
        try {
          await vaultsService.createVault(formData.value);
          Modal.getInstance('#createVault').hide();
          formData.value = { isPrivate: false };
        }
        catch (error) { Pop.error(error); }
      }
    }
  }
};
</script>


<style lang="scss" scoped>
.tiny {
  font-size: x-small;
  color: var(--color-6);
  opacity: 70%;
}
</style>