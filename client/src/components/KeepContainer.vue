<template>
  <div class="container-fluid">
    <section class="row justify-content-center px-1 px-md-5 py-0 py-md-4">
      <div class="col-6 col-md-4 " v-for="keep in keeps" :key="keep.id">
        <KeepCard :keep="keep" />
      </div>
    </section>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed } from 'vue';
import KeepCard from "../components/KeepCard.vue";

export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps)
    }
  },
  components: { KeepCard }
};
</script>


<style lang="scss" scoped></style>