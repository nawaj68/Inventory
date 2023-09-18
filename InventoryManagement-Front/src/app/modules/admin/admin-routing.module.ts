import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { inventory, navs, salesInvoice, settings } from 'src/app/features/configs/route.config';

const routes: Routes = [];

navs.forEach((element) => {
  routes.push({
    path: element.path,
    component: element.component,
    redirectTo: element.redirectTo,
    pathMatch: element.pathMatch,
    data: element.data,
    //children: element.children,
  });
});

settings.forEach((element) => {
  routes.push({
    path: element.path,
    component: element.component,

  });
});

salesInvoice.forEach((element) => {
  routes.push({
    path: element.path,
    component: element.component,
  });
});

inventory.forEach((element) => {
  routes.push({
    path: element.path,
    component: element.component,
  });
});

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
