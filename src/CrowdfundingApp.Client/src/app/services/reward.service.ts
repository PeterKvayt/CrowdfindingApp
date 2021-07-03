import { Injectable } from '@angular/core';
import { HttpService } from './http.service';

@Injectable()
export class RewardService {
  constructor(
    private http: HttpService
  ) { }

    private controller = 'rewards/';

  public getRewardsByProjectId(projectId: string) {
    return this.http.get(this.controller + 'GetByProjectId/' + projectId);
  }

  public getPublicById(rewardId: string) {
    return this.http.get(this.controller + rewardId);
  }
}
