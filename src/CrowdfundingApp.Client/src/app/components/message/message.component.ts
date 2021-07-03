import { Component, OnInit } from '@angular/core';
import { MessageService } from 'src/app/services/message.service';
import { ErrorInfo } from 'src/app/models/common/ErrorInfo';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {

  constructor(
    public messageService: MessageService
  ) { }

  ngOnInit() {
  }

  public removeMessage(error: ErrorInfo): void {
    this.messageService.errors.remove(error);
  }

}
