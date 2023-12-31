import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},

  /** @type {import('./models/Profile.js').Profile} */
  activeProfile: null,

  /** @type {import('./models/Vault.js').Vault[]} */
  vaults: [],
  /** @type {import('./models/Vault.js').Vault[]} */
  myVaults: [], // for form select array
  /** @type {import('./models/Vault.js').Vault} */
  activeVault: null,

  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],
  /** @type {import('./models/Keep.js').Keep} */
  activeKeep: null,

  /** @type {import('./models/VaultKeep.js').VaultKeep[]} */
  myVaultKeeps: [],

  ogVault: null // keep form vault value comparison check

})
