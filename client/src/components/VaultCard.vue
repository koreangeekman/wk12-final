<template>
  <div class="keep-card px-3 py-2 d-flex align-items-end">
    <span class="d-flex align-items-center justify-content-between h-100 w-100">
      <p class="fs-3 mb-0 app-font">{{ keep.name }}</p>
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
}
</style>