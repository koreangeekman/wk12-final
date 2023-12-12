import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {

  setActiveKeep(keepObj) { AppState.activeKeep = keepObj; }

  async getKeeps() {
    AppState.keeps = [];
    const res = await api.get('api/keeps');
    AppState.keeps = (res.data.map(keep => new Keep(keep))).sort((a, b) => b.id - a.id);
  }

  async getKeepById(keepId, vkId) {
    AppState.activeKeep = null;
    const res = await api.get('api/keeps/' + keepId);
    AppState.activeKeep = new Keep(res.data);
    AppState.activeKeep.vaultKeepId = vkId;
  }

  async getKeepsByProfileId(profileId) {
    AppState.keeps = [];
    const res = await api.get(`api/profiles/${profileId}/keeps`);
    AppState.keeps = (res.data.map(keep => new Keep(keep))).sort((a, b) => b.id - a.id);
  }

  async getKeepsByVaultId(vaultId) {
    AppState.keeps = [];
    const res = await api.get(`api/vaults/${vaultId}/keeps/`);
    AppState.keeps = (res.data.map(keep => new Keep(keep))).sort((a, b) => b.id - a.id);
  }

  async createKeep(keepData) {
    const res = await api.post('api/keeps/', keepData)
    const newKeep = new Keep(res.data);
    AppState.activeKeep = newKeep;
    AppState.keeps.unshift(newKeep);
  }

  async deleteKeep(keepId) {
    const res = await api.delete('api/keeps/' + keepId);
    AppState.keeps = AppState.keeps.filter(keep => keep.id != keepId);
    AppState.activeKeep = null;
    logger.log('Keep deletion result:', res.data);
  }

}

export const keepsService = new KeepsService();