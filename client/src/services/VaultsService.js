import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { api } from "./AxiosService.js";

class VaultsService {

  async getVaultsByProfileId(profileId) {
    const res = await api.get('api/profiles/' + profileId);
    AppState.vaults = res.data.map(vault => new Vault(vault));
  }

}

export const vaultsService = new VaultsService();