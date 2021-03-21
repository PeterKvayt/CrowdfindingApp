import { PagingInfo } from 'src/app/models/common/PagingInfo';
import { ProjectFilterInfo } from 'src/app/models/replies/projects/ProjectFilterInfo';

export class PagingControl<TCollection> {
  public paging: PagingInfo;
  public filter: ProjectFilterInfo;
  public collection?: TCollection[];
}