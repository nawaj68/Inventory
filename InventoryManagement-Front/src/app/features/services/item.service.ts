import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import { catchError, Observable } from "rxjs";
import {HttpService} from "src/app/core/services/http/http.service";
import { Item } from "../models/item.model";

const routePrefix = "/api/item";


@Injectable({
  providedIn: 'root'
})
export class ItemService extends HttpService<Item> {
  constructor(http: HttpClient) {
    super(http, routePrefix);
  }
  
  getItemDetail<T>(itemId: number): Observable<T> {
    return this.http.get<T[]>(`${this.BaseUrl}/${itemId}`).pipe(catchError(this.handleError));
  }

  addItemDetail<T>(item: T): Observable<T> {
    return this.http.post<T>(this.BaseUrl, item).pipe(catchError(this.handleError));
  }
  updateItemDetail<T>(itemId: number, item  : any): Observable<T> {
    return this.http.put<T>(`${this.BaseUrl}/${itemId}`, item).pipe(catchError(this.handleError));
  }
}