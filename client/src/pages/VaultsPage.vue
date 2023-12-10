<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-12">
        <VaultContainer />
      </div>
    </section>
  </div>
</template>

<script>
import VaultContainer from "../components/VaultContainer.vue";

export default {
  setup() {
    return {};
  },
  components: { VaultContainer }
}
</script>

<style scoped lang="scss"></style>
