export class GenericLookupItem<TKey, TValue> {

  constructor(
    public key: TKey,
    public value: TValue
  ) { }
}
