<template>
  <form @submit.prevent="createKeep()" class="d-flex flex-column p-4">
    <div class="mb-3">
      <input v-model="formData.name" type="text" id="name" class="form-control" placeholder="Title..." required>
    </div>
    <div class="mb-3">
      <input v-model="formData.img" type="url" id="img" class="form-control" placeholder="Image URL..." required>
    </div>
    <div class="mb-3">
      <input v-model="newTag.name" type="text" id="tags" class="form-control" placeholder="Tags...">
      <p class="tiny ms-3">* Separate tags with a comma</p>
      <span v-if="tags?.length > 0" class="d-flex flex-wrap mb-3">
        <div v-for="tag in tags" class="tag rounded-pill mx-2 my-1 px-2 py-1 bg-primary">{{ tag }}</div>
      </span>
    </div>
    <textarea v-model="formData.description" name="description" id="description" rows="5" required class="form-control"
      placeholder="Description..."></textarea>
    <button class="btn btn-secondary mt-4 align-self-end" type="submit">Create</button>
  </form>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, watch } from 'vue';
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";

export default {
  setup() {
    const formData = ref({});
    const newTag = ref({ count: 0 });

    watch(newTag, () => {
      if (newTag.value.includes(',')) {
        _createTag(newTag.value.name);
      }
    })

    async function _createTag() {
      try {
        await keepsService.createTag(formData.value);
      }
      catch (error) { Pop.error(error); }
    }

    return {
      formData,
      newTag,
      account: computed(() => AppState.account),
      tags: computed(() => AppState.tags),
      async createKeep() {
        try {
          await keepsService.createKeep(formData.value);
          Modal.getInstance('#createKeep').hide();
          formData.value = {};
        }
        catch (error) { Pop.error(error); }
      },
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

.tag {
  font-size: small;
}
</style>