import { Injectable } from '@angular/core';
import { ErrorInfo } from '../models/common/ErrorInfo';

@Injectable()
export class MessageService {
  public errors: ErrorInfo[] = [];

  public addErrorRange(range: ErrorInfo[]): void {
    range.forEach(error => {
      this.errors.push(error);
    });
  }

  public clearAllMessages(): void {
    this.errors = [];
  }
}
