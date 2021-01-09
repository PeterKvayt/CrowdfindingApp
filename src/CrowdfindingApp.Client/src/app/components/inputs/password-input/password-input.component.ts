import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { PasswordInput } from './PasswordInut';

@Component({
  selector: 'app-password-input',
  templateUrl: './password-input.component.html',
  styleUrls: ['./password-input.component.css']
})
export class PasswordInputComponent implements OnInit {

  @Input() item: PasswordInput;

  @Output() valueChange = new EventEmitter<PasswordInput>();

  public hidePassword = true;
  public inputType = 'password';

  public onValueChange(value: string): void {
      this.item.value = value;
      this.valueChange.emit(this.item);
  }

  public ngOnInit(): void {
    this.item.valid = this.item.valid === undefined ? true : this.item.valid;
    // this.item.min = this.item.min === undefined ? 0 : this.item.min;
    // this.item.max = this.item.max === undefined ? Number.MAX_SAFE_INTEGER : this.item.max;
    // this.item.placeholder = this.item.placeholder === undefined ? this.item.label : this.item.placeholder;
  }

  public onEyeClick(): void {
    this.hidePassword = !this.hidePassword;
    if (this.hidePassword) {
      this.inputType = 'password';
    } else {
      this.inputType = 'text';
    }
  }
}
