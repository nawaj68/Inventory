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
import { Country } from 'src/app/features/models/country.model';
import { State } from 'src/app/features/models/state.model';
import { CountryService } from 'src/app/features/services/country.service';
import { StateService } from 'src/app/features/services/state.service';


@Component({
  selector: 'app-state-create-update',
  templateUrl: './state-create-update.component.html',
  styleUrls: ['./state-create-update.component.scss']
})
export class StateCreateUpdateComponent implements OnInit {
  jsonUrl = "assets/data/state.data.json";

  message = Messages;
  errors: any;
  formData: any;
  
  isEdit = false;

  state: State;

  countryId: number;
  stateId: number;
  stateForm: FormGroup;
  stateFormValue: any;

  countries: Country[];
  filteredCountries: Country[];

  private countrySubject: Subject<string> = new Subject<string>();

  @ViewChild("lableInput") labelInput: ElementRef<HTMLInputElement>;
  @ViewChild("auto") matAutocomplete: MatAutocomplete;

  displayedColumns: string[] = ["name", "country", "action"];
  dataSource = new MatTableDataSource<State>();
  selection = new SelectionModel<State>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public http: HttpClient,
    public fb: FormBuilder,
    public stateService: StateService,
    public countryService: CountryService,
    private fileHandler: FileHandler,
    private cd: ChangeDetectorRef,
    private messageService: MessageService,
    private dataPassService: DataPassService
  ) { }

  ngAfterViewInit(): void {
    this.load();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.stateForm.valueChanges.pipe(distinctUntilChanged()).subscribe({
      next: (v: any) => {
        let obj = { data: this.stateForm.getRawValue(), errors: this.errors };
        this.dataPassService.setData(obj);
      },
    });
  }

  load(): any {
    return this.http.get(this.jsonUrl).subscribe((e: any) => {
      this.dataSource = new MatTableDataSource<State>(e);
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => (this.stateId = params["id"] ? Number(params["id"]) : 0));
    this.isEdit = !!this.stateId;
    this.stateForm = this.form;
    this.stateForm.patchValue({ id: this.stateId });
    this.getStateDetail(this.stateId);
    this.getCountry("");
    this.countrySubject.pipe(debounceTime(1000)).subscribe({ next: (v: string) => this.getCountry(v) });

  }

  getStateDetail(stateId: any) {
    if (!this.stateId) return;
    this.stateService.getStateDetail(stateId).subscribe({
      next: (res: any) => {
        if (res) {
          this.state = res.data;
          this.form = res.data;
        }
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  getCountry(searchText: string) {
    this.countryService.getDropdown(0, 10, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.countries = res.data.data;
          this.filteredCountries = res.data.data;
        }
      },
      // error: (error: any) => {
      //   console.error(error);
      // },
    });
  }
  onCountryFilter($event: any) {
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.countrySubject.next(value);
  }

  get form(): any {
    return this.fb.group({
      id: [0],
      // userId: [1],
      countryId: [],
      name: ["", [Validators.required, Validators.maxLength(50)]],
    });
  }

  set form(user: any) {
    if (user !== null) {
      this.stateForm.patchValue({
        id: user.id,
        // userId: 1,//user.userId,
        name: user.name,
        countryId: user.countryId,
      });
    }
  }


  save(): void {
    this.errors = FormExtension.getFormValidationErrors(this.stateForm);
    if (this.stateForm.invalid) {
      this.stateForm.markAllAsTouched();
      return;
    }

    //   this.userInformationForm.patchValue({
    //     birthDate: this.userInformationForm.get('birthDate')?.value.toISOString()
    // });
    this.stateFormValue = this.stateForm.getRawValue();
    this.formData = FormExtension.toFormData(this.stateForm);
    console.log("save", this.stateFormValue);

    debugger;
    if (this.stateFormValue.id > 0) {
      // this.userInformationService.updateUserDetailWithFile(this.submitForm.id, this.submitForm, this.avatar).subscribe({
      //   next: (n: any) => console.log(n),
      //   error: (e: any) => console.log(e),
      // });

      this.stateService.updateStateDetail(this.stateFormValue.id, this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.updateSuccess);
          this.router.navigate(['state']);
        },
      });
    } else {
      this.stateService.addStateDetail(this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.saveSucess);
          this.router.navigate(['state']);
        },
      });
    }
  }

  reset(): void {
    this.stateForm.reset(this.form.value);
  }

  clearState(): void {
    this.stateForm.reset();
    this.stateForm.markAsUntouched();
    FormExtension.markAllAsUntoched(this.stateForm);
  }

}