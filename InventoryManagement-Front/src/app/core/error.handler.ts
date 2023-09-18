import { HttpErrorResponse } from '@angular/common/http';
import {ErrorHandler, Injectable, Injector} from "@angular/core";
import {Observable, throwError} from "rxjs";
import {MessageService} from "./services/message/message.service";

@Injectable()
export class MyErrorHandler implements ErrorHandler {
  constructor(private messageService: MessageService, private injector: Injector) {}
  handleError(error: any): Observable<any> {
    let errorMessage = "";
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`; // client side error
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`; // server side error
    }

    console.log('handler1', error);
    // this.messageService.toast(`${errorMessage}`, "error");
    // let router = this.injector.get(Router);
    // console.log('URL: ' + router.url);
    // console.error(error);

    // router.navigate(['/error']);

    //return new Error(errorMessage);
    return throwError(() => new Error(`Something bad happened; please try again later. ${error.message}`));
  }

  // private handleError(err: HttpErrorResponse): Observable<never> {
  //   // just a test ... more could would go here
  //   return throwError(() => err);
  // }
}

// https://rxjs.dev/api/index/function/throwError
