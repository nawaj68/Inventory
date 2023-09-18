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
import { City } from 'src/app/features/models/city.model';
import { State } from 'src/app/features/models/state.model';
import { CityService } from 'src/app/features/services/city.service';
import { StateService } from 'src/app/features/services/state.service';

@Component({
  selector: 'app-city-create-update',
  templateUrl: './city-create-update.component.html',
  styleUrls: ['./city-create-update.component.scss']
})
export class CityCreateUpdateComponent implements OnInit {
  jsonUrl = "assets/data/city.data.json";

  message = Messages;
  errors: any;
  formData: any;
  
  isEdit = false;

  city: City;
  
  cityId: number;
  cityForm: FormGroup;
  cityFormValue: any;

  stateId: number;
  states: State[];
  filteredStates: State[];

  private stateSubject: Subject<string> = new Subject<string>();

  @ViewChild("lableInput") labelInput: ElementRef<HTMLInputElement>;
  @ViewChild("auto") matAutocomplete: MatAutocomplete;

  displayedColumns: string[] = ["name", "state", "action"];
  dataSource = new MatTableDataSource<City>();
  selection = new SelectionModel<City>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public http: HttpClient,
    public fb: FormBuilder,
    public cityService: CityService,
    public stateService: StateService,
    private fileHandler: FileHandler,
    private cd: ChangeDetectorRef,
    private messageService: MessageService,
    private dataPassService: DataPassService
  ) { }

  ngAfterViewInit(): void {
    this.load();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.cityForm.valueChanges.pipe(distinctUntilChanged()).subscribe({
      next: (v: any) => {
        let obj = { data: this.cityForm.getRawValue(), errors: this.errors };
        this.dataPassService.setData(obj);
      },
    });
  }

  load(): any {
    return this.http.get(this.jsonUrl).subscribe((e: any) => {
      this.dataSource = new MatTableDataSource<City>(e);
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => (this.cityId = params["id"] ? Number(params["id"]) : 0));
    this.isEdit = !!this.cityId;
    this.cityForm = this.form;
    this.cityForm.patchValue({ id: this.cityId });
    this.getCityDetail(this.cityId);
    this.getState("");
    this.stateSubject.pipe(debounceTime(1000)).subscribe({ next: (v: string) => this.getState(v) });

  }

  getCityDetail(cityId: any) {
    if (!this.cityId) return;
    this.cityService.getCityDetail(cityId).subscribe({
      next: (res: any) => {
        if (res) {
          this.city = res.data;
          this.form = res.data;
        }
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  getState(searchText: string) {
    this.stateService.getDropdown(0, 10, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.states = res.data.data;
          this.filteredStates = res.data.data;
        }
      },
      // error: (error: any) => {
      //   console.error(error);
      // },
    });
  }
  onStateFilter($event: any) {
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.stateSubject.next(value);
  }

  get form(): any {
    return this.fb.group({
      id: [0],
      // userId: [1],
      stateId: [],
      name: ["", [Validators.required, Validators.maxLength(50)]],
    });
  }

  set form(user: any) {
    if (user !== null) {
      this.cityForm.patchValue({
        id: user.id,
        // userId: 1,//user.userId,
        name: user.name,
        stateId: user.stateId,
      });
    }
  }


  save(): void {
    this.errors = FormExtension.getFormValidationErrors(this.cityForm);
    if (this.cityForm.invalid) {
      this.cityForm.markAllAsTouched();
      return;
    }

    //   this.userInformationForm.patchValue({
    //     birthDate: this.userInformationForm.get('birthDate')?.value.toISOString()
    // });
    this.cityFormValue = this.cityForm.getRawValue();
    this.formData = FormExtension.toFormData(this.cityForm);
    console.log("save", this.cityFormValue);

    debugger;
    if (this.cityFormValue.id > 0) {
      // this.userInformationService.updateUserDetailWithFile(this.submitForm.id, this.submitForm, this.avatar).subscribe({
      //   next: (n: any) => console.log(n),
      //   error: (e: any) => console.log(e),
      // });

      this.cityService.updateCityDetail(this.cityFormValue.id, this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.updateSuccess);
          this.router.navigate(['city']);
        },
      });
    } else {
      this.cityService.addCityDetail(this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.saveSucess);
          this.router.navigate(['city']);
        },
      });
    }
  }

  reset(): void {
    this.cityForm.reset(this.form.value);
  }

  clearCity(): void {
    this.cityForm.reset();
    this.cityForm.markAsUntouched();
    FormExtension.markAllAsUntoched(this.cityForm);
  }

}