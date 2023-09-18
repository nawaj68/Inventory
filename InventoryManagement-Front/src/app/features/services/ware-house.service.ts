import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { HttpService } from 'src/app/core/services/http/http.service';
import { WareHouse } from '../models/wareHouse.model';

const routePrefix = "/api/wareHouse";


@Injectable({
  providedIn: 'root'
})
export class WareHouseService extends HttpService<WareHouse>{

  constructor(http: HttpClient) {
    super(http, routePrefix);
  }
  getWareHouseDetail<T>(wareHouseId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${wareHouseId}`).pipe(catchError(this.handleError));
  }

  addWareHouseDetail<T>(wareHouse: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, wareHouse).pipe(catchError(this.handleError));
  }
  updateWareHouseDetail<T>(wareHouseId: number, unit  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${wareHouseId}`, unit).pipe(catchError(this.handleError));
  }
}
