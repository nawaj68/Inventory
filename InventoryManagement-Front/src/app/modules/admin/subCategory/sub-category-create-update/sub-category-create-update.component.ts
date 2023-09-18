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
import { debounceTime, distinctUntilChanged, Observable, Subject, switchMap } from "rxjs";
import { FileUploader } from 'ng2-file-upload';
import { FormExtension } from 'src/app/core/form/form-extension';
import { DataPassService } from 'src/app/core/services/data-pass.service';
import { FileHandler } from 'src/app/core/services/file/file.handler';
import { MessageService } from 'src/app/core/services/message/message.service';
import { Messages } from 'src/app/core/services/message/messages';
import { environment } from 'src/environments/environment';
import { SubCategory } from 'src/app/features/models/subCategory.model';
import { SubCategoryService } from 'src/app/features/services/sub-category.service';
import { Category } from 'src/app/features/models/category.model';
import { CategoryService } from 'src/app/features/services/category.service';


@Component({
  selector: 'app-sub-category-create-update',
  templateUrl: './sub-category-create-update.component.html',
  styleUrls: ['./sub-category-create-update.component.scss']
})
export class SubCategoryCreateUpdateComponent  implements OnInit {

  jsonUrl = "assets/data/subCategory.data.json";
  message = Messages;
  errors: any;
  formData: any;

  subCategoryId: number;
  subCategoryForm: FormGroup;
  subCategoryFormValue: any;
  isEdit = false;
  picture: any;
  avaterPreview: any;
  subCategory: SubCategory;
  
  public uploader: FileUploader;
  uploadUrl: string;

  categoryId: number;
  categories: Category;
  filterCategories:Category[];

  categorySubject : Subject<string> = new Subject<string>();



  @ViewChild("lableInput") labelInput: ElementRef<HTMLInputElement>;
  @ViewChild("auto") matAutocomplete: MatAutocomplete;

  tomorrow = new Date();

  displayedColumns: string[] = ["picture", "name", "phone", "email", "genderId", "action"];
  dataSource = new MatTableDataSource<SubCategory>();
  selection = new SelectionModel<SubCategory>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public http: HttpClient,
    public fb: FormBuilder,
    public subCategoryService: SubCategoryService,
    public categoryService: CategoryService,
    private fileHandler: FileHandler,
    private cd: ChangeDetectorRef,
    private messageService: MessageService,
    private dataPassService: DataPassService
  ) { }

  ngAfterViewInit(): void {
    this.load();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.subCategoryForm.valueChanges.pipe(distinctUntilChanged()).subscribe({
      next: (v: any) => {
        let obj = { data: this.subCategoryForm.getRawValue(), errors: this.errors };
        this.dataPassService.setData(obj);
      },
    });
  }
  load(): any {
    return this.http.get(this.jsonUrl).subscribe((e: any) => {
      this.dataSource = new MatTableDataSource<SubCategory>(e);
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => (this.subCategoryId = params["id"] ? Number(params["id"]) : 0));
    this.isEdit = !!this.subCategoryId;
    this.subCategoryForm = this.form;
    this.subCategoryForm.patchValue({ id: this.subCategoryId });
    this.getSubCategoryDetail(this.subCategoryId);
    this.getCategory("");
    this.categorySubject
    this.uploader = new FileUploader({});
  }

  getSubCategoryDetail(subCategoryId: any) {
    if (!this.subCategoryId) return;
    this.subCategoryService.getSubCategoryDetail(subCategoryId).subscribe({
      next: (res: any) => {
        if (res) {
          this.subCategory = res.data;
          this.form = res.data;
        }
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  getCategory(searchText: string) {
    this.categoryService.getDropdown(0, 10, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.categories = res.data.data;
          this.filterCategories = res.data.data;
        }
      },
      // error: (error: any) => {
      //   console.error(error);
      // },
    });
  }

  onCategoryFilter($event: any){
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.categorySubject.next(value);
  }

  get form(): any {
    return this.fb.group({
      id: [0],
      categoryId:[],
      subCategoryName: ["", [Validators.required, Validators.maxLength(50)]],
      subCategoryCode: ["", [Validators.required]],
      description: [""],
      cancel: [true],
      picture: [null],
      pictureFile: []
    });
  }

  set form(data: any) {
    if (data !== null) {
      this.subCategoryForm.patchValue({
        id: data.id,
        categoryId: data.categoryId,
        subCategoryName: data.subCategoryName,
        subCategoryCode: data.subCategoryCode,
        description: data.description,
        cancel: data.cancel,
        picture: data.picture
      });

      this.avaterPreview = data.picture ? `${environment.baseUrl}/${data.picture}` : "";
      this.picture = data.picture;
      console.log("path avater", this.picture);
      console.log("path", data, this.subCategoryForm.getRawValue());
    }
  }

  
  save(): void {
    this.errors = FormExtension.getFormValidationErrors(this.subCategoryForm);
    if (this.subCategoryForm.invalid) {
      this.subCategoryForm.markAllAsTouched();
      return;
    }

    //   this.userInformationForm.patchValue({
    //     birthDate: this.userInformationForm.get('birthDate')?.value.toISOString()
    // });
    this.subCategoryFormValue = this.subCategoryForm.getRawValue();
    console.log("save avater", this.picture);
    this.subCategoryForm.patchValue({ picture: this.picture });
    this.formData = FormExtension.toFormData(this.subCategoryForm);
    console.log("save", this.subCategoryFormValue);

    debugger;
    if (this.subCategoryFormValue.id > 0) {
      // this.userInformationService.updateUserDetailWithFile(this.submitForm.id, this.submitForm, this.picture).subscribe({
      //   next: (n: any) => console.log(n),
      //   error: (e: any) => console.log(e),
      // });

      this.subCategoryService.updateSubCategoryDetail(this.subCategoryFormValue.id, this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.updateSuccess);
          this.router.navigate(['subCategory']);
        },
      });
    } else {
      this.subCategoryService.addSubCategoryDetail(this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.saveSucess);
          this.router.navigate(['subCategory']);
        },
      });
    }
  }

  reset(): void {
    this.subCategoryForm.reset(this.form.value);
  }

  clearCategory(): void {
    this.subCategoryForm.reset();
    this.subCategoryForm.markAsUntouched();
    FormExtension.markAllAsUntoched(this.subCategoryForm);
  }

  pictureUpload($event: any): void {
    const files = $event.target.files;
    console.log(files);
    this.subCategoryForm.patchValue({
      pictureFile: files[0],
    });
    this.subCategoryForm.get("image")?.updateValueAndValidity();
    this.cd.markForCheck();
    console.log(this.subCategoryForm);
    this.uploader.setOptions({
      url: this.uploadUrl,
    });

    if (files.length === 0 || files == null) {
    }
    const file = files[0];

    if (file.size === 0) {
    }

    if (!this.fileHandler.fileValidate(file)) {
      this.uploader.clearQueue();
      return;
    }
  }

  uploadFileAttach($event: any) {
    const reader = new FileReader();
    const file = $event.target.files[0];
    console.log(file);
    this.subCategoryForm.get("image")?.updateValueAndValidity();
    // this.cd.markForCheck();
    if ($event.target.files && $event.target.files.length) {
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.subCategoryForm.patchValue({
          pictureFile: file,
        });
        this.picture = file;
        this.avaterPreview = reader.result as string;

        // need to run CD since file load runs outside of zone
        this.cd.markForCheck();
      };
      reader.onerror = () => { };
    }

    console.log(this.subCategoryForm);
    // reader.readAsDataURL(file)
  }
}

