<template>
  <div class="keep-card p-2 d-flex align-items-end">
    {{ keep }}
    <span class="d-flex align-items-center justify-content-between">
      <p class="fs-3">{{ keep.name }}</p>
      <img :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name" class="creator-img">
    </span>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed } from 'vue';
import { Keep } from "../models/Keep.js";

export default {
  props: { keep: { type: Keep, required: true } },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      keepImg: computed(() => `url('${props.keep.img}')`),

    }
  }
};
</script>


<style lang="scss" scoped>
.keep-card {
  min-height: 5rem;
  background-color: var(--color-3);
  background-image: v-bind(keepImg);
  background-position: center;
  background-size: cover;
  border-radius: .5rem;
  box-shadow: 0 .25rem .5rem var(--color-5);
}

.creator-img {
  border-radius: 50%;
  aspect-ratio: 1/1;
  height: 2rem;
}
</style>