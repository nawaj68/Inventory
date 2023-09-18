import { Component } from '@angular/core';
import { inventory, navs, salesInvoice, settings } from 'src/app/features/configs/route.config';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent {
  navs = navs.filter((e) => e.component !== undefined && e.menu);
  settings = settings.filter((e) => e.component !== undefined && e.menu);
  salesInvoice = salesInvoice.filter((e) => e.component !== undefined && e.menu);
  inventory = inventory.filter((e) => e.component !== undefined && e.menu);
}
