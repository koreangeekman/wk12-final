import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService {

  async getVaults() {
    const res = await api.get('api/vaults');
    AppState.vaults = res.data.map(vault => new Vault(vault));
  }

  async getMyVaults() {
    const res = await api.get('account/vaults');
    AppState.myVaults = res.data.map(vault => new Vault(vault));
  }

  async getVaultById(vaultId) {
    const res = await api.get('api/vaults/' + vaultId);
    AppState.activeVault = new Vault(res.data);
  }

  async getVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`);
    AppState.vaults = res.data.map(vault => new Vault(vault));
  }

  async createVault(vaultData) {
    const res = await api.post('api/vaults', vaultData);
    const newVault = new Vault(res.data);
    AppState.activeVault = newVault;
    AppState.vaults.push(newVault);
  }

  async deleteVault(vaultId) {
    const res = await api.delete('api/delete/' + vaultId);
    AppState.vaults = AppState.vaults.filter(vault => vault.id != vaultId);
    logger.log('Vault deletion result:', res.data);
  }

}

export const vaultsService = new VaultsService();