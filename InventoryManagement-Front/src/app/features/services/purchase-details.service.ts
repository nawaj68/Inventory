import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { HttpService } from 'src/app/core/services/http/http.service';
import { PurchaseDetails } from '../models/purchaseDetails.model';

const routePrefix = "/api/PurchaseDetails";
@Injectable({
  providedIn: 'root'
})
export class PurchaseDetailsService extends HttpService<PurchaseDetails> {

  constructor(http: HttpClient) {
    super(http, routePrefix);
  }
  getPurchaseDetailsDetail<T>(purchaseDetailsId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${purchaseDetailsId}`).pipe(catchError(this.handleError));
  }

  addPurchaseDetailsDetail<T>(purchaseDetails: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, purchaseDetails).pipe(catchError(this.handleError));
  }
  updatePurchaseDetailsDetail<T>(purchaseDetailsId: number, purchaseDetails  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${purchaseDetailsId}`, purchaseDetails).pipe(catchError(this.handleError));
  }
}
