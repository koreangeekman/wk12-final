<template>
  <div class="position-relative">
    <div class="keep-card selectable" @click="openKeepDetails()">
      <img class="keep-img rounded" :src="keep.img" :alt="keep.name">
      <span class="keep-info d-flex align-items-center justify-content-between w-100 p-2 position-absolute">
        <p class="fs-5 mb-0 ms-1 p-1 rounded app-font light-shadow">{{ keep.name }}</p>
        <img :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name" type="button"
          class="creator-img selectable rounded-circle" v-if="route.name == 'Keeps'"
          @click.stop="openProfile(keep.creatorId)">
      </span>
    </div>
    <DeleteItem :keep="keep" :itemType="'keep'" />
  </div>
</template>


<script>
import Pop from "../utils/Pop.js";
import { Modal } from "bootstrap";
import { useRoute, useRouter } from "vue-router";
import { keepsService } from "../services/KeepsService.js";
import { Keep } from "../models/Keep.js";
import DeleteItem from "./DeleteItem.vue";

export default {
  props: { keep: { type: Keep, required: true } },
  setup(props) {
    const route = useRoute();
    const router = useRouter();
    async function _getKeepById(keepId) {
      try { await keepsService.getKeepById(keepId); }
      catch (error) { Pop.error(error); }
    }
    return {
      route,
      async openKeepDetails() {
        try {
          // vaultKeepService.setVault();
          keepsService.setActiveKeep(props.keep);
          Modal.getOrCreateInstance('#keepDetail').show();
          await _getKeepById(props.keep.id);
        }
        catch (error) { Pop.error(error); }
      },
      openProfile(profileId) {
        router.push({ name: 'Profile', params: { profileId } });
      },
    };
  },
  components: { DeleteItem }
};
</script>


<style lang="scss" scoped>
p {
  color: var(--color-1);
}

.keep-card {
  width: 100%;
  border-radius: .5rem;
  box-shadow: 0 .25rem .5rem var(--color-5);
  background-color: var(--color-3);
  overflow: hidden;
}

.keep-img {
  object-position: center;
  object-fit: cover;
  width: 100%;
  transition: .25s;
}

.keep-img:hover {
  transform: scale(1.1);
}

.keep-info {
  left: 0;
  bottom: 0;
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