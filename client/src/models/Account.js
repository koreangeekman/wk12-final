import { Profile } from "./Profile.js";

export class Account extends Profile {
  constructor(data) {
    super(data)
    // this.id = data.id
    // this.name = data.name
    // this.picture = data.picture
    this.email = data.email
  }
}
