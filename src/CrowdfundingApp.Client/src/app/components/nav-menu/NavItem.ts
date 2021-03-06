export class NavItem {
  constructor(
    public value: string,
    public route: string,
    public ico?: string,
    public show?: boolean,
  ) {
    this.show = this.show === undefined ? false : show;
  }
}