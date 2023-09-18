import {HttpClient, HttpParams} from "@angular/common/http";
import {Injectable} from "@angular/core";
import { catchError, Observable } from "rxjs";
import {HttpService} from "src/app/core/services/http/http.service";
import { City } from "../models/city.model";

const routePrefix = "/api/city";

@Injectable({
  providedIn: 'root'
})
export class CityService extends HttpService<City> {

  constructor(http: HttpClient) {
    super(http, routePrefix);
  }
  getDropdownByState<T>(stateId: number, searchText?: string): Observable<T[]> {
    let params = new HttpParams()
    .set("searchText", searchText)
    .set("stateId", stateId);
    // if (searchText) params.set("searchText", searchText);

    return this.http.get<T[]>(`${this.BaseUrl}/dropdown?${params.toString()}`).pipe(catchError(this.handleError));
  }

  getCityDetail<T>(cityId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${cityId}`).pipe(catchError(this.handleError));
  }

  addCityDetail<T>(city: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, city).pipe(catchError(this.handleError));
  }
  
  updateCityDetail<T>(cityId: number, city  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${cityId}`, city).pipe(catchError(this.handleError));
  }
}
