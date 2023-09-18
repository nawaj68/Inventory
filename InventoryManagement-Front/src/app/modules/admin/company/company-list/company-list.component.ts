import { SelectionModel } from '@angular/cdk/collections';
import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { debounceTime, distinctUntilChanged, map, merge, pipe, share, startWith, Subject, switchMap } from 'rxjs';
import { MessageService } from 'src/app/core/services/message/message.service';
import { Messages } from 'src/app/core/services/message/messages';
import { RouteService } from 'src/app/core/services/routes/route.service';
import { FrontRoute } from 'src/app/features/configs/front-route';
import { CompanyService } from 'src/app/features/services/company.service';
import { Company } from '../../../../features/models/company.model';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.scss']
})
export class CompanyListComponent {
  frontRoute = FrontRoute;
  userId: number;

  company: Company[];
  companys: Company[];

  displayedColumns: string[] = ["companyName","address","contactNumber","country","action"];
  dataSource: MatTableDataSource<Company> = new MatTableDataSource();
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageIndex = 0;
  pageSize = 5;
  length = 0;

  selection = new SelectionModel<Company>(true, []);
  filterInput: Subject<string> = new Subject<string>();
  filterValue: string = "";

  isLoadingResults = true;
  message = Messages;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    public dialog: MatDialog, 
    public companyService: CompanyService, 
    public routeService: RouteService, 
    private messageService: MessageService) { }
    
    ngOnInit(): void { }

    ngAfterViewInit(): void {
      this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.getData();
    }
  
    customPipe = pipe(
      share(),
      debounceTime(1000),
      distinctUntilChanged(),
      startWith({}),
      switchMap(() => {
        this.isLoadingResults = true;
        return this.load();
      }),
  
      map((data: any) => {
        this.isLoadingResults = false;
  
        if (data === null) {
          return [];
        }
  
        return data;
      })
    );
  
    customSubscribe = (data: any) => {
      this.company = data.data.data;
      this.dataSource = new MatTableDataSource(data.data.data);
      this.paginator.pageIndex = data.data.pageIndex;
      this.paginator.length = data.data.total;
    };
  
    load(): any {
      return this.companyService.getSearch(
        this.paginator.pageIndex,
        this.paginator.pageSize,
        this.sort.active,
        this.sort.direction,
        this.filterValue
      );
    }
  
    getData(): void {
      merge(this.sort.sortChange, this.paginator.page, this.filterInput)
        .pipe(this.customPipe)
        .subscribe((data: any) => {
          this.customSubscribe(data);
        });
    }
  
    reload(reloadType: boolean): void {
      if (reloadType) {
        this.paginator.pageIndex = 0;
        this.filterValue = "";
      }
  
      this.getData();
    }
    pageChanged(event: PageEvent) {
      this.paginator.pageSize = event.pageSize;
      this.paginator.pageIndex = event.pageIndex;
    }
  
    search(event: Event): void {
      const value = (event.target as HTMLInputElement).value;
  
      if (this.filterInput.observers.length === 0) {
        this.filterInput.pipe(debounceTime(1000), distinctUntilChanged());
      }
  
      this.filterInput.next(value);
      this.filterInput.subscribe((e) => (this.filterValue = e));
  
      if (this.dataSource.paginator) {
        this.dataSource.paginator.firstPage();
      }
    }
  
    remove(companyId: number): void {
      if (companyId) {
        this.messageService.confirm().then((result) => {
          if (result.value) {
            this.companyService.delete(companyId).subscribe((e) => {
              this.messageService.success(this.message.deleteSuccess);
              this.reload(true);
            });
          }
        });
      }
    }
}
