import { RewardInfo } from '../rewards/RewardInfo';
import { QuestionInfo } from './QuestionInfo';
import { ProjectStatusEnum } from '../../enums/ProjectStatus';

export class ProjectInfo {
  public id?: string;
  public ownerId?: string;
  public categoryId?: string;
  public title?: string;
  public shortDescription?: string;
  public fullDescription?: string;
  public location?: string;
  public videoUrl?: string;
  public image?: string;
  public startDateTime?: Date;
  public duration?: number;
  public budget?: number;
  public authorSurname?: string;
  public authorName?: string;
  public authorDateOfBirth?: Date;
  public authorMiddleName?: string;
  public authorPersonalNo?: string;
  public authorIdentificationNo?: string;
  public whomGivenDocument?: string;
  public whenGivenDocument?: Date;
  public authorAddress?: string;
  public authorPhone?: string;
  public rewards?: RewardInfo[];
  public questions?: QuestionInfo[];
  public status?: ProjectStatusEnum;
}
