export class ChangePasswordRequestMessage {
  constructor(
    public oldPassword: string,
    public newPassword: string,
    public confirmPassword: string) { }
}
