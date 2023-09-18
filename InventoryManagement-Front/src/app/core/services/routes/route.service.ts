import { Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RouteService {

  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  toRoute = (routeLink: string) => this.router.navigate([routeLink]);
}
