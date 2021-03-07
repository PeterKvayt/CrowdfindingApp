import { Component, OnInit, Input } from '@angular/core';
import { RewardCard } from './RewardCard';

@Component({
  selector: 'app-reward-card',
  templateUrl: './reward-card.component.html',
  styleUrls: ['./reward-card.component.css']
})
export class RewardCardComponent implements OnInit {

  @Input() item: RewardCard;
  @Input() editable: boolean;

  constructor() { }

  ngOnInit() {
  }

}
