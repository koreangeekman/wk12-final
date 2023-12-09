<template>
  <div class="keep-card p-5">

  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed } from 'vue';

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
  background-color: var(--color-3);
  background-image: v-bind(keepImg);
  border-radius: .5rem;
  box-shadow: 0 .25rem .5rem var(--color-5);
}
</style>