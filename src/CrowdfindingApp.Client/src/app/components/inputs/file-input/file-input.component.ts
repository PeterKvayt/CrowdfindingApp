import { Component, OnInit, OnDestroy, Input, Output } from '@angular/core';
import { FileInput } from './FileInput';
import { EventEmitter } from 'events';
import { FileService } from 'src/app/services/file.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-file-input',
  templateUrl: './file-input.component.html',
  styleUrls: ['./file-input.component.css']
})
export class FileInputComponent implements OnInit, OnDestroy {

  public subscriptions = new Subscription();
  @Input() item: FileInput;

  @Output() valueChange = new EventEmitter();

  constructor(
  ) { }

  ngOnInit() {
  }

  public ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  public onChange(files): void {
    this.item.file = files[0] as File;
    this.valueChange.emit('');
  }

  onUploadClick(fileInput) {
    fileInput.click();
  }
}
