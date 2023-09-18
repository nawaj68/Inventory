import {MessageService} from "./../message/message.service";
import {HttpErrorResponse} from "@angular/common/http";
import {Observable, throwError} from "rxjs";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class MyHttpHelper {
  public static handleError(error: HttpErrorResponse): Observable<any> {
    const messegeService = new MessageService();
    let errorMessage = "";

    if (error.status === 0) {
      errorMessage = `Error: ${error.statusText}\n ${error.message}`;
    } else {
      errorMessage = `Error Code: ${error.status}\n${error.statusText}\nMessage: ${error.message}`;
    }
    console.log('http', error);

    messegeService?.toast(errorMessage, 'error');

    return throwError(() => new Error(`HTTP:  ${errorMessage}`));
  }
}

// global error handler
// static handler - MyHttpHandler
// gloabl error handler - MyErrorHandler
// http intercept - HttpErrorInterceptor
