import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { APIResponse } from './wrappers/api-response';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class EstateService extends BaseService {

  baseUrl: string = environment.EstateApiUrl + 'Estate';


  getEstates(): Observable<APIResponse> {
    return this.get(this.baseUrl);
  }

  getEstateById(estateId: any): Observable<APIResponse> {
    return this.get(this.baseUrl + `/${estateId}`);
  }

  addEstate(data: any): Observable<APIResponse> {
    return this.post(this.baseUrl, data);
  }

  updateEstate(data: any): Observable<APIResponse> {
    return this.put(this.baseUrl, data);
  }

  deleteEstate(estateId: any): Observable<APIResponse> {
    return this.delete(this.baseUrl + `/${estateId}`);
  }

}
