import { Make } from './make.model';

export class Model {
  VehicleModelId: number;
  Name: string;
  Abrv: string;
  VehicleMakeId: number;
}

export class ModelViewModel {
  Items: Array<Model>;
  MakeList: Array<Make>;
  TotalCount: number;
  PageNumber: number;
  PageSize: number;
  TotalPageCount: number;
}
