import {HttpClient, HttpParams} from "@angular/common/http";
import {Injectable} from "@angular/core";
import { catchError, Observable } from "rxjs";
import {HttpService} from "src/app/core/services/http/http.service";
import { State } from "../models/state.model";

const routePrefix = "/api/state";

@Injectable({
  providedIn: 'root'
})
export class StateService extends HttpService<State> {

  constructor(http: HttpClient) {
    super(http, routePrefix);
  }
  getDropdownByCountry<T>(countryId: number, searchText?: string): Observable<T[]> {
    let params = new HttpParams()
    .set("searchText", searchText)
    .set("countryId", countryId);
    // if (searchText) params.set("searchText", searchText);

    return this.http.get<T[]>(`${this.BaseUrl}/dropdown?${params.toString()}`).pipe(catchError(this.handleError));
  }
  getStateDetail<T>(stateId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${stateId}`).pipe(catchError(this.handleError));
  }

  addStateDetail<T>(state: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, state).pipe(catchError(this.handleError));
  }
  updateStateDetail<T>(stateId: number, state  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${stateId}`, state).pipe(catchError(this.handleError));
  }
}
