import { ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Messages } from 'src/app/core/services/message/messages';
import { PurchasesMaster } from './../../../../features/models/purchasesMaster.model';
import { Supplier } from 'src/app/features/models/supplier.model';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';
import { MatAutocomplete } from '@angular/material/autocomplete';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { SupplierService } from 'src/app/features/services/supplier.service';
import { MessageService } from 'src/app/core/services/message/message.service';
import { DataPassService } from 'src/app/core/services/data-pass.service';
import { FormExtension } from 'src/app/core/form/form-extension';
import { CompanyService } from 'src/app/features/services/company.service';
import { PurchasesMasterService } from 'src/app/features/services/purchases-master.service';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { Company } from 'src/app/features/models/company.model';

@Component({
  selector: 'app-purchases-master-create-update',
  templateUrl: './purchases-master-create-update.component.html',
  styleUrls: ['./purchases-master-create-update.component.scss']
})
export class PurchasesMasterCreateUpdateComponent {
  jsonUrl = "assets/data/purchasesMaster.data.json";
  message = Messages;
  errors: any;
  formData: any;
  userId: number;
  purchasesmasterId: number;
  purchasesmasterForm: FormGroup;
  purchasesmasterFormValue: any;
  isEdit = false;
  purchasesmaster: PurchasesMaster;
  countryId: number;
  stateId: number;
  cityId: number;
  countries: PurchasesMaster[];
  supplier:Supplier[];
  filteredCompany: Company[];
  filteredSupplier: Supplier[];

  today = new Date();

  private companySubject: Subject<string> = new Subject<string>();
  private supplierSubject: Subject<string> = new Subject<string>();

  @ViewChild("lableInput") labelInput: ElementRef<HTMLInputElement>;
  @ViewChild("auto") matAutocomplete: MatAutocomplete;

  tomorrow = new Date();

  displayedColumns: string[] =["purchasesType","purchasesCode","warrenty","supplier","company","action"];
  dataSource = new MatTableDataSource<PurchasesMaster>();
  selection = new SelectionModel<PurchasesMaster>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public http: HttpClient,
    public fb: FormBuilder,
    public purchasesmasterService: PurchasesMasterService,
    public supplierService: SupplierService,
    public companyService: CompanyService,
    private cd: ChangeDetectorRef,
    private messageService: MessageService,
    private dataPassService: DataPassService
  ) { }

  
  ngAfterViewInit(): void {
    this.load();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.purchasesmasterForm.valueChanges.pipe(distinctUntilChanged()).subscribe({
      next: (v: any) => {
        let obj = { data: this.purchasesmasterForm.getRawValue(), errors: this.errors };
        this.dataPassService.setData(obj);
      },
    });
  }
  load(): any {
    return this.http.get(this.jsonUrl).subscribe((e: any) => {
      this.dataSource = new MatTableDataSource<PurchasesMaster>(e);
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => (this.purchasesmasterId = params["id"] ? Number(params["id"]) : 0));
    this.isEdit = !!this.purchasesmasterId;
    this.purchasesmasterForm = this.form;
    this.purchasesmasterForm.patchValue({ id: this.purchasesmasterId });
    this.getPurchasesmasterDetail(this.purchasesmasterId);
    this.getSupplier("");
    this.getCompany("");
    this.companySubject.pipe(debounceTime(1000)).subscribe({next: (v: string) => this.getCompany(v)});
    this.supplierSubject.pipe(debounceTime(1000)).subscribe({next: (v: string) => this.getSupplier(v)});

  }

  getPurchasesmasterDetail(purchasesmasterId: any) {
    if (!this.purchasesmasterId) return;
    this.purchasesmasterService.getPurchasesMasterDetail(purchasesmasterId).subscribe({
      next: (res: any) => {
        if (res) {
          this.purchasesmaster = res.data;
          this.form = res.data;
        }
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }
  getCompany(searchText: string) {
    this.companyService.getDropdown(0, 10, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.countries = res.data.data;
          this.filteredCompany = res.data.data;
        }
      },
      // error: (error: any) => {
      //   console.error(error);
      // },
    });
  }
  getSupplier(searchText: string) {
    this.supplierService.getDropdown(0, 10, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.countries = res.data.data;
          this.filteredSupplier = res.data.data;
        }
      },
      // error: (error: any) => {
      //   console.error(error);
      // },
    });
  }


  get form(): any {
    return this.fb.group({
      id: [0],
      userId:[2],
      purchasesCode: ["", [Validators.required, Validators.maxLength(50)]],
      purchasesDate: [""],
      companyId:[null],
      supplierId:[null],
      purchasesType:[""],
      warrenty:[""],
      attn:[""],
      lcNumber:[""],
      lcDate:[""],
      poNumber:[""],
      remarks:[""],
      discountAmount:[""],
      discountPercent:[""],
      vatAmount:[""],
      vatPercent:[""],
      paymentType:[""],
      cancle:[""]


    });
  }

  set form(data: any) {
    if (data !== null) {
      this.purchasesmasterForm.patchValue({
        id: data.id,
        userId: data.userId,
        purchasesCode: data.purchasesCode,
        purchasesDate: data.purchasesDate,
        companyId: data.companyId,
        supplierId: data.supplierId,
        purchasesType: data.purchasesType,
        warrenty: data.warrenty,
        attn: data.attn,
        lcNumber: data.lcNumber,
        lcDate: data.lcDate,
        poNumber: data.poNumber,
        remarks: data.remarks,
        discountAmount: data.discountAmount,
        discountPercent: data.discountPercent,
        vatAmount: data.vatAmount,
        vatPercent: data.vatPercent,
        paymentType: data.paymentType,
        cancle: data.cancle,
      });
    }
  }
  dateChange(type: string, event: MatDatepickerInputEvent<Date>) {
    console.log(`${type}: ${event.value}`);
  }
  onCompanyFilter($event: any) {
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.companySubject.next(value);
  }

  onSupplierFilter($event: any) {
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.supplierSubject.next(value);
  }


  
  save(): void {
    this.errors = FormExtension.getFormValidationErrors(this.purchasesmasterForm);
    if (this.purchasesmasterForm.invalid) {
      this.purchasesmasterForm.markAllAsTouched();
      return;
    }

    //   this.userInformationForm.patchValue({
    //     birthDate: this.userInformationForm.get('birthDate')?.value.toISOString()
    // });
    this.purchasesmasterFormValue = this.purchasesmasterForm.getRawValue();
    this.formData = FormExtension.toFormData(this.purchasesmasterForm);
    console.log("save", this.purchasesmasterFormValue);

    debugger;
    if (this.purchasesmasterFormValue.id > 0) {
      // this.userInformationService.updateUserDetailWithFile(this.submitForm.id, this.submitForm, this.picture).subscribe({
      //   next: (n: any) => console.log(n),
      //   error: (e: any) => console.log(e),
      // });

      this.purchasesmasterService.updatePurchasesMasterDetail(this.purchasesmasterFormValue.id, this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.updateSuccess);
          this.router.navigate(['purchasesMaster']);
        },
      });
    } else {
      this.purchasesmasterService.addPurchasesMasterDetail(this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.saveSucess);
          this.router.navigate(['purchasesMaster']);
        },
      });
    }
  }

  reset(): void {
    this.purchasesmasterForm.reset(this.form.value);
  }

  clearpurchasesmaster(): void {
    this.purchasesmasterForm.reset();
    this.purchasesmasterForm.markAsUntouched();
    FormExtension.markAllAsUntoched(this.purchasesmasterForm);
  }

}
