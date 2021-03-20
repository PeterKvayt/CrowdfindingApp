export class PagingInfo {

  constructor(
    public pageNumber: number,
    public pageSize: number,
    public totalCount?: number
  ) { }
}
