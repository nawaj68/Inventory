import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './layout/admin/admin.component';

const routes: Routes = [
  {
    path:'',
    component:AdminComponent,
    loadChildren:()=>import('./modules/admin/admin.module').then(x=>x.AdminModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
