import { SelectionModel } from '@angular/cdk/collections';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatAutocomplete } from '@angular/material/autocomplete';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, Observable, Subject } from 'rxjs';
import { FormExtension } from 'src/app/core/form/form-extension';
import { DataPassService } from 'src/app/core/services/data-pass.service';
import { FileHandler } from 'src/app/core/services/file/file.handler';
import { MessageService } from 'src/app/core/services/message/message.service';
import { Messages } from 'src/app/core/services/message/messages';
import { Item } from 'src/app/features/models/item.model';
import { PurchaseDetails } from 'src/app/features/models/purchaseDetails.model';
import { PurchasesMaster } from 'src/app/features/models/purchasesMaster.model';
import { ItemService } from 'src/app/features/services/item.service';
import { PurchaseDetailsService } from 'src/app/features/services/purchase-details.service';
import { PurchasesMasterService } from 'src/app/features/services/purchases-master.service';

@Component({
  selector: 'app-purchase-details-create-update',
  templateUrl: './purchase-details-create-update.component.html',
  styleUrls: ['./purchase-details-create-update.component.scss']
})
export class PurchaseDetailsCreateUpdateComponent implements OnInit {
  jsonUrl = "assets/data/purchaseDetails.data.json";

  
  message = Messages;
  errors: any;
  formData: any;

  userId: number;
  purchaseDetailsId: number;
  purchaseDetailsForm: FormGroup;
  purchaseDetailsFormValue: any;
  isEdit = false;
  purchaseDetails: PurchaseDetails;
  itemId: number;
  purchasesMasterId: number;
  items: Item[];
  filteredItems: Item[];
  purchasesMaster: PurchasesMaster[];
  filteredPurchasesMaster: PurchasesMaster[];

  filteredLabels: Observable<string[]>;

  relatedPosts = [];
  status: [];
  today = new Date();
  
  stateCtrl = new FormControl();
  private itemSubject: Subject<string> = new Subject<string>();
  private purchasesMasterSubject: Subject<string> = new Subject<string>();

  @ViewChild("lableInput") labelInput: ElementRef<HTMLInputElement>;
  @ViewChild("auto") matAutocomplete: MatAutocomplete;

  tomorrow = new Date();

  displayedColumns: string[] = ["itemId","quantity","unitePrice","sellPrice","action"];
  dataSource = new MatTableDataSource<PurchaseDetails>();
  selection = new SelectionModel<PurchaseDetails>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public http: HttpClient,
    public fb: FormBuilder,
    public purchaseDetailsService: PurchaseDetailsService,
    public itemsService: ItemService,
    public purchasesMasterService: PurchasesMasterService,
    private fileHandler: FileHandler,
    private cd: ChangeDetectorRef,
    private messageService: MessageService,
    private dataPassService: DataPassService
  ) {}

  ngAfterViewInit(): void {
    this.load();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.purchaseDetailsForm.valueChanges.pipe(distinctUntilChanged()).subscribe({
      next: (v: any) => {
        let obj = {data: this.purchaseDetailsForm.getRawValue(), errors: this.errors};
        this.dataPassService.setData(obj);
      },
    });
  }
  load(): any {
    return this.http.get(this.jsonUrl).subscribe((e: any) => {
      this.dataSource = new MatTableDataSource<PurchaseDetails>(e);
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => (this.purchaseDetailsId = params["id"] ? Number(params["id"]) : 0));
    this.isEdit = !!this.purchaseDetailsId;
    this.purchaseDetailsForm = this.form;
    this.purchaseDetailsForm.patchValue({id: this.purchaseDetailsId});
    this.getPurchaseDetailsDetail(this.purchaseDetailsId);
    this.getItem("");
    this.getPurchasesMaster("");
    this.itemSubject.pipe(debounceTime(1000)).subscribe({next:(v:string)=> this.getItem(v)});
    this.purchasesMasterSubject.pipe(debounceTime(1000)).subscribe({next:(v:string)=>this.getPurchasesMaster(v)});
   }

  getPurchaseDetailsDetail(purchaseDetailsId: any) {
    if (!this.purchaseDetailsId) return;
    this.purchaseDetailsService.getPurchaseDetailsDetail(purchaseDetailsId).subscribe({
      next: (res: any) => {
        if (res) {
          this.purchaseDetails= res.data;
          this.form = res.data;
        }
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }
 

  getItem(searchText: string) {
    this.itemsService.getDropdown(0, 10, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.items = res.data.data;
          this.filteredItems = res.data.data;
        }
      },
      // error: (error: any) => {
      //   console.error(error);
      // },
    });
  }
   
  onItemFilter($event:any){
    const value = $event.target.value.toLocaleLowerCase().toString();
    this.itemSubject.next(value);
  }

  getPurchasesMaster(searchText: string) {
    this.purchasesMasterService.getDropdown(0, 10, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.purchasesMaster = res.data.data;
          this.filteredPurchasesMaster = res.data.data;
        }
      },
      // error: (error: any) => {
      //   console.error(error);
      // },
    });
  }
  onPurchasesMasterFilter($event:any){
    const value = $event.target.value.toLocaleLowerCase().toString();
    this.purchasesMasterSubject.next(value);
  }

  get form(): any {
    return this.fb.group({
      id: [0], 
      userId: [1],
      purchasesMasterId: ["", Validators.required],
      itemId: ["", Validators.required],
      quantity: ["", Validators.required],
      unitePrice: ["", [Validators.required]],
      sellPrice: ["", [Validators.required]],
      batchNumber: ["", [Validators.required]],
    });
  }

  set form(user: any) {
    if (user !== null) {
      this.purchaseDetailsForm.patchValue({
        id: user.id,
        userId: 1,//user.userId,
        purchasesMasterId: user.purchasesMasterId,
        itemId: user.itemId,
        quantity: user.quantity,
        unitePrice: user.unitePrice,
        sellPrice: user.sellPrice,
        batchNumber: user.batchNumber,
      });

    }
  }

  dateChange(type: string, event: MatDatepickerInputEvent<Date>) {
    console.log(`${type}: ${event.value}`);
  }
 
  save(): void {
    this.errors = FormExtension.getFormValidationErrors(this.purchaseDetailsForm);
    if (this.purchaseDetailsForm.invalid) {
      this.purchaseDetailsForm.markAllAsTouched();
      return;
    }
    
    this.purchaseDetailsFormValue = this.purchaseDetailsForm.getRawValue();
    this.formData = FormExtension.toFormData(this.purchaseDetailsForm);
    console.log("save", this.purchaseDetailsForm);

    if (this.purchaseDetailsFormValue.id > 0) {

      this.purchaseDetailsService.updatePurchaseDetailsDetail(this.purchaseDetailsFormValue.id, this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.updateSuccess);
          this.router.navigate(['/purchaseDetails']);
        },
      });
    } else {
      this.purchaseDetailsService.addPurchaseDetailsDetail(this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.saveSucess);
          //this.reset();
          this.router.navigate(['/purchaseDetails']);
        },
      });
    }
  }

  reset(): void {
    this.purchaseDetailsForm.reset(this.form.value);
  }

  clearState(): void {
    this.purchaseDetailsForm.reset();
    this.purchaseDetailsForm.markAsUntouched();
    FormExtension.markAllAsUntoched(this.purchaseDetailsForm);
  }
}

