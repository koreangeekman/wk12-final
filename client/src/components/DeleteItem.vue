<template>
  <div v-if="account?.id && (account.id == keep?.creatorId || account.id == vault?.creatorId)"
    class="position-absolute deleteMe selectable rounded-circle">
    <i class="mdi mdi-delete-forever text-danger px-2" @click.stop="routeDelete()"></i>
  </div>
</template>


<script>
import Pop from "../utils/Pop.js";
import { computed } from 'vue';
import { useRoute } from "vue-router";
import { AppState } from '../AppState.js';
import { Vault } from "../models/Vault.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";

export default {
  props: {
    keep: { type: Keep, default: null },
    vault: { type: Vault, default: null },
    itemType: { type: String, required: true }
  },
  setup(props) {
    const route = useRoute();
    return {
      route,
      account: computed(() => AppState.account),
      routeDelete() {
        if (props.itemType == 'keep') {
          this.deleteKeep();
        }
        else { this.deleteVault(); }
      },
      async deleteKeep() {
        try {
          const yes = await Pop.confirm("Permanently delete this keep?");
          if (!yes) { return }
          await keepsService.deleteKeep(props.keep.id);
        }
        catch (error) { Pop.error(error); }
      },
      async deleteVault() {
        try {
          const yes = await Pop.confirm("Permanently delete this vault?");
          if (!yes) { return }
          await vaultsService.deleteVault(props.vault.id);
        }
        catch (error) { Pop.error(error); }
      }
    }
  }
};
</script>


<style lang="scss" scoped>
.deleteMe {
  top: .2rem;
  right: .2rem;
  aspect-ratio: 1/1;
  line-height: 2.5rem;
  transition: .25s;
  font-size: larger;
  opacity: .8;
  text-shadow: 0 0 .42rem var(--color-1);
}

.deleteMe:hover {
  font-size: x-large;
  box-shadow: 0 0 8px yellow inset;
  opacity: 1;
  background-color: var(--color-3);
}
</style>