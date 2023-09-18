import { SelectionModel } from '@angular/cdk/collections';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatAutocomplete } from '@angular/material/autocomplete';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { distinctUntilChanged } from 'rxjs';
import { FormExtension } from 'src/app/core/form/form-extension';
import { DataPassService } from 'src/app/core/services/data-pass.service';
import { FileHandler } from 'src/app/core/services/file/file.handler';
import { MessageService } from 'src/app/core/services/message/message.service';
import { Messages } from 'src/app/core/services/message/messages';
import { Unit } from 'src/app/features/models/unit.model';
import { UnitService } from 'src/app/features/services/unit.service';

@Component({
  selector: 'app-unit-create-update',
  templateUrl: './unit-create-update.component.html',
  styleUrls: ['./unit-create-update.component.scss']
})
export class UnitCreateUpdateComponent implements OnInit{

  jsonUrl = "assets/data/unit.data.json";
  
  message = Messages;
  errors: any;
  formData: any;

  unitId: number;
  unitForm: FormGroup;
  unitFormValue: any;
  isEdit = false;
  unit: Unit;

  @ViewChild("lableInput") labelInput: ElementRef<HTMLInputElement>;
  @ViewChild("auto") matAutocomplete: MatAutocomplete;

  tomorrow = new Date();

  displayedColumns: string[] = ["unitName", "action"];
  dataSource = new MatTableDataSource<Unit>();
  selection = new SelectionModel<Unit>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public http: HttpClient,
    public fb: FormBuilder,
    public unitService: UnitService,
    private fileHandler: FileHandler,
    private cd: ChangeDetectorRef,
    private messageService: MessageService,
    private dataPassService: DataPassService
  ) { }

  ngAfterViewInit(): void {
    this.load();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.unitForm.valueChanges.pipe(distinctUntilChanged()).subscribe({
      next: (v: any) => {
        let obj = { data: this.unitForm.getRawValue(), errors: this.errors };
        this.dataPassService.setData(obj);
      },
    });
  }
  load(): any {
    return this.http.get(this.jsonUrl).subscribe((e: any) => {
      this.dataSource = new MatTableDataSource<Unit>(e);
    });
  }
  


  ngOnInit(): void {
    this.route.params.subscribe((params) => (this.unitId = params["id"] ? Number(params["id"]) : 0));
    this.isEdit = !!this.unitId;
    this.unitForm = this.form;
    this.unitForm.patchValue({ id: this.unitId });
    this.getUnitDetail(this.unitId);
    //this.uploader = new FileUploader({});
  }

  getUnitDetail(unitId: any) {
    if (!this.unitId) return;
    this.unitService.getUnitDetail(unitId).subscribe({
      next: (res: any) => {
        if (res) {
          this.unit = res.data;
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
      unitName: ["", [Validators.required, Validators.maxLength(50)]]
    });
  }

  set form(data: any) {
    if (data !== null) {
      this.unitForm.patchValue({
        id: data.id,
        unitName: data.unitName
      });

      // this.avaterPreview = data.picture ? `${environment.baseUrl}/${data.picture}` : "";
      // this.picture = data.picture;
      // console.log("path avater", this.picture);
      console.log("path", data, this.unitForm.getRawValue());
    }
  }

  save(): void {
    this.errors = FormExtension.getFormValidationErrors(this.unitForm);
    if (this.unitForm.invalid) {
      this.unitForm.markAllAsTouched();
      return;
    }

    //   this.userInformationForm.patchValue({
    //     birthDate: this.userInformationForm.get('birthDate')?.value.toISOString()
    // });
    this.unitFormValue = this.unitForm.getRawValue();
    //console.log("save avater", this.picture);
    //this.unitForm.patchValue({ picture: this.picture });
    this.formData = FormExtension.toFormData(this.unitForm);
    console.log("save", this.unitFormValue);

    debugger;
    if (this.unitFormValue.id > 0) {
      // this.userInformationService.updateUserDetailWithFile(this.submitForm.id, this.submitForm, this.picture).subscribe({
      //   next: (n: any) => console.log(n),
      //   error: (e: any) => console.log(e),
      // });

      this.unitService.updateUnitDetail(this.unitFormValue.id, this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.updateSuccess);
          this.router.navigate(['unit']);
        },
      });
    } else {
      this.unitService.addUnitDetail(this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.saveSucess);
          this.router.navigate(['unit']);
        },
      });
    }
  }

  reset(): void {
    this.unitForm.reset(this.form.value);
  }

  clearUnit(): void {
    this.unitForm.reset();
    this.unitForm.markAsUntouched();
    FormExtension.markAllAsUntoched(this.unitForm);
  }

  // pictureUpload($event: any): void {
  //   const files = $event.target.files;
  //   console.log(files);
  //   this.categoryForm.patchValue({
  //     pictureFile: files[0],
  //   });
  //   this.categoryForm.get("image")?.updateValueAndValidity();
  //   this.cd.markForCheck();
  //   console.log(this.categoryForm);
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

  // uploadFileAttach($event: any) {
  //   const reader = new FileReader();
  //   const file = $event.target.files[0];
  //   console.log(file);
  //   this.categoryForm.get("image")?.updateValueAndValidity();
  //   // this.cd.markForCheck();
  //   if ($event.target.files && $event.target.files.length) {
  //     reader.readAsDataURL(file);
  //     reader.onload = () => {
  //       this.categoryForm.patchValue({
  //         pictureFile: file,
  //       });
  //       this.picture = file;
  //       this.avaterPreview = reader.result as string;

  //       // need to run CD since file load runs outside of zone
  //       this.cd.markForCheck();
  //     };
  //     reader.onerror = () => { };
  //   }

  //   console.log(this.categoryForm);
  //   // reader.readAsDataURL(file)
  // }

}
