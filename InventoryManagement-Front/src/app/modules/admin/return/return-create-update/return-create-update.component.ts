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
import { Company } from 'src/app/features/models/company.model';
import { Item } from 'src/app/features/models/item.model';
import { Return } from 'src/app/features/models/return.model';
import { CompanyService } from 'src/app/features/services/company.service';
import { ItemService } from 'src/app/features/services/item.service';
import { ReturnService } from 'src/app/features/services/return.service';

@Component({
  selector: 'app-return-create-update',
  templateUrl: './return-create-update.component.html',
  styleUrls: ['./return-create-update.component.scss']
})
export class ReturnCreateUpdateComponent implements OnInit {
  jsonUrl = "assets/data/return.data.json";

  message = Messages;
  errors: any;
  formData: any;

  userId: number;
  returnId: number;
  returnForm: FormGroup;
  returnFormValue: any;
  isEdit = false;
  returns: Return;
  itemId: number;
  companyId: number;
  items: Item[];
  filteredItems: Item[];
  company: Company[];
  filteredCompany: Company[];

  filteredLabels: Observable<string[]>;

  relatedPosts = [];
  status: [];
  today = new Date();
  
  stateCtrl = new FormControl();
  private itemSubject: Subject<string> = new Subject<string>();
  private companySubject: Subject<string> = new Subject<string>();

  @ViewChild("lableInput") labelInput: ElementRef<HTMLInputElement>;
  @ViewChild("auto") matAutocomplete: MatAutocomplete;

  tomorrow = new Date();

  displayedColumns: string[] = ["itemId","quantity","reasonOfReturn","returnDate","action"];
  dataSource = new MatTableDataSource<Return>();
  selection = new SelectionModel<Return>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public http: HttpClient,
    public fb: FormBuilder,
    public returnService: ReturnService,
    public itemsService: ItemService,
    public companyService: CompanyService,
    private fileHandler: FileHandler,
    private cd: ChangeDetectorRef,
    private messageService: MessageService,
    private dataPassService: DataPassService
  ) {}

  ngAfterViewInit(): void {
    this.load();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.returnForm.valueChanges.pipe(distinctUntilChanged()).subscribe({
      next: (v: any) => {
        let obj = {data: this.returnForm.getRawValue(), errors: this.errors};
        this.dataPassService.setData(obj);
      },
    });
  }
  load(): any {
    return this.http.get(this.jsonUrl).subscribe((e: any) => {
      this.dataSource = new MatTableDataSource<Return>(e);
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => (this.returnId = params["id"] ? Number(params["id"]) : 0));
    this.isEdit = !!this.returnId;
    this.returnForm = this.form;
    this.returnForm.patchValue({id: this.returnId});
    this.getReturnDetail(this.returnId);
    this.getItem("");
    this.getCompany("");
    this.itemSubject.pipe(debounceTime(1000)).subscribe({next:(v:string)=> this.getItem(v)});
    this.companySubject.pipe(debounceTime(1000)).subscribe({next:(v:string)=>this.getCompany(v)});
   }

  getReturnDetail(returnId: any) {
    if (!this.returnId) return;
    this.returnService.getReturnDetail(returnId).subscribe({
      next: (res: any) => {
        if (res) {
          this.returns= res.data;
          this.form = res.data;
        }
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }
  onItemFilter($event:any){
    const value = $event.target.value.toLocaleLowerCase().toString();
    this.itemSubject.next(value);
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
  onCompanyFilter($event:any){
    const value = $event.target.value.toLocaleLowerCase().toString();
    this.companySubject.next(value);
  }

  getCompany(searchText: string) {
    this.companyService.getDropdown(0, 10, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.company = res.data.data;
          this.filteredCompany = res.data.data;
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
      userId: [1],
      companyId:  ["", [Validators.required]],
      itemId:  ["", [Validators.required]],
      quantity: ["", Validators.required],
      reasonOfReturn: ["", [Validators.required]],
      returnDate: ["", [Validators.required]],
      
    });
  }

  set form(user: any) {
    if (user !== null) {
      this.returnForm.patchValue({
        id: user.id,
        userId: 1,//user.userId,
        companyId: user.companyId,
        itemId: user.itemId,
        quantity: user.quantity,
        reasonOfReturn: user.reasonOfReturn,
        returnDate: user.returnDate,
       
      });

    }
  }

  dateChange(type: string, event: MatDatepickerInputEvent<Date>) {
    console.log(`${type}: ${event.value}`);
  }
 
  save(): void {
    this.errors = FormExtension.getFormValidationErrors(this.returnForm);
    if (this.returnForm.invalid) {
      this.returnForm.markAllAsTouched();
      return;
    }
    
    this.returnFormValue = this.returnForm.getRawValue();
    this.formData = FormExtension.toFormData(this.returnForm);
    console.log("save", this.returnForm);

    if (this.returnFormValue.id > 0) {

      this.returnService.updateReturnDetail(this.returnFormValue.id, this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.updateSuccess);
          this.router.navigate(['/return']);
        },
      });
    } else {
      this.returnService.addReturnDetail(this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.saveSucess);
          //this.reset();
          this.router.navigate(['/return']);
        },
      });
    }
  }

  reset(): void {
    this.returnForm.reset(this.form.value);
  }

  clearState(): void {
    this.returnForm.reset();
    this.returnForm.markAsUntouched();
    FormExtension.markAllAsUntoched(this.returnForm);
  }
}


