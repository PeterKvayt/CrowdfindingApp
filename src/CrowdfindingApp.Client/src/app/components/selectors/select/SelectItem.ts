export class SelectItem {
  constructor(
    public value: string,
    public name: string,
    public disabled?: boolean,
    public selected?: boolean
  ) { }
}
