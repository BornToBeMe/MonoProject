export class Make {
  Id: number;
  Name: string;
  Abrv: string;
}

export class MakeViewModel {
  Items: Array<Make>;
  TotalCount: number;
  PageNumber: number;
  PageSize: number;
  TotalPageCount: number;
}
