import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import { catchError, Observable } from "rxjs";
import {HttpService} from "src/app/core/services/http/http.service";
import { SubCategory } from "../models/subCategory.model";

const routePrefix = "/api/subCategory";

@Injectable({
  providedIn: 'root'
})
export class SubCategoryService extends HttpService<SubCategory> {

  constructor(http: HttpClient) {
    super(http, routePrefix);
  }
  getSubCategoryDetail<T>(subCategoryId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${subCategoryId}`).pipe(catchError(this.handleError));
  }

  addSubCategoryDetail<T>(subCategory: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, subCategory).pipe(catchError(this.handleError));
  }
  updateSubCategoryDetail<T>(subCategoryId: number, subCategory  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${subCategoryId}`, subCategory).pipe(catchError(this.handleError));
  }
}