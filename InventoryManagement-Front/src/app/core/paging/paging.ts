// export class Paging<T> extends Array<T> {
//   public static readonly DEFAULT_PAGE_SIZE: number = 10;

//   pageIndex: number;
//   pageSize: number;
//   totalCount: number;

//   constructor( pgIx?: number, pgSize?: number, tot?: number, items?: T[] )
//   {
//       super();
//       this.pageIndex = pgIx ? pgIx : 0;
//       this.pageSize = pgSize ? pgSize : 0;
//       this.totalCount = tot ? tot : 0;
//       if ( items && items.length > 0 ) {
//           this.push( ...items );
//       }
//   }
// }

export class Paging<T> {
  data: T[];
  total: number;
  pageIndex: number;
  pageSize: number;
  currentPage: number;
  totalPage: number;
  hasPreviousPage: number;
  hasNextPage: number;
}

export class Dropdown<T> {
  data: T[];
  size: number;
}

export class ApiOkResult<T> {
  data: T;
  statusCode: number;
  isSuccess: boolean;
}
