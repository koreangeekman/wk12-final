import { AppState } from "../AppState";
import { VaultKeep } from "../models/VaultKeep";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VaultKeepService {

  // async getMyVaultKeeps() {
  //   const res = await api.get('api/vaultkeeps');
  //   AppState.myVaultKeeps = res.data.map(vk => new VaultKeep(vk));
  // }

  async createVaultKeep(vkData) {
    const res = await api.post('api/vaultkeeps', vkData);
    // AppState.myVaultKeeps.push(new VaultKeep(res.data));
    logger.log('Vault-Keep association created', new VaultKeep(res.data));
  }

  async deleteVaultKeep(vkId) {
    const res = await api.delete('api/vaultkeeps/' + vkId);
    // AppState.myVaultKeeps = AppState.myVaultKeeps.filter(vk => vk.id != vkId);
    logger.log('Vault-Keep association deleted..', res.data)
  }
}

export const vaultKeepService = new VaultKeepService();