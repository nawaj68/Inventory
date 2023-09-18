import { SelectionModel } from '@angular/cdk/collections';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatAutocomplete } from '@angular/material/autocomplete';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { distinctUntilChanged } from 'rxjs';
import { FormExtension } from 'src/app/core/form/form-extension';
import { DataPassService } from 'src/app/core/services/data-pass.service';
import { MessageService } from 'src/app/core/services/message/message.service';
import { Messages } from 'src/app/core/services/message/messages';
import { Supplier } from 'src/app/features/models/supplier.model';
import { SupplierService } from 'src/app/features/services/supplier.service';

@Component({
  selector: 'app-supplier-create-update',
  templateUrl: './supplier-create-update.component.html',
  styleUrls: ['./supplier-create-update.component.scss']
})
export class SupplierCreateUpdateComponent {
  jsonUrl = "assets/data/supplier.data.json";
  message = Messages;
  errors: any;
  formData: any;

  supplierId: number;
  supplierForm: FormGroup;
  supplierFormValue: any;
  isEdit = false;
  supplier: Supplier;
  
  @ViewChild("lableInput") labelInput: ElementRef<HTMLInputElement>;
  @ViewChild("auto") matAutocomplete: MatAutocomplete;

  tomorrow = new Date();

  displayedColumns: string[] = ["supplierName","supplierAddress","action"];
  dataSource = new MatTableDataSource<Supplier>();
  selection = new SelectionModel<Supplier>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public http: HttpClient,
    public fb: FormBuilder,
    public supplierService: SupplierService,
    private cd: ChangeDetectorRef,
    private messageService: MessageService,
    private dataPassService: DataPassService
  ) { }

  
  ngAfterViewInit(): void {
    this.load();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.supplierForm.valueChanges.pipe(distinctUntilChanged()).subscribe({
      next: (v: any) => {
        let obj = { data: this.supplierForm.getRawValue(), errors: this.errors };
        this.dataPassService.setData(obj);
      },
    });
  }
  load(): any {
    return this.http.get(this.jsonUrl).subscribe((e: any) => {
      this.dataSource = new MatTableDataSource<Supplier>(e);
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => (this.supplierId = params["id"] ? Number(params["id"]) : 0));
    this.isEdit = !!this.supplierId;
    this.supplierForm = this.form;
    this.supplierForm.patchValue({ id: this.supplierId });
    this.getsupplierDetail(this.supplierId);
  }

  getsupplierDetail(supplierId: any) {
    if (!this.supplierId) return;
    this.supplierService.getSupplierDetail(supplierId).subscribe({
      next: (res: any) => {
        if (res) {
          this.supplier = res.data;
          this.form = res.data;
        }
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  get form(): any {
    return this.fb.group({
      id: [0],
      supplierName: ["", [Validators.required, Validators.maxLength(50)]],
      supplierAddress: [""],

    });
  }

  set form(data: any) {
    if (data !== null) {
      this.supplierForm.patchValue({
        id: data.id,
        supplierName: data.supplierName,
        supplierAddress: data.supplierAddress,

      });
    }
  }

  
  save(): void {
    this.errors = FormExtension.getFormValidationErrors(this.supplierForm);
    if (this.supplierForm.invalid) {
      this.supplierForm.markAllAsTouched();
      return;
    }

    //   this.userInformationForm.patchValue({
    //     birthDate: this.userInformationForm.get('birthDate')?.value.toISOString()
    // });
    this.supplierFormValue = this.supplierForm.getRawValue();
    this.formData = FormExtension.toFormData(this.supplierForm);
    console.log("save", this.supplierFormValue);

    debugger;
    if (this.supplierFormValue.id > 0) {
      // this.userInformationService.updateUserDetailWithFile(this.submitForm.id, this.submitForm, this.picture).subscribe({
      //   next: (n: any) => console.log(n),
      //   error: (e: any) => console.log(e),
      // });

      this.supplierService.updateSupplierDetail(this.supplierFormValue.id, this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.updateSuccess);
          this.router.navigate(['supplier']);
        },
      });
    } else {
      this.supplierService.addSupplierDetail(this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.saveSucess);
          this.router.navigate(['supplier']);
        },
      });
    }
  }

  reset(): void {
    this.supplierForm.reset(this.form.value);
  }

  clearsupplier(): void {
    this.supplierForm.reset();
    this.supplierForm.markAsUntouched();
    FormExtension.markAllAsUntoched(this.supplierForm);
  }
}
