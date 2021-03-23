import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { RewardService } from 'src/app/services/reward.service';
import { ReplyMessage } from 'src/app/models/replies/common/ReplyMessage';
import { RewardInfo } from 'src/app/models/replies/rewards/RewardInfo';
import { RewardCard } from 'src/app/components/reward-card/RewardCard';

@Component({
  selector: 'app-rewards-page',
  templateUrl: './rewards-page.component.html',
  styleUrls: ['./rewards-page.component.css']
})
export class RewardsPageComponent extends Base implements OnInit {

  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    private titleService: Title,
    private rewardService: RewardService,
  ) { super(router, activatedRoute); }

  public projectId: string;
  public rewards: RewardInfo[] = [];

  ngOnInit() {
    this.titleService.setTitle('Вознаграждения');
    this.projectId = this.activatedRoute.snapshot.params.projectId;
    this.setRewards();
  }
  
  setRewards() {
    this.showLoader = true;
    console.log('asdas');
    this.subscriptions.add(
      this.rewardService.getRewardsByProjectId(this.projectId).subscribe(
        (reply: ReplyMessage<RewardInfo[]>) => {
          console.log(reply);
          this.rewards = reply.value;
          this.showLoader = false;
        },
        () => { this.showLoader = false; }
      )
    );
  }

  getCard(reward: RewardInfo): RewardCard {
    return new RewardCard(
      reward.title,
      reward.price,
      reward.description,
      reward.image,
      reward.deliveryType,
      reward.deliveryDate,
      reward.limit
    );
  }

  onBuyClick(reward: RewardInfo) {
    console.log(reward.id);
  }

}
