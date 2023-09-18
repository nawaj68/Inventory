import { SelectionModel } from '@angular/cdk/collections';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatAutocomplete } from '@angular/material/autocomplete';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';
import { FormExtension } from 'src/app/core/form/form-extension';
import { DataPassService } from 'src/app/core/services/data-pass.service';
import { FileHandler } from 'src/app/core/services/file/file.handler';
import { MessageService } from 'src/app/core/services/message/message.service';
import { Messages } from 'src/app/core/services/message/messages';
import { Company } from 'src/app/features/models/company.model';
import { Item } from 'src/app/features/models/item.model';
import { SubCategory } from 'src/app/features/models/subCategory.model';
import { Unit } from 'src/app/features/models/unit.model';
import { CompanyService } from 'src/app/features/services/company.service';
import { ItemService } from 'src/app/features/services/item.service';
import { SubCategoryService } from 'src/app/features/services/sub-category.service';
import { UnitService } from 'src/app/features/services/unit.service';



@Component({
  selector: 'app-item-create-update',
  templateUrl: './item-create-update.component.html',
  styleUrls: ['./item-create-update.component.scss']
})
export class ItemCreateUpdateComponent implements OnInit {
  jsonUrl = "assets/data/item.data.json";

  message = Messages;
  errors: any;
  formData: any;
  
  isEdit = false;

  item: Item;

 
  itemId: number;
  itemForm: FormGroup;
  itemFormValue: any;

  companyId: number;
  companies: Company[];
  filteredCompanies: Company[];

  unitId: number;
  units: Unit[];
  filteredUnits: Unit[];

  subCategoryId: number;
  subCategories: SubCategory[];
  filteredSubCategories: SubCategory[];

  private companySubject: Subject<string> = new Subject<string>();
  private unitSubject: Subject<string> = new Subject<string>();
  private subCategorySubject: Subject<string> = new Subject<string>();

  @ViewChild("lableInput") labelInput: ElementRef<HTMLInputElement>;
  @ViewChild("auto") matAutocomplete: MatAutocomplete;

  displayedColumns: string[] = ["name", "company", "action"];
  dataSource = new MatTableDataSource<Item>();
  selection = new SelectionModel<Item>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public http: HttpClient,
    public fb: FormBuilder,
    public itemService: ItemService,
    public companyService: CompanyService,
    public unitService:UnitService,
    public subCategoryService:SubCategoryService,
    private fileHandler: FileHandler,
    private cd: ChangeDetectorRef,
    private messageService: MessageService,
    private dataPassService: DataPassService
  ) { }

  ngAfterViewInit(): void {
    this.load();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.itemForm.valueChanges.pipe(distinctUntilChanged()).subscribe({
      next: (v: any) => {
        let obj = { data: this.itemForm.getRawValue(), errors: this.errors };
        this.dataPassService.setData(obj);
      },
    });
  }

  load(): any {
    return this.http.get(this.jsonUrl).subscribe((e: any) => {
      this.dataSource = new MatTableDataSource<Item>(e);
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => (this.itemId = params["id"] ? Number(params["id"]) : 0));
    this.isEdit = !!this.itemId;
    this.itemForm = this.form;
    this.itemForm.patchValue({ id: this.itemId });
    this.getItemDetail(this.itemId);
    this.getCompany("");
    this.getUnit("");
    this.getSubCategory("");
    this.companySubject.pipe(debounceTime(1000)).subscribe({ next: (v: string) => this.getCompany(v) });
    this.unitSubject.pipe(debounceTime(1000)).subscribe({ next: (v: string) => this.getUnit(v) });
    this.subCategorySubject.pipe(debounceTime(1000)).subscribe({ next: (v: string) => this.getSubCategory(v) });

  }

  getItemDetail(itemId: any) {
    if (!this.itemId) return;
    this.itemService.getItemDetail(itemId).subscribe({
      next: (res: any) => {
        if (res) {
          this.item = res.data;
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
          this.companies = res.data.data;
          this.filteredCompanies = res.data.data;
        }
      },
      // error: (error: any) => {
      //   console.error(error);
      // },
    });
  }
  onCompanyFilter($event: any) {
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.companySubject.next(value);
  }

  getUnit(searchText: string) {
    this.unitService.getDropdown(0, 10, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.units = res.data.data;
          this.filteredUnits = res.data.data;
        }
      },
      // error: (error: any) => {
      //   console.error(error);
      // },
    });
  }
  onUnitFilter($event: any) {
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.unitSubject.next(value);
  }

  getSubCategory(searchText: string) {
    this.subCategoryService.getDropdown(0, 10, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.subCategories = res.data.data;
          this.filteredSubCategories = res.data.data;
        }
      },
      // error: (error: any) => {
      //   console.error(error);
      // },
    });
  }
  onSubCategoryFilter($event: any) {
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.subCategorySubject.next(value);
  }

  get form(): any {
    return this.fb.group({
      id: [0],
      subCategoryId:[0],
      companyId: [0],
      unitId:[0],
      itemCode:["", [Validators.required]],
      itemName:["", [Validators.required, Validators.maxLength(50)]],
      description: [""],
      measure:[""],
      measurevalue:[0],
      unitPrice:[0, [Validators.required]],
      sellPrice:[0, [Validators.required]],
      oldUnitPrice:[0],
      oldSellPrice:[0],
      reOrderLevel:[""],
      stock:[0],
    });
  }

  set form(data: any) {
    if (data !== null) {
      this.itemForm.patchValue({
        id: data.id,
        subCategoryId: data.subCategoryId,
        companyId: data.companyId,
        unitId: data.unitId,
        itemCode: data.itemCode,
        itemName: data.itemName,
        description: data.description,
        measure: data.measure,
        measurevalue: data.measurevalue,
        unitPrice: data.unitPrice,
        sellPrice: data.sellPrice,
        oldUnitPrice: data.oldUnitPrice,
        oldSellPrice: data.oldSellPrice,
        reOrderLevel: data.reOrderLevel,
        stock: data.stock
      });
    }
  }


  save(): void {
    this.errors = FormExtension.getFormValidationErrors(this.itemForm);
    if (this.itemForm.invalid) {
      this.itemForm.markAllAsTouched();
      return;
    }

    //   this.userInformationForm.patchValue({
    //     birthDate: this.userInformationForm.get('birthDate')?.value.toISOString()
    // });
    this.itemFormValue = this.itemForm.getRawValue();
    this.formData = FormExtension.toFormData(this.itemForm);
    console.log("save", this.itemFormValue);

    
    if (this.itemFormValue.id > 0) {
      // this.userInformationService.updateUserDetailWithFile(this.submitForm.id, this.submitForm, this.avatar).subscribe({
      //   next: (n: any) => console.log(n),
      //   error: (e: any) => console.log(e),
      // });

      this.itemService.updateItemDetail(this.itemFormValue.id, this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.updateSuccess);
          this.router.navigate(['item']);
        },
      });
    } else {
      this.itemService.addItemDetail(this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.saveSucess);
          this.router.navigate(['item']);
        },
      });
    }
  }

  reset(): void {
    this.itemForm.reset(this.form.value);
  }

  clearItem(): void {
    this.itemForm.reset();
    this.itemForm.markAsUntouched();
    FormExtension.markAllAsUntoched(this.itemForm);
  }

}