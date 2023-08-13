export class APIResponse {
  Status: APIResponseStatus = APIResponseStatus.Error
  Data: any | any[]
  Messages: any
  TotalCount: any
}

export enum APIResponseStatus {
  Error = 0,
  Success = 1,
  Warning = 2,
}
