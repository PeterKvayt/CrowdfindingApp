export class UpdateUserRequestMessage {
  constructor(
    public userName: string,
    public email: string,
    public name: string,
    public surname: string,
    public middleName: string,
    public image: string,
  ) { }
}
