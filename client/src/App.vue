<template>
  <div class="order-2 order-md-1">
    <header>
      <Navbar />
    </header>
  </div>
  <div class="order-1 order-md-2">
    <main>
      <router-view />
    </main>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 64px);
  --color-1: #F9F6FA;
  --color-2: #FEF6F0;
  --color-3: #E9D8D6;
  --color-4: #DED6E9;
  --color-5: #877A8F;
  --color-6: #2D2D2D;
}
</style>
