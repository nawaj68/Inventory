import { AvatarPipe } from './pipe/avatar.pipe';
import {ErrorHandler, NgModule} from "@angular/core";

import {CommonModule} from "@angular/common";
import {MyErrorHandler} from "./error.handler";
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import {HttpErrorInterceptor} from "./interceptors/http-error.interceptor";
import { GenderPipe } from './pipe/gender.pipe';

@NgModule({
  imports: [CommonModule],
  declarations: [AvatarPipe,GenderPipe],
  
  exports: [AvatarPipe,GenderPipe],
 

  providers: [
    // {provide: ErrorHandler, useClass: MyErrorHandler},
    // {provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true},
  ],
})

export class CoreModule {}
