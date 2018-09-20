export class Model {
  VehicleModelId: number;
  Name: string;
  Abrv: string;
  VehicleMakeId: number;
}

export class ModelViewModel {
  Items: Array<Model>;
  TotalCount: number;
  PageNumber: number;
  PageSize: number;
  TotalPageCount: number;
}
