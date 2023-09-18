import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import { catchError, Observable } from "rxjs";
import {HttpService} from "src/app/core/services/http/http.service";
import { Category } from "../models/category.model";

const routePrefix = "/api/category";

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends HttpService<Category> {

  constructor(http: HttpClient) {
    super(http, routePrefix);
  }
  getCategoryDetail<T>(categoryId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${categoryId}`).pipe(catchError(this.handleError));
  }

  addCategoryDetail<T>(category: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, category).pipe(catchError(this.handleError));
  }
  updateCategoryDetail<T>(categoryId: number, category  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${categoryId}`, category).pipe(catchError(this.handleError));
  }
}
