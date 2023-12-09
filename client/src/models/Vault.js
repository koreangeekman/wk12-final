import { DataModel } from "./DataModel.js"

export class Vault extends DataModel {
  constructor(data) {
    super(data)
    // this.id = data.id
    // this.creatorId = data.creatorId
    // this.img = data.img
    // this.name = data.name
    // this.description = data.description
    this.isPrivate = data.isPrivate
    // this.creator = data.creator
  }
}