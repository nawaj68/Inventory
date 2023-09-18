import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { HttpService } from 'src/app/core/services/http/http.service';
import { Return } from '../models/return.model';

const routePrefix = "/api/return";


@Injectable({
  providedIn: 'root'
})
export class ReturnService extends HttpService<Return> {

  constructor(http: HttpClient) {
    super(http, routePrefix);
  }
  getReturnDetail<T>(returnId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${returnId}`).pipe(catchError(this.handleError));
  }

  addReturnDetail<T>(returns: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, returns).pipe(catchError(this.handleError));
  }
  updateReturnDetail<T>(returnId: number, returns  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${returnId}`, returns).pipe(catchError(this.handleError));
  }
}