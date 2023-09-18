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
import { Category } from 'src/app/features/models/category.model';
import { CategoryService } from 'src/app/features/services/category.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-category-create-update',
  templateUrl: './category-create-update.component.html',
  styleUrls: ['./category-create-update.component.scss']
})
export class CategoryCreateUpdateComponent implements OnInit {

  jsonUrl = "assets/data/category.data.json";
  message = Messages;
  errors: any;
  formData: any;

  categoryId: number;
  categoryForm: FormGroup;
  categoryFormValue: any;
  isEdit = false;
  picture: any;
  avaterPreview: any;
  category: Category;
  
  public uploader: FileUploader;
  uploadUrl: string;

  @ViewChild("lableInput") labelInput: ElementRef<HTMLInputElement>;
  @ViewChild("auto") matAutocomplete: MatAutocomplete;

  tomorrow = new Date();

  displayedColumns: string[] = ["picture", "name", "phone", "email", "genderId", "action"];
  dataSource = new MatTableDataSource<Category>();
  selection = new SelectionModel<Category>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public http: HttpClient,
    public fb: FormBuilder,
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
    this.categoryForm.valueChanges.pipe(distinctUntilChanged()).subscribe({
      next: (v: any) => {
        let obj = { data: this.categoryForm.getRawValue(), errors: this.errors };
        this.dataPassService.setData(obj);
      },
    });
  }
  load(): any {
    return this.http.get(this.jsonUrl).subscribe((e: any) => {
      this.dataSource = new MatTableDataSource<Category>(e);
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => (this.categoryId = params["id"] ? Number(params["id"]) : 0));
    this.isEdit = !!this.categoryId;
    this.categoryForm = this.form;
    this.categoryForm.patchValue({ id: this.categoryId });
    this.getCategoryDetail(this.categoryId);
    this.uploader = new FileUploader({});
  }

  getCategoryDetail(categoryId: any) {
    if (!this.categoryId) return;
    this.categoryService.getCategoryDetail(categoryId).subscribe({
      next: (res: any) => {
        if (res) {
          this.category = res.data;
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
      categoryName: ["", [Validators.required, Validators.maxLength(50)]],
      categoryCode: ["", [Validators.required]],
      description: [""],
      cancel: [true],
      picture: [null],
      pictureFile: []
    });
  }

  set form(data: any) {
    if (data !== null) {
      this.categoryForm.patchValue({
        id: data.id,
        categoryName: data.categoryName,
        categoryCode: data.categoryCode,
        description: data.description,
        cancel: data.cancel,
        picture: data.picture
      });

      this.avaterPreview = data.picture ? `${environment.baseUrl}/${data.picture}` : "";
      this.picture = data.picture;
      console.log("path avater", this.picture);
      console.log("path", data, this.categoryForm.getRawValue());
    }
  }

  
  save(): void {
    this.errors = FormExtension.getFormValidationErrors(this.categoryForm);
    if (this.categoryForm.invalid) {
      this.categoryForm.markAllAsTouched();
      return;
    }

    //   this.userInformationForm.patchValue({
    //     birthDate: this.userInformationForm.get('birthDate')?.value.toISOString()
    // });
    this.categoryFormValue = this.categoryForm.getRawValue();
    console.log("save avater", this.picture);
    this.categoryForm.patchValue({ picture: this.picture });
    this.formData = FormExtension.toFormData(this.categoryForm);
    console.log("save", this.categoryFormValue);

    debugger;
    if (this.categoryFormValue.id > 0) {
      // this.userInformationService.updateUserDetailWithFile(this.submitForm.id, this.submitForm, this.picture).subscribe({
      //   next: (n: any) => console.log(n),
      //   error: (e: any) => console.log(e),
      // });

      this.categoryService.updateCategoryDetail(this.categoryFormValue.id, this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.updateSuccess);
          this.router.navigate(['category']);
        },
      });
    } else {
      this.categoryService.addCategoryDetail(this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.saveSucess);
          this.router.navigate(['category']);
        },
      });
    }
  }

  reset(): void {
    this.categoryForm.reset(this.form.value);
  }

  clearCategory(): void {
    this.categoryForm.reset();
    this.categoryForm.markAsUntouched();
    FormExtension.markAllAsUntoched(this.categoryForm);
  }

  pictureUpload($event: any): void {
    const files = $event.target.files;
    console.log(files);
    this.categoryForm.patchValue({
      pictureFile: files[0],
    });
    this.categoryForm.get("image")?.updateValueAndValidity();
    this.cd.markForCheck();
    console.log(this.categoryForm);
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
    this.categoryForm.get("image")?.updateValueAndValidity();
    // this.cd.markForCheck();
    if ($event.target.files && $event.target.files.length) {
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.categoryForm.patchValue({
          pictureFile: file,
        });
        this.picture = file;
        this.avaterPreview = reader.result as string;

        // need to run CD since file load runs outside of zone
        this.cd.markForCheck();
      };
      reader.onerror = () => { };
    }

    console.log(this.categoryForm);
    // reader.readAsDataURL(file)
  }
}
