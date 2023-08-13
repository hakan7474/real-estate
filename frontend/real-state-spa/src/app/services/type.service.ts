import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { APIResponse } from './wrappers/api-response';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class TypeService extends BaseService {

  baseUrl: string = environment.EstateApiUrl;

  getTypes(typeCode: string): Observable<APIResponse> {
    return this.get(this.baseUrl + `Type/${typeCode}`);
  }

}
