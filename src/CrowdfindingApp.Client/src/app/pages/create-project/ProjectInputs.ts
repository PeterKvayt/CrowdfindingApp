import { FileInput } from 'src/app/components/inputs/file-input/FileInput';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { TextArea } from 'src/app/components/inputs/text-area/TextArea';
import { DecimalInput } from 'src/app/components/inputs/decimal-input/DecimalInput';
import { DateInput } from 'src/app/components/inputs/date-input/DateInput';
import { SelectInput } from 'src/app/components/selectors/select/SelectInput';

export class ProjectInputs {
  private currentDate = new Date();

  public image = new FileInput('Загрузить изображение', 'fas fa-upload');

  public name: TextInput = {
    placeholder: 'Введите название проекта'
  };

  public shortDescription: TextArea = {
    placeholder: 'Кратко опишите проект (до 280 символов)',
    max: 280
  };

  public description: TextArea = {
    placeholder: 'Введите полное описание проекта ...'
  };

  public video: TextInput = {
    placeholder: 'Введите ссылку на видео'
  };

  public purpose: DecimalInput = {
    placeholder: 'Введите финансовую цель (BYN)',
    min: 1
  };

  public duration: DecimalInput = {
    placeholder: 'Введите продолжительность проекта в днях',
    min: 1
  };

  public ownerSurname: TextInput = {
    placeholder: 'Фамилия'
  };

  public ownerName: TextInput = {
    placeholder: 'Имя'
  };

  public ownerMiddleName: TextInput = {
    placeholder: 'Отчество'
  };

  public ownerPassportNumber: TextInput = {
    placeholder: 'Серия и номер',
    label: 'Паспортные данные',
    min: 9, max: 9,
    example: 'Пример: AB1234567'
  };

  public ownerPrivateNumber: TextInput = {
    placeholder: 'Личный номер',
    min: 14,
    max: 14,
    example: 'Пример: 1234567A012PB0'
  };

  public ownerWhomIssuedDoc: TextInput = {
    placeholder: 'Кем  выдан документ',
    example: 'Пример: Советское РУВД г.Минска'
  };

  public ownerPhoneNumber: TextInput = {
    placeholder: 'Контактный номер',
    min: 12,
    max: 12,
    example: 'Пример: 375291234567'
  };

  public ownerAddress: TextInput = {
    placeholder: 'Адрес регистрации',
    example: 'Пример: г.Минск, ул. Е. Полоцкой, д.3, кв. 16'
  };

  public ownerWhenIssuedDoc: DateInput = {
    label: 'Дата выдачи документа',
    max: this.currentDate,
    min: new Date(this.currentDate.getFullYear() - 50, this.currentDate.getMonth(), this.currentDate.getDay()),
    value: this.currentDate
  };

  public ownerBirthday: DateInput = {
    label: 'Дата рождения',
    max: new Date(this.currentDate.getFullYear() - 18, this.currentDate.getMonth(), this.currentDate.getDay()),
    min: new Date(this.currentDate.getFullYear() - 90, this.currentDate.getMonth(), this.currentDate.getDay()),
    value: new Date(this.currentDate.getFullYear() - 18, this.currentDate.getMonth(), this.currentDate.getDay())
  };

  public categorySelect: SelectInput = {
    list: [],
    defaultValue: 'Выберите категорию',
  };
}
