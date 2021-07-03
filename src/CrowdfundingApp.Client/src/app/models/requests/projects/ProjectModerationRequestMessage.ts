import { SaveDraftProjectRequestMessage } from './SaveDraftProjectRequestMessage';
import { ProjectInfo } from '../../replies/projects/ProjectInfo';

export class ProjectModerationRequestMessage extends SaveDraftProjectRequestMessage {
  constructor(data: ProjectInfo
  ) {
    super(data);
  }
}
