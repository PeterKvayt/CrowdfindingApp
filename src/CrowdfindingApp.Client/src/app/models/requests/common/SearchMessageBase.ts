import { PagingInfo } from '../../common/PagingInfo';

export class SearchMessageBase<TFilter> {
  public filter?: TFilter;
  public paging?: PagingInfo;
}
