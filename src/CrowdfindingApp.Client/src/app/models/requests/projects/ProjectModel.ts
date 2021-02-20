export class ProjectModel {

  constructor(name: string, description: string, categoryId: string, img: string, purpose: number, result: number, id?: string) {
    this.CategoryId = categoryId;
    this.CurrentResult = result;
    this.Description = description;
    this.ImgPath = img;
    this.FinancialPurpose = purpose;
    this.Name = name;
    this.Id = id;
  }
    public Id?: string; 
    public Name: string;
    public Description: string;
    public CategoryId: string;
    public ImgPath: string;
    public FinancialPurpose: number;
    public CurrentResult: number;
}