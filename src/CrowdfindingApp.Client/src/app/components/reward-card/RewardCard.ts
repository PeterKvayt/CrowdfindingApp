export class RewardCard {
  constructor(
    public name: string,
    public price: number,
    public description: string,
    public image: string,
    public deliveryMonth: string,
    public deliveryYear: string,
    public deliveryType: string,
    public availableCount?: number
  ) { }
}
