import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import { catchError, Observable } from "rxjs";
import {HttpService} from "src/app/core/services/http/http.service";
import { Country } from "../models/country.model";

const routePrefix = "/api/country";

@Injectable({
  providedIn: "root",
})
export class CountryService extends HttpService<Country> {
  constructor(http: HttpClient) {
    super(http, routePrefix);
  }
  getCountryDetail<T>(countryId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${countryId}`).pipe(catchError(this.handleError));
  }

  addCountryDetail<T>(country: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, country).pipe(catchError(this.handleError));
  }
  updateCountryDetail<T>(countryId: number, country  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${countryId}`, country).pipe(catchError(this.handleError));
  }
}
