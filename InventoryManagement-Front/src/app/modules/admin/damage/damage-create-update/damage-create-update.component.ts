import { SelectionModel } from '@angular/cdk/collections';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatAutocomplete } from '@angular/material/autocomplete';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, Subject } from 'rxjs';
import { FormExtension } from 'src/app/core/form/form-extension';
import { DataPassService } from 'src/app/core/services/data-pass.service';
import { FileHandler } from 'src/app/core/services/file/file.handler';
import { MessageService } from 'src/app/core/services/message/message.service';
import { Messages } from 'src/app/core/services/message/messages';
import { Company } from 'src/app/features/models/company.model';
import { Damage } from 'src/app/features/models/damage.model';
import { Item } from 'src/app/features/models/item.model';
import { CompanyService } from 'src/app/features/services/company.service';
import { DamageService } from 'src/app/features/services/damage.service';
import { ItemService } from 'src/app/features/services/item.service';

@Component({
  selector: 'app-damage-create-update',
  templateUrl: './damage-create-update.component.html',
  styleUrls: ['./damage-create-update.component.scss']
})
export class DamageCreateUpdateComponent  implements OnInit{

  message = Messages;
  errors: any;
  formData: any;

  damageId: number;
  damageForm: FormGroup;
  damageFormValue: any;
  isEdit = false;
  // quantity: any;
  // damageQuantity: any;
  // damageReason:any;
  // damageDate:any;
  damage: Damage;

  items:Item[];
  filtereditems:Item[];

  companys:Company[];
  filteredcompanys:Company[];

  private itemSubjet: Subject<string> = new Subject<string>();
  private companySubjet: Subject<string> = new Subject<string>();

  @ViewChild("lableInput") labelInput: ElementRef<HTMLInputElement>;
  @ViewChild("auto") matAutocomplete: MatAutocomplete;

  today = new Date();

  displayedColumns: string[] = ["quantity", "damageQuantity", "damageReason", "item", "company", "action"];

  dataSource = new MatTableDataSource<Damage>();
  selection = new SelectionModel<Damage>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public http: HttpClient,
    public fb: FormBuilder,
    public damageService: DamageService,
    public itemService:ItemService,
    public companyService: CompanyService,
    private fileHandler: FileHandler,
    private cd: ChangeDetectorRef,
    private messageService: MessageService,
    private dataPassService: DataPassService
  ) { }


  ngOnInit(): void {
    this.route.params.subscribe((params) => (this.damageId = params["id"] ? Number(params["id"]) : 0));
    this.isEdit = !!this.damageId;
    this.damageForm = this.form;
    this.damageForm.patchValue({ id: this.damageId });
    this.getDamageDetail(this.damageId);
    this.getItems("");
    this.getCompany("");
    this.itemSubjet.pipe(debounceTime(1000)).subscribe({ next: (v: string) => this.getItems(v) });
    this.companySubjet.pipe(debounceTime(1000)).subscribe({ next: (v: string) => this.getCompany(v) });
  }

  getDamageDetail(damageId: any) {
    if (!this.damageId) return;
    this.damageService.getDamageDetail(damageId).subscribe({
      next: (res: any) => {
        if (res) {
          this.damage = res.data;
          this.form = res.data;
        }
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  getItems(searchText: string){
    this.itemService.getDropdown(0, 10, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.items = res.data.data;
          this.filtereditems = res.data.data;
        }
      },
      // error: (error: any) => {
      //   console.error(error);
      // },
    });

  }
  onItemFilter($event: any) {
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.itemSubjet.next(value);
  }

  getCompany(searchText: string){
    this.companyService.getDropdown(0, 10, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.companys = res.data.data;
          this.filteredcompanys = res.data.data;
        }
      },
      // error: (error: any) => {
      //   console.error(error);
      // },
    });
  }
  onCompanyFilter($event: any) {
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.companySubjet.next(value);
  }

  get form(): any {
    return this.fb.group({
      id: [0],
      itemId: [],
      companyId: [],
      quantity: ["", [Validators.required]],
      damageQuantity: ["", Validators.required],
      damageReason: ["", Validators.required],
      damageDate: ["", Validators.required]

    });
  }

  set form(data: any) {
    if (data !== null) {
      this.damageForm.patchValue({
        id: data.id,
        itemId: data.itemId,
        companyId: data.companyId,
        quantity: data.quantity,
        damageQuantity: data.damageQuantity,
        damageReason: data.damageReason,
        damageDate: data.damageDate

      });

      // this.avaterPreview = user.avatar ? `${environment.baseUrl}/${user.avatar}` : "";
      // this.avatar = user.avatar;
      // console.log("path avater", this.avatar);
      console.log("path", data, this.damageForm.getRawValue());
    }
  }
  dateChange(type: string, event: MatDatepickerInputEvent<Date>) {
    console.log(`${type}: ${event.value}`);
  }

  save(): void {
    this.errors = FormExtension.getFormValidationErrors(this.damageForm);
    if (this.damageForm.invalid) {
      this.damageForm.markAllAsTouched();
      return;
    }

    //   this.userInformationForm.patchValue({
    //     birthDate: this.userInformationForm.get('birthDate')?.value.toISOString()
    // });
    this.damageFormValue = this.damageForm.getRawValue();
    // console.log("save avater", this.avatar);
    // this.awardInfoForm.patchValue({ avatar: this.avatar });
    this.formData = FormExtension.toFormData(this.damageForm);
    console.log("save", this.damageFormValue);

    debugger;
    if (this.damageFormValue.id > 0) {
      // this.userInformationService.updateUserDetailWithFile(this.submitForm.id, this.submitForm, this.avatar).subscribe({
      //   next: (n: any) => console.log(n),
      //   error: (e: any) => console.log(e),
      // });

      this.damageService.updateDamageDetail(this.damageFormValue.id, this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.updateSuccess);
          this.router.navigate(['damage']);
        },
      });
    } else {
      this.damageService.addDamageDetail(this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.saveSucess);
          this.router.navigate(['damage']);
        },
      });
    }
  }

  reset(): void {
    this.damageForm.reset(this.form.value);
  }

  clearDamage(): void {
    this.damageForm.reset();
    this.damageForm.markAsUntouched();
    FormExtension.markAllAsUntoched(this.damageForm);
  }

  // avatarUpload($event: any): void {
  //   const files = $event.target.files;
  //   console.log(files);
  //   this.awardInfoForm.patchValue({
  //     avatarFile: files[0],
  //   });
  //   this.awardInfoForm.get("image")?.updateValueAndValidity();
  //   this.cd.markForCheck();
  //   console.log(this.awardInfoForm);
  //   this.uploader.setOptions({
  //     url: this.uploadUrl,
  //   });

  //   if (files.length === 0 || files == null) {
  //   }
  //   const file = files[0];

  //   if (file.size === 0) {
  //   }

  //   if (!this.fileHandler.fileValidate(file)) {
  //     this.uploader.clearQueue();
  //     return;
  //   }
  // }

  

}
