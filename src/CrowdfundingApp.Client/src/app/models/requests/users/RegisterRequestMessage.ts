export class RegisterRequestMessage {
  constructor(
    public email: string,
    public password: string,
    public confirmPassword: string) { }
}
