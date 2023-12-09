<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-12">
        <KeepContainer />
      </div>
    </section>
  </div>

  <span>
    <ModalComponent :modalId="'keepDetail'" :modalSize="'modal-xl'" :showHeader="false">
      <template #modalBody>
        <KeepDetails />
      </template>
    </ModalComponent>
  </span>
</template>

<script>
import KeepDetails from "../components/KeepDetails.vue";
import KeepContainer from "../components/KeepContainer.vue";
import ModalComponent from "../components/ModalComponent.vue";
import { AppState } from "../AppState.js";
import { computed } from "vue";

export default {
  setup() {
    return {
      activeKeep: computed(() => AppState.activeKeep),
    };
  },
  components: { ModalComponent, KeepContainer, KeepDetails }
}
</script>

<style scoped lang="scss"></style>
