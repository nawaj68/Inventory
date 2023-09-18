import { Injectable } from '@angular/core';
import { HttpService } from 'src/app/core/services/http/http.service';
import { Company } from '../models/company.model';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';

const routePrefix = "/api/company";
@Injectable({
  providedIn: 'root'
})
export class CompanyService extends HttpService<Company> {

  constructor(http : HttpClient) {
    super(http,routePrefix)
   }
   getCompanyDetail<T>(companyId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${companyId}`).pipe(catchError(this.handleError));
  }

  addCompanyDetail<T>(company: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, company).pipe(catchError(this.handleError));
  }
  updateCompanyDetail<T>(companyId: number, company  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${companyId}`, company).pipe(catchError(this.handleError));
  }
}
