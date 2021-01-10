export class GetTokenRequestMessage {
  constructor(
    public email: string,
    public password: string) { }
}