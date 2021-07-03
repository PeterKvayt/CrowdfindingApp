import { ReplyMessageBase } from '../common/ReplyMessageBase';

export class ReplyMessage<T> extends ReplyMessageBase {
  public value: T;
}
