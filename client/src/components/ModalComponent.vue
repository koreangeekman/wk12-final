<template>
  <div class="modal fade" :id="modalId" tabindex="-1" :aria-labelledby="modalId + 'Label'" aria-hidden="true">
    <div class="modal-dialog" :class="modalSize">
      <div class="modal-content">
        <div class="modal-body p-0">
          <span v-if="showHeader" class="d-flex justify-content-between">
            <h1 class="modal-title fs-4 m-4" :id="modalId + 'Label'">
              <slot name="modalHeader">Modal title</slot>
            </h1>
            <button type="button" class="btn-close m-4" data-bs-dismiss="modal" aria-label="Close"></button>
          </span>
          <slot name="modalBody">Modal Body</slot>
        </div>
        <div v-if="showFooter" class="modal-footer">
          <slot name="modalFooter">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" aria-label="Close">Close</button>
            <button type="button" class="btn btn-primary" aria-label="Save changes">Save changes</button>
          </slot>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed } from 'vue';

export default {
  props: {
    modalId: { type: String },
    modalSize: { type: String, default: '' },
    showHeader: { type: Boolean, default: true },
    showFooter: { type: Boolean, default: false },
  },
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
};
</script>


<style lang="scss" scoped></style>