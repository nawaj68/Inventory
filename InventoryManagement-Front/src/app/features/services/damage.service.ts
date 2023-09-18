import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { HttpService } from 'src/app/core/services/http/http.service';
import { Damage } from '../models/damage.model';

const routePrefix = "/api/damage";

@Injectable({
  providedIn: 'root'
})
export class DamageService extends HttpService<Damage>{

  constructor(http: HttpClient) {
    super(http, routePrefix);
  }
  getDamageDetail<T>(damageId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${damageId}`).pipe(catchError(this.handleError));
  }

  addDamageDetail<T>(damage: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, damage).pipe(catchError(this.handleError));
  }
  updateDamageDetail<T>(damageId: number, damage  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${damageId}`, damage).pipe(catchError(this.handleError));
  }
}
