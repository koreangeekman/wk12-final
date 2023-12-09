<template>
  <form @submit.prevent class="d-flex flex-column">
    <div class="mb-3">
      <input v-model="formData.name" type="text" id="name" class="form-control" placeholder="Title..." required>
    </div>
    <div class="mb-3">
      <input v-model="formData.img" type="url" id="img" class="form-control" placeholder="Image URL..." required>
    </div>
    <span class="d-flex justify-content-between align-items-center">
      <div class="form-check ms-3 my-2">
        <input v-model="formData.isPrivate" class="form-check-input" type="checkbox" for="isPrivate">
        <label class="form-check-label" name="isPrivate" id="isPrivate">Make Vault Private?</label>
        <p class="mb-0 tiny">Private Vaults can only be seen by you</p>
      </div>
      <button class="btn btn-secondary my-3 me-3 align-self-end" type="submit">Create</button>
    </span>
  </form>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref } from 'vue';

export default {
  setup() {
    const formData = ref({});

    return {
      formData,
      account: computed(() => AppState.account),
      tags: computed(() => AppState.tags),
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