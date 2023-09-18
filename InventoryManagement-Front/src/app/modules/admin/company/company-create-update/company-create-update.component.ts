import { SelectionModel } from '@angular/cdk/collections';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatAutocomplete } from '@angular/material/autocomplete';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';
import { FormExtension } from 'src/app/core/form/form-extension';
import { DataPassService } from 'src/app/core/services/data-pass.service';
import { MessageService } from 'src/app/core/services/message/message.service';
import { Messages } from 'src/app/core/services/message/messages';
import { City } from 'src/app/features/models/city.model';
import { Company } from 'src/app/features/models/company.model';
import { Country } from 'src/app/features/models/country.model';
import { State } from 'src/app/features/models/state.model';
import { CityService } from 'src/app/features/services/city.service';
import { CompanyService } from 'src/app/features/services/company.service';
import { CountryService } from 'src/app/features/services/country.service';
import { StateService } from 'src/app/features/services/state.service';

@Component({
  selector: 'app-company-create-update',
  templateUrl: './company-create-update.component.html',
  styleUrls: ['./company-create-update.component.scss']
})
export class CompanyCreateUpdateComponent {
  jsonUrl = "assets/data/company.data.json";
  message = Messages;
  errors: any;
  formData: any;
  userId: number;
  companyId: number;
  companyForm: FormGroup;
  companyFormValue: any;
  isEdit = false;
  company: Company;
  countryId: number;
  stateId: number;
  cityId: number;
  countries: Country[];
  states:State[];
  cities:City[];
  filteredCountries: Country[];
  filteredStates: State[];
  filteredCities: City[];

  private countrySubject: Subject<string> = new Subject<string>();
  private stateSubject: Subject<string> = new Subject<string>();
  private citySubject: Subject<string> = new Subject<string>();

  @ViewChild("lableInput") labelInput: ElementRef<HTMLInputElement>;
  @ViewChild("auto") matAutocomplete: MatAutocomplete;

  tomorrow = new Date();

  displayedColumns: string[] =["companyName","address","contactNumber","country","action"];
  dataSource = new MatTableDataSource<Company>();
  selection = new SelectionModel<Company>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public http: HttpClient,
    public fb: FormBuilder,
    public companyService: CompanyService,
    public stateService: StateService,
    public countryService: CountryService,
    public cityService: CityService,
    private cd: ChangeDetectorRef,
    private messageService: MessageService,
    private dataPassService: DataPassService
  ) { }

  
  ngAfterViewInit(): void {
    this.load();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.companyForm.valueChanges.pipe(distinctUntilChanged()).subscribe({
      next: (v: any) => {
        let obj = { data: this.companyForm.getRawValue(), errors: this.errors };
        this.dataPassService.setData(obj);
      },
    });
  }
  load(): any {
    return this.http.get(this.jsonUrl).subscribe((e: any) => {
      this.dataSource = new MatTableDataSource<Company>(e);
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => (this.companyId = params["id"] ? Number(params["id"]) : 0));
    this.isEdit = !!this.companyId;
    this.companyForm = this.form;
    this.companyForm.patchValue({ id: this.companyId });
    this.getcompanyDetail(this.companyId);
    
    this.getCountry("");
    this.countrySubject.pipe(debounceTime(1000)).subscribe({next: (searchText: string) => this.getCountry(searchText)});
    this.stateSubject.pipe(debounceTime(1000)).subscribe({next: (searchText: string) => this.getState(this.countryId, searchText)});
    this.citySubject.pipe(debounceTime(1000)).subscribe({next: (searchText: string) => this.getCity(this.stateId, searchText)});
    this.companyForm
      .get("countryId")
      ?.valueChanges.pipe(distinctUntilChanged())
      .subscribe({
        next: (countryId: number) => {
          this.countryId = countryId;
          this.getState(countryId,"");
        },
      });
    this.companyForm
      .get("stateId")
      ?.valueChanges.pipe(distinctUntilChanged())
      .subscribe({
        next: (stateId: number) => {
          this.stateId = stateId;
          this.getCity(stateId, "");
        },
      });
  }

  getcompanyDetail(companyId: any) {
    if (!this.companyId) return;
    this.companyService.getCompanyDetail(companyId).subscribe({
      next: (res: any) => {
        if (res) {
          this.company = res.data;
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
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  getState(countryId: number, searchText?: string) {
    if (!countryId) return;

    this.stateService.getDropdownByCountry(countryId, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.states = res.data.data;
          this.filteredStates = res.data.data;
        }
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  getCity(stateId: number, searchText?: string) {
    if (!stateId) return;
    this.cityService.getDropdownByState(stateId, searchText).subscribe({
      next: (res: any) => {
        if (res) {
          this.cities = res.data.data;
          this.filteredCities = res.data.data;
        }
      },
      error: (error: any) => {
        console.error(error);
      },
    });
  }

  onCountryFilter($event: any) {
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.countrySubject.next(value);
  }
  
  onStateFilter($event: any) {
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.stateSubject.next(value);
  }

  onCityFilter($event: any) {
    const value = $event.target.value.toLocaleLowerCase().toString();

    this.citySubject.next(value);
  }

  get form(): any {
    return this.fb.group({
      id: [0],
      companyName: ["", [Validators.required, Validators.maxLength(50)]],
      address: [""],
      countryId:[null],
      stateId:[null],
      cityId:[null],
      zipCode:[""],
      contactNumber:["",[Validators.required]]

    });
  }

  set form(data: any) {
    if (data !== null) {
      this.companyForm.patchValue({
        id: data.id,
        companyName: data.companyName,
        address: data.address,
        countryId: data.countryId,
        stateId: data.stateId,
        cityId: data.cityId,
        zipCode: data.zipCode,
        contactNumber: data.contactNumber
      });
    }
  }
  
 

  
  save(): void {
    this.errors = FormExtension.getFormValidationErrors(this.companyForm);
    if (this.companyForm.invalid) {
      this.companyForm.markAllAsTouched();
      return;
    }

    //   this.userInformationForm.patchValue({
    //     birthDate: this.userInformationForm.get('birthDate')?.value.toISOString()
    // });
    this.companyFormValue = this.companyForm.getRawValue();
    this.formData = FormExtension.toFormData(this.companyForm);
    console.log("save", this.companyFormValue);

    debugger;
    if (this.companyFormValue.id > 0) {
      // this.userInformationService.updateUserDetailWithFile(this.submitForm.id, this.submitForm, this.picture).subscribe({
      //   next: (n: any) => console.log(n),
      //   error: (e: any) => console.log(e),
      // });

      this.companyService.updateCompanyDetail(this.companyFormValue.id, this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.updateSuccess);
          this.router.navigate(['company']);
        },
      });
    } else {
      this.companyService.addCompanyDetail(this.formData).subscribe({
        next: (n: any) => {
          this.messageService.success(this.message.saveSucess);
          this.router.navigate(['company']);
        },
      });
    }
  }

  reset(): void {
    this.companyForm.reset(this.form.value);
  }

  clearcompany(): void {
    this.companyForm.reset();
    this.companyForm.markAsUntouched();
    FormExtension.markAllAsUntoched(this.companyForm);
  }
}
