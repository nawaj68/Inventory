import {MyHttpHelper} from "./http.helper";
import {HttpClient, HttpErrorResponse, HttpHeaders, HttpParams} from "@angular/common/http";
import {Inject, Injectable} from "@angular/core";
import {Observable, catchError} from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export abstract class HttpService<T> {
  protected readonly BaseUrl: string;

  public readonly defaultHttpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
    }),
  };

  public readonly multipartHttpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "multipart/form-data",
      Accept: "multipart/form-data",
    }),
  };

  public readonly undefinedtHttpOptions = {
    headers: new HttpHeaders({"Content-Type": ""}),
  };

  constructor(protected http: HttpClient, @Inject(String) routePrefix: string) {
    this.BaseUrl = environment.baseUrl + routePrefix;
  }

  getPaging(pageIndex: number, pageSize: number): Observable<T[]> {
    let params = new HttpParams().set("pageIndex", pageIndex.toString()).set("pageSize", pageSize.toString());

    return this.http.get<T[]>(`${this.BaseUrl}/page?${params.toString()}`).pipe(catchError(this.handleError));
  }

  getSearch(pageIndex: number, pageSize: number, sortColumn: string, sortDirection: string, searchText: string): Observable<T[]> {
    let params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("searchText", searchText);

    // console.log(pageIndex, pageSize, sortColumn, sortDirection, searchText);
    return this.http.get<T[]>(`${this.BaseUrl}/search?${params.toString()}`).pipe(catchError(this.handleError));
  }

  getDropdown(pageIndex: number, pageSize: number, searchText: string): Observable<T[]> {
    let params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("searchText", searchText);

    return this.http.get<T[]>(`${this.BaseUrl}/dropdown?${params.toString()}`).pipe(catchError(this.handleError));
  }

  get(id: string | number): Observable<T> {
    return this.http.get<T>(`${this.BaseUrl}/${id}`).pipe(catchError(this.handleError));
  }

  add<T>(resource: T, endpoint: string = "", options: any = null): Observable<T> {
    // if (!endpoint) endpoint = this.BaseUrl;
    if (options == null) options = this.defaultHttpOptions;

    return this.http.post<T>(`${endpoint}`, resource, options).pipe(catchError(this.handleError));
  }

  delete(id: string | number): Observable<any> {
    return this.http.delete(`${this.BaseUrl}/delete/${id}`).pipe(catchError(this.handleError));
  }

  update<T>(resource: T, endpoint: string = "", options: any = null): Observable<T> {
    // if (!endpoint) endpoint = this.BaseUrl;
    // console.log('endpoint', endpoint);
    if (options == null) options = this.defaultHttpOptions;

    return this.http.put<T>(`${endpoint}`, resource, options).pipe(catchError(MyHttpHelper.handleError));
  }

  public handleError(error: HttpErrorResponse): Observable<any> {
    return MyHttpHelper.handleError(error);
  }
}

// https://nichola.dev/generic-approach-to-consume-rest-api/
