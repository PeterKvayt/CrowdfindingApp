import { ProjectStatusEnum } from '../../enums/ProjectStatus';

export class SetProjectStatusRequestMessage {

  constructor(
    public status: ProjectStatusEnum,
    public projectId: string
  ) { }
}
