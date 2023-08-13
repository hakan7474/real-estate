import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { APIResponse } from './wrappers/api-response';
import { Observable } from 'rxjs';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor(private _http: HttpClient,
    private _notificationService: NotificationService
  ) { }

  getHeaders() {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    return headers;
  }

  get(requestUrl: string): Observable<APIResponse> {
    return this._http.get<APIResponse>(requestUrl, { headers: this.getHeaders() });
  }

  post(requestUrl: string, data: any): Observable<APIResponse> {
    return this._http.post<APIResponse>(requestUrl, data, { headers: this.getHeaders() });
  }

  put(requestUrl: string, data: any): Observable<APIResponse> {
    return this._http.put<APIResponse>(requestUrl, data, { headers: this.getHeaders() });
  }

  delete(requestUrl: string): Observable<APIResponse> {
    return this._http.delete<APIResponse>(requestUrl, { headers: this.getHeaders() });
  }

  errorWithDictionary(messages: any) {
    if (messages)
      Object.keys(messages).forEach(msg => {

        if (!messages[msg])
          return;

        if (messages[msg].length < 500) {
          this._notificationService.openSnackBar(messages[msg]);
        } else {
          this._notificationService.openSnackBar("An error occurred during processing");
        }

      });
  }

}
