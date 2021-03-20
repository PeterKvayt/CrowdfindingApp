import { ReplyMessage } from './ReplyMessage';
import { PagingInfo } from '../../common/PagingInfo';

export class PagedReplyMessage<T> extends ReplyMessage<T> {

  public paging: PagingInfo;

}
