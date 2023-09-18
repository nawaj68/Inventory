import {Injectable} from "@angular/core";
import {Observable, Subject} from "rxjs";

@Injectable({
  providedIn: "root",
})
export class DataPassService {
  private messageSource = new Subject<any>();

  constructor() {}

  public getData(): Observable<String> {
    return this.messageSource.asObservable();
  }

  public setData(obj: any) {
    return this.messageSource.next(obj);
  }
}
