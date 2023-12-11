<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-12">
        <div class="cover-image">
        </div>
        <div class="position-relative d-flex justify-content-center">
          <div class="position-absolute d-block text-center user-profile">
          </div>
          <!-- <div class="position-absolute user-edit" v-if="route.name == 'Account'"> -->
          <i class="fs-1 mdi mdi-dots-horizontal btn selectable py-0 my-2" @click="editAccount()"></i>
          <!-- </div> -->
        </div>
      </div>
    </section>
  </div>
</template>

<script>

export default {
  setup() {
    return {};
  },
}
</script>

<style scoped lang="scss"></style>
