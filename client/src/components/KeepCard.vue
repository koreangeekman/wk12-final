<template>
  <div class="keep-card p-2 px-3 d-flex align-items-end" @click="openKeepDetails()">
    <span class="d-flex align-items-center justify-content-between h-100 w-100">
      <p class="fs-5 mb-0 app-font light-shadow">{{ keep.name }}</p>
      <img :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name" type="button"
        class="creator-img selectable rounded-circle" @click.stop="openProfile(keep.creatorId)">
    </span>
  </div>
</template>


<script>
import { Modal } from "bootstrap";
import { computed } from 'vue';
import { useRouter } from "vue-router";
import { AppState } from '../AppState.js';
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";

export default {
  props: { keep: { type: Keep, required: true } },
  setup(props) {
    const router = useRouter();
    return {
      account: computed(() => AppState.account),
      keepImg: computed(() => `url('${props.keep?.img}')`),

      openKeepDetails() {
        keepsService.setActiveKeep(props.keep);
        Modal.getOrCreateInstance('#keepDetail').show();
      },

      openProfile(profileId) {
        router.push({ name: 'Profile', params: { profileId } })
      }

    }
  }
};
</script>


<style lang="scss" scoped>
p {
  color: var(--color-1);
}

.keep-card {
  min-height: 15rem;
  background-color: var(--color-3);
  background-image: v-bind(keepImg);
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  border-radius: .5rem;
  box-shadow: 0 .25rem .5rem var(--color-5);
  transition: .25s;
}

.keep-card:hover {
  background-size: 105%;
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