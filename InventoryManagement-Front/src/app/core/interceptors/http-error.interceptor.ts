import {Injectable} from "@angular/core";
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse, HttpErrorResponse} from "@angular/common/http";
import {catchError} from "rxjs/operators";
import {Router} from "@angular/router";
import { Observable, of, throwError } from "rxjs";

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(public router: Router) {}

  handleError(error: HttpErrorResponse){
    console.log("lalalalalalalala");
    return throwError(() => error.message);
   }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        if (error instanceof HttpErrorResponse) {
          if (error.error instanceof ErrorEvent) {
            console.error("Error Event");
          } else {
            console.log(`error status : ${error.status} ${error.statusText}`);
            switch (error.status) {
              case 401: //login
                this.router.navigateByUrl("/login");
                break;
              case 403:
                this.router.navigateByUrl("/unauthorized");
                break;
            }
          }
        } else {
          console.error("some thing else happened");
        }

        console.log("error is intercept");
        return throwError(() => error.message);
      })
    );
  }

  //1.  No Errors
  intercept1(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        console.log("error in intercept");
        console.error(error);
        return throwError(() => error.message);
      })
    );
  }

  //2. Sending an Invalid Token will generate error
  intercept5(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token: string = "invald token";
    req = req.clone({headers: req.headers.set("Authorization", "Bearer " + token)});

    return next.handle(req).pipe(
      catchError((error) => {
        console.log("error in intercept");
        console.error(error);
        return throwError(() => error.message);
      })
    );
  }

  intercept3(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token: string = "invald token";
    req = req.clone({headers: req.headers.set("Authorization", "Bearer " + token)});

    return next.handle(req).pipe(
      catchError((error) => {
        let handled: boolean = false;
        console.error(error);
        if (error instanceof HttpErrorResponse) {
          if (error.error instanceof ErrorEvent) {
            console.error("Error Event");
          } else {
            console.log(`error status : ${error.status} ${error.statusText}`);
            switch (error.status) {
              case 401: //login
                this.router.navigateByUrl("/login");
                console.log(`redirect to login`);
                handled = true;
                break;
              case 403: //forbidden
                this.router.navigateByUrl("/login");
                console.log(`redirect to login`);
                handled = true;
                break;
            }
          }
        } else {
          console.error("Other Errors");
        }

        if (handled) {
          console.log("return back ");
          return of(error);
        } else {
          console.log("throw error back to to the subscriber");
          return throwError(() => error.message);
        }
      })
    );
  }
}
