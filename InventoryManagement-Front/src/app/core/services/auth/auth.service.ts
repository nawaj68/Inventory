import { ApiRoute } from './../../../feature/configs/api-route';
import { RouteService } from './../routes/route.service';
import {MessageService} from "./../message/message.service";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {BehaviorSubject, Observable} from "rxjs";
import {StorageService} from "../storage/storage.service";
import * as moment from "moment";
import { DefaultData } from 'src/app/feature/configs/default-data';
import { FrontRoute } from 'src/app/feature/configs/front-route';

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private isAutheniticate = new BehaviorSubject(false);
  private apiRoute = ApiRoute;
  private frontRoute = FrontRoute;
  private default = DefaultData;

  constructor(
    private http: HttpClient,
    private storage: StorageService,
    private routeService: RouteService,
    private messageService: MessageService
  ) {}

  isTokenExpire() {
    let expireDt = this.storage.getItem(".expires");

    if (expireDt != null) {
      expireDt = moment.utc(expireDt).toDate();
      expireDt = new Date(moment(expireDt).local().format(this.default.dateTimeFormat));

      const currentDt = new Date(moment(new Date()).local().format(this.default.dateTimeFormat));
      return currentDt > expireDt;
    } else {
      return true;
    }
  }

  setAuthenitication(isAuth: boolean) {
    this.isAutheniticate.next(isAuth);
  }

  getAuthenitication() {
    this.setAuthenitication(!this.isTokenExpire());
    return this.isAutheniticate;
  }

  signUpInternal(data: any, selectedRoles: any[], groupPolicies: any): Observable<any> {
    data.roles = selectedRoles;
    data.groupPolicies = groupPolicies;

    return this.http.post(this.apiRoute.signupInternal, data);
  }

  signup(data: any): Observable<any> {
    return this.http.post(this.apiRoute.signup, data);
  }

  signin(username: string, password: string) {
    const data = `grant_type=password&username=${username}&password=${password}`;

    const headers = new HttpHeaders({
      "Content-Type": "application/x-www-form-urlencoded",
      "Access-Control-Allow-Origin": "*",
      "Is-External": "true",
    });

    this.http.post(this.apiRoute.token, data, {headers}).subscribe({
      next: (res: any) => {
        Object.keys(res).forEach((x) => {
          this.storage.setItem(x, res[x]);
        });
        this.setAuthenitication(true);
        this.routeService.toRoute(this.frontRoute.home);
      },
      error: (error) => {
        this.messageService.show(error.error.error_description, "error");
      },
    });
  }

  signout() {
    return this.http.post(this.apiRoute.signout, {}).subscribe(() => {
      this.signoutLocally();
    });
  }

  signoutLocally() {
    this.routeService.toRoute(this.frontRoute.login);
    this.storage.reset();
    this.setAuthenitication(false);
  }
}
