import { RewardInfo } from '../rewards/RewardInfo';
import { QuestionInfo } from './QuestionInfo';

export class DraftProjectInfo {
  public id: string;
  public categoryId: string;
  public title: string;
  public shortDescription: string;
  public fullDescription: string;
  public location: string;
  public videoUrl: string;
  public image: string;
  public startDateTime: Date;
  public duration: number;
  public budget: number;
  public authorSurname: string;
  public authorName: string;
  public authorDateOfBirth: string;
  public authorMiddleName: string;
  public authorPersonalNumber: string;
  public whomGivenDocument: string;
  public whenGivenDocument: Date;
  public authorAddress: string;
  public authorPhone: string;
  public rewards: RewardInfo[];
  public questions: QuestionInfo[];
}
