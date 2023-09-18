import { Injectable } from '@angular/core';
import { HttpService } from 'src/app/core/services/http/http.service';
import { Supplier } from './../models/supplier.model';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';

const routePrefix = "/api/supplier";
@Injectable({
  providedIn: 'root'
})
export class SupplierService extends HttpService<Supplier> {

  constructor(http: HttpClient) {
    super(http,routePrefix);
  }
  getSupplierDetail<T>(supplierId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${supplierId}`).pipe(catchError(this.handleError));
  }

  addSupplierDetail<T>(supplier: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, supplier).pipe(catchError(this.handleError));
  }
  updateSupplierDetail<T>(supplierId: number, supplier  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${supplierId}`, supplier).pipe(catchError(this.handleError));
  }
}
