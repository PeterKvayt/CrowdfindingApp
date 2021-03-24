import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from 'src/app/services/auth.service';
import { ReplyMessage } from 'src/app/models/replies/common/ReplyMessage';
import { UserInfo } from 'src/app/models/replies/users/UserInfo';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { FileService } from 'src/app/services/file.service';
import { FileInput } from 'src/app/components/inputs/file-input/FileInput';
import { PasswordInput } from 'src/app/components/inputs/password-input/PasswordInut';
import { UpdateUserRequestMessage } from 'src/app/models/requests/users/UpdateUserRequestMessage';

@Component({
  selector: 'app-profile-settings',
  templateUrl: './profile-settings.component.html',
  styleUrls: ['./profile-settings.component.css']
})
export class ProfileSettingsComponent extends Base implements OnInit {

  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public userService: UserService,
    private titleService: Title,
    public authService: AuthenticationService,
    public fileService: FileService
  ) { super(router, activatedRoute); }

  public userInfo: UserInfo;
  public surnameInput: TextInput = {placeholder: 'Введите фамилию', label: 'Фамилия'};
  public nameInput: TextInput = {placeholder: 'Введите имя', label: 'Имя'};
  public middleNameInput: TextInput = {placeholder: 'Введите отчество', label: 'Отчество'};
  public emailInput: TextInput = {placeholder: 'Введите почту', label: 'Email'};
  public currentPasswordInput: PasswordInput = {placeholder: 'Введите текущий пароль', label: 'Текущий пароль'};
  public newPasswordInput: PasswordInput = {placeholder: 'Введите новый пароль', label: 'Новый пароль'};
  public confirmPasswordInput: PasswordInput = {placeholder: 'Подтвердите новый пароль', label: 'Подтвердите пароль'};
  public imageInput = new FileInput('Загрузить', 'fas fa-upload');

  ngOnInit() {
    this.titleService.setTitle('Настройки профиля');
    this.setUserInfo();
  }

  setUserInfo() {
    this.showLoader = true;
    this.subscriptions.add(
      this.userService.getUserInfo().subscribe(
        (reply: ReplyMessage<UserInfo>) => {
          this.userInfo = reply.value;

          this.surnameInput.value = this.userInfo.surname;
          this.nameInput.value = this.userInfo.name;
          this.middleNameInput.value = this.userInfo.middleName;
          this.emailInput.value = this.userInfo.email;

          this.showLoader = false;
        },
        () => { this.showLoader = false; }
      )
    );
  }

  onSaveClick() {
    this.showLoader = true;
    const request: UpdateUserRequestMessage = {
      name: this.nameInput.value,
      surname: this.surnameInput.value,
      middleName: this.middleNameInput.value,
      image: this.imageInput.fileName,
      email: this.emailInput.value,
      currentPassword: this.currentPasswordInput.value,
      newPassword: this.newPasswordInput.value,
      confirmPassword: this.confirmPasswordInput.value
    };
    this.subscriptions.add(
      this.userService.updateUserInfo(request).subscribe(
        () => {
          this.showLoader = false;
        },
        () => { this.showLoader = false; }
      )
    );
  }

  onUploadImgClick() {
    this.showLoader = true;
    const data = new FormData();
    data.append('file', this.imageInput.file);
    this.subscriptions.add(
    this.fileService.save(data).subscribe(
        (reply: ReplyMessage<string>) => {
          this.userInfo.photo = reply.value;
          this.imageInput.fileName = reply.value;
          this.showLoader = false;
        },
        () => {this.showLoader = false; }
      )
    );
  }

}
