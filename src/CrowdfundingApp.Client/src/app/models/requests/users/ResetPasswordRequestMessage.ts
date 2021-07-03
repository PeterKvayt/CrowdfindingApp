export class ResetPasswordRequestMessage {
  constructor(
    public token: string,
    public password: string,
    public confirmPassword: string) { }
}
