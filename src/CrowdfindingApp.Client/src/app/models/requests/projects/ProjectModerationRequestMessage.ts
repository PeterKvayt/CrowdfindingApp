import { SaveDraftProjectRequestMessage } from './SaveDraftProjectRequestMessage';
import { DraftProjectInfo } from '../../replies/projects/DraftProjectInfo';

export class ProjectModerationRequestMessage extends SaveDraftProjectRequestMessage {
  constructor(data: DraftProjectInfo
  ) {
    super(data);
  }
}
