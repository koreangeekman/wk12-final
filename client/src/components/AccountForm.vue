<template>
  <form class="container-fluid card bg-light p-3" @submit.prevent="updateProfile()">
    <section class="row justify-content-between">
      <div class="col-12">
        <p class="fs-3 text-center">Edit profile</p>
        <hr>
      </div>

      <div class="col-12 col-md-6">
        <section class="user">
          <div class="m-3 mt-0">
            <label for="name">Name</label>
            <input v-model="accountForm.name" class="ms-2 form-control" type="text" id="name" maxlength="96">
          </div>
          <div class="m-3 mt-0">
            <label for="picture">User Picture URL</label>
            <input v-model="accountForm.picture" class="ms-2 form-control" type="url" id="picture" maxlength="255">
          </div>
          <div class="m-3 mt-0">
            <label for="coverImg">Cover Image</label>
            <input v-model="accountForm.coverImg" class="ms-2 form-control" type="url" id="coverImg" maxlength="255">
          </div>
        </section>
      </div>
      <div class="col-12 col-md-6">
        <section class="socials">
          <div class="m-3 mt-0">
            <label for="github">Github</label>
            <input v-model="accountForm.github" class="ms-2 form-control" type="url" id="github" maxlength="64">
          </div>
          <div class="m-3 mt-0">
            <label for="linkedin">LinkedIn</label>
            <input v-model="accountForm.linkedin" class="ms-2 form-control" type="url" id="linkedin" maxlength="64">
          </div>
          <div class="m-3 mt-0">
            <label for="website">Website</label>
            <input v-model="accountForm.website" class="ms-2 form-control" type="text" id="website" maxlength="64">
          </div>
        </section>
      </div>
      <div class="col-12">
        <div class="mx-3 mt-0 mb-4">
          <label for="bio">Bio</label>
          <textarea v-model="accountForm.bio" class="ms-2 form-control" name="bio" id="bio" rows="4"
            maxlength="500"></textarea>
        </div>
        <hr>
      </div>

      <div class="d-flex justify-content-center pe-3">
        <button class="btn btn-success" type="submit" aria-label="Submit Account Changes">Submit Changes</button>
      </div>
    </section>
  </form>
</template>


<script>
import { ref, watchEffect } from "vue";
import { AppState } from "../AppState";
import { accountService } from "../services/AccountService";
import Pop from "../utils/Pop";
import { Modal } from "bootstrap";

export default {
  setup() {

    const accountForm = ref({});

    watchEffect(() => {
      if (AppState.activeProfile) {
        accountForm.value = { ...AppState.activeProfile };
      }
      else {
        accountForm.value = {};
      }
    });

    return {
      accountForm,

      async updateProfile() {
        try {
          await accountService.updateProfile(accountForm.value);
          Modal.getInstance('#editAccount').hide();
        }
        catch (error) { Pop.error(error); }
      }

    }
  }
};
</script>


<style lang="scss" scoped></style>