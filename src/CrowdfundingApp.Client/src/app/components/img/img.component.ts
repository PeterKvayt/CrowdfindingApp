import { Component, OnInit, Input } from '@angular/core';
import { FileService } from 'src/app/services/file.service';
import { FileInput } from '../inputs/file-input/FileInput';

@Component({
  selector: 'app-img',
  templateUrl: './img.component.html',
  styleUrls: ['./img.component.css']
})
export class ImgComponent implements OnInit {

  @Input() input: FileInput;
  @Input() src: string;
  @Input() alt: string;
  @Input() default: string;

  constructor(
    public fileService: FileService
  ) { }

  ngOnInit() {
  }

  getUrl() {
    if (this.input && this.input.fileName && !this.input.fileName.includes('stock')) {
      return this.fileService.absoluteFileStoragePath + this.input.fileName;
    } else if (this.src) {
      return this.fileService.absoluteFileStoragePath + this.src;
    } else {
      return this.default;
    }
  }
}
