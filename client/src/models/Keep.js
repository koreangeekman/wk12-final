import { DataModel } from "./DataModel.js"

export class Keep extends DataModel {
  constructor(data) {
    // this.id = data.id
    // this.creatorId = data.creatorId
    // this.img = data.img
    // this.name = data.name
    // this.description = data.description
    this.views = data.views
    this.kept = data.kept
    this.vaultKeepId = data.vaultKeepId
    // this.creator = data.creator
  }
}