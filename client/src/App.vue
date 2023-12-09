<template>
  <div class="order-2 order-md-1">
    <header>
      <Navbar />
    </header>
  </div>
  <div class="order-1 order-md-2 px-0 px-lg-3">
    <main>
      <router-view />
    </main>
  </div>

  <!-- 
  <ModalComponent :modalId="'exampleModal'" :modalSize="'modal-lg'" :showHeader="true" :showFooter="false">
    <template #modalHeader></template>
    <template #modalBody></template>
    <template #modalFooter></template>
  </ModalComponent>
   -->

  <ModalComponent :modalId="'createKeep'">
    <template #modalHeader>Add your keep</template>
    <template #modalBody>
      <KeepForm />
    </template>
  </ModalComponent>

  <ModalComponent :modalId="'createVault'">
    <template #modalHeader>Add your vault</template>
    <template #modalBody>
      <VaultForm />
    </template>
  </ModalComponent>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import KeepForm from './components/KeepForm.vue'
import VaultForm from "./components/VaultForm.vue"
import ModalComponent from "./components/ModalComponent.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, ModalComponent, KeepForm, VaultForm }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 96px);
  --color-1: #F9F6FA;
  --color-2: #FEF6F0;
  --color-3: #E9D8D6;
  --color-4: #DED6E9;
  --color-5: #877A8F;
  --color-6: #2D2D2D;
}

header {
  box-shadow: 0 0 .25rem .25rem var(--color-3);
}

main {
  height: var(--main-height);
  overflow-y: scroll;
}

.app-font {
  font-family: 'Marko One', serif;
  font-size: 1rem;
  font-weight: 400;
  line-height: 1.25rem;
  letter-spacing: 0em;
}

// https://dev.to/xtrp/how-to-create-a-beautiful-custom-scrollbar-for-your-site-in-plain-css-1mjg
::-webkit-scrollbar {
  width: 20px;
}

::-webkit-scrollbar-track {
  background-color: transparent;
}

::-webkit-scrollbar-thumb {
  background-color: #d6dee1;
  border-radius: 20px;
  border: 6px solid transparent;
  background-clip: content-box;
}

::-webkit-scrollbar-thumb:hover {
  background-color: #a8bbbf;
}
</style>
