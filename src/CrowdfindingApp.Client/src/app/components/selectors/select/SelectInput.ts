import { SelectItem } from './SelectItem';

export class SelectInput {

  constructor(
    public list: SelectItem[],
    public defaultValue?: string,
    public currentValue?: SelectItem
  ) { }
}
