import {HttpInterceptor, HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpErrorResponse} from "@angular/common/http";

import {Observable, throwError} from "rxjs";
import {map, catchError, finalize} from "rxjs/operators";
import {Injectable} from "@angular/core";
import {NgxSpinnerService} from "ngx-spinner";
import {environment} from "src/environments/environment";
import {StorageService} from "../services/storage/storage.service";
import {AuthService} from "../services/auth/auth.service";

@Injectable()
export class HttpRequestInterceptor implements HttpInterceptor {
  loaderActive = false;
  endPointUrl: string;
  anonymousUrls = ["token", "signup", "opt-request", "reset-password", "public"];

  constructor(private spinner: NgxSpinnerService, private storageService: StorageService, private authService: AuthService) {
    this.endPointUrl = environment.baseUrl;
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.loaderActive == false) {
      this.spinner.show();
      this.loaderActive = true;
    }

    // const accessToken = this.storageService.getItem("access_token");
    // const tokenType = this.storageService.getItem("token_type");
    // if (accessToken) {
    //   const authToken = `${tokenType} ${accessToken}`;
    //   request = request.clone({headers: request.headers.set("Authorization", authToken)});
    // } else {
    //   this.authService.signoutLocally();
    // }

    return next.handle(request).pipe(
      map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          this.loaderActive = true;
        }
        return event;
      }),
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.authService.signoutLocally();
        }
        return throwError(() => error);
      }),
      finalize(() => {
        setTimeout(() => {
          this.spinner.hide();
        });
        this.loaderActive = false;
      })
    );
  }
}
