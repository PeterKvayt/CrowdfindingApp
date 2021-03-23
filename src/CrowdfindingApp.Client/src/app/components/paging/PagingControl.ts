import { PagingInfo } from 'src/app/models/common/PagingInfo';
import { ProjectFilterInfo } from 'src/app/models/replies/projects/ProjectFilterInfo';
import { PagedReplyMessage } from 'src/app/models/replies/common/PagedReplyMessage';
import { ProjectCard } from '../project-card/ProjectCard';
import { ProjectSearchRequestMessage } from 'src/app/models/requests/projects/ProjectSearchRequestMessage';

export class PagingControl<TCollection> {
  public paging: PagingInfo;
  public filter: ProjectFilterInfo;
  public collection?: TCollection[];
}