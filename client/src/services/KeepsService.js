import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {

  setActiveKeep(keep) { AppState.activeKeep = keep; }

  async getKeeps() {
    const res = await api.get('api/keeps');
    AppState.keeps = res.data.map(keep => new Keep(keep));
  }

  async getKeepById(keepId) {
    const res = await api.get('api/keeps/' + keepId);
    AppState.activeKeep = new Keep(res.data);
  }

  async getMyKeeps() {
    const res = await api.get('account/keeps');
    AppState.myKeeps = res.data.map(keep => new Keep(keep));
  }

  async getKeepsByVaultId(vaultId) {
    AppState.vaultKeeps = [];
    const res = await api.get(`api/vaults/${vaultId}/keeps/`);
    AppState.vaultKeeps = res.data.map(keep => new Keep(keep));
  }

  async createKeep(keepData) {
    const res = await api.post('api/keeps/', keepData)
    const newKeep = new Keep(res.data);
    AppState.activeKeep = newKeep;
    AppState.keeps.push(newKeep);
  }

  async deleteKeep(keepId) {
    const res = await api.delete('api/keeps/' + keepId);
    AppState.keeps = AppState.keeps.filter(keep => keep.id != keepId);
    AppState.activeKeep = null;
    logger.log('Keep deletion result:', res.data);
  }

}

export const keepsService = new KeepsService();