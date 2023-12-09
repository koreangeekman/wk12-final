import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { api } from "./AxiosService.js";

class KeepsService {

  async getKeeps() {
    const res = await api.get('api/keeps');
    AppState.keeps = res.data.map(keep => new Keep(keep));
  }

  async getKeepById(keepId) {
    const res = await api.get('api/keeps/' + keepId);
    AppState.activeKeep = new Keep(res.data);
  }

}

export const keepsService = new KeepsService();