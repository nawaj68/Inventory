import { Injectable } from '@angular/core';
import { PurchasesMaster } from '../models/purchasesMaster.model';
import { HttpService } from 'src/app/core/services/http/http.service';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
const routePrefix = "/api/purchasesMaster";
@Injectable({
  providedIn: 'root'
})
export class PurchasesMasterService extends HttpService <PurchasesMaster> {

  constructor(http:HttpClient) {
    super(http,routePrefix);
  }
  getPurchasesMasterDetail<T>(purchasesmasterId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${purchasesmasterId}`).pipe(catchError(this.handleError));
  }

  addPurchasesMasterDetail<T>(purchasesmaster: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, purchasesmaster).pipe(catchError(this.handleError));
  }
  updatePurchasesMasterDetail<T>(purchasesmasterId: number, purchasesmaster  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${purchasesmasterId}`, purchasesmaster).pipe(catchError(this.handleError));
  }
}
