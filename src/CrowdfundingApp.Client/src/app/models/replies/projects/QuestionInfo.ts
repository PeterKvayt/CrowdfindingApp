export class QuestionInfo {
  constructor(
    public question: string,
    public answer: string,
    public projectId?: string,
    public id?: string
  ) { }
}