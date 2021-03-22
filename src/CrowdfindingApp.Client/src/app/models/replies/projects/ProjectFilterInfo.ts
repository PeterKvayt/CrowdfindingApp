import { ProjectStatusEnum } from '../../enums/ProjectStatus';

export class ProjectFilterInfo {
  public id?: string[];
  public title?: string[];
  public categoryId?: string[];
  public status?: ProjectStatusEnum[];
  public ownerId?: string[];
}
