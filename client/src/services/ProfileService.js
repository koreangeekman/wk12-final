import { AppState } from "../AppState.js";
import { Profile } from "../models/Profile.js";
import { api } from "./AxiosService.js";

class ProfileService {

  async GetProfile(profileId) {
    const res = await api.get('api/profiles/' + profileId);
    AppState.activeProfile = new Profile(res.data);
  }

}

export const profileService = new ProfileService();