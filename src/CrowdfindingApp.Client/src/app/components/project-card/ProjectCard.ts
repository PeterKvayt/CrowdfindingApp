import { ProjectStatusEnum } from 'src/app/models/enums/ProjectStatus';

export class ProjectCard {

constructor(
  public status: ProjectStatusEnum,
  public name: string,
  public description: string,
  public categoryName: string,
  public categoryId: string,
  public imgPath: string,
  public purpose: number,
  public currentResult: number,
  public id: string) {}
}
