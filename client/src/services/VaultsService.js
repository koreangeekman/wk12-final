import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService {

  async getVaults() {
    AppState.vaults = [];
    const res = await api.get('api/vaults');
    const vaults = res.data.map(vault => new Vault(vault));
    AppState.vaults = vaults.reverse();
  }

  async getMyVaults() {
    const res = await api.get('account/vaults/');
    const vaults = res.data.map(vault => new Vault(vault));
    AppState.myVaults = vaults.sort((a, b) => b.id - a.id);
  }

  async getVaultById(vaultId) {
    AppState.activeVault = null;
    const res = await api.get('api/vaults/' + vaultId);
    AppState.activeVault = new Vault(res.data);
  }

  async getVaultsByProfileId(profileId) {
    AppState.vaults = [];
    const res = await api.get(`api/profiles/${profileId}/vaults`);
    const vaults = res.data.map(vault => new Vault(vault));
    AppState.vaults = vaults.sort((a, b) => b.id - a.id);
  }

  async createVault(vaultData) {
    const res = await api.post('api/vaults', vaultData);
    const newVault = new Vault(res.data);
    AppState.activeVault = newVault;
    AppState.vaults.unshift(newVault);
    AppState.myVaults.unshift(newVault);
    // AppState.myVaults.sort();
  }

  async deleteVault(vaultId) {
    const res = await api.delete('api/vaults/' + vaultId);
    AppState.vaults = AppState.vaults.filter(vault => vault.id != vaultId);
    AppState.myVaults = AppState.myVaults.filter(vault => vault.id != vaultId);
    // TODO Test remove vaults from myVaults selection list when deleted
    logger.log('Vault deletion result:', res.data);
  }

}

export const vaultsService = new VaultsService();