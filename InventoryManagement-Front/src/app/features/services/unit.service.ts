import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { HttpService } from 'src/app/core/services/http/http.service';
import { Unit } from '../models/unit.model';

const routePrefix = "/api/unit";

@Injectable({
  providedIn: 'root'
})
export class UnitService extends HttpService<Unit>{

  constructor(http: HttpClient) {
    super(http, routePrefix);
  }
  getUnitDetail<T>(unitId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${unitId}`).pipe(catchError(this.handleError));
  }

  addUnitDetail<T>(unit: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, unit).pipe(catchError(this.handleError));
  }
  updateUnitDetail<T>(unitId: number, unit  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${unitId}`, unit).pipe(catchError(this.handleError));
  }
}
