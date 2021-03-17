import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { TextArea } from '../../inputs/text-area/TextArea';
import { TextInput } from '../../inputs/text-input/TextInput';
import { AuthenticationService } from 'src/app/services/auth.service';
import { SelectInput } from '../../selectors/select/SelectInput';
import { SelectItem } from '../../selectors/select/SelectItem';

@Component({
  selector: 'app-feedback-modal',
  templateUrl: './feedback-modal.component.html',
  styleUrls: ['./feedback-modal.component.css']
})
export class FeedbackModalComponent implements OnInit {

  constructor(
    private authService: AuthenticationService
  ) { }

  public name: string;
  public email: string;
  @Input() show: boolean;
  @Output() showChange = new EventEmitter();

  public select: SelectInput = {
    list: [
      new SelectItem('Служба поддержки', 'Служба поддержки'),
      new SelectItem('Жалоба', 'Жалоба'),
      new SelectItem('Предложение', 'Предложение'),
      new SelectItem('Другое', 'Другое')
    ],
    defaultValue: 'Выберите тему вопроса'
  };

  public questionInput: TextArea = { placeholder: 'Введите вопрос' };
  public nameInput: TextInput = { placeholder: 'Как к вам обращаться' };
  public emailInput: TextInput = { placeholder: 'Контактный email' };
  private topic: string;

  public ngOnInit(): void {
    if (this.authService.isAuthenticated()) {

    } else {
      
    }
    this.nameInput.value = this.name ? this.nameInput.value : undefined;
    this.emailInput.value = this.email ? this.emailInput.value : undefined;
  }

  public onSelectTopic(topic: string): void {
    this.topic = topic;
  }

  public close(): void {
    this.show = false;
    this.showChange.emit();
  }

  public onAskClick(): void {
    this.close();
  }
}
