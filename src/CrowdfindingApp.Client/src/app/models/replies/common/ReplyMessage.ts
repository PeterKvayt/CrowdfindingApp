import { ReplyMessageBase } from '../ReplyMessageBase';

export class ReplyMessage<T> extends ReplyMessageBase {
  public value: T;
}
