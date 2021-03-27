import { FileInput } from 'src/app/components/inputs/file-input/FileInput';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { DecimalInput } from 'src/app/components/inputs/decimal-input/DecimalInput';
import { TextArea } from 'src/app/components/inputs/text-area/TextArea';

export class RewardInputs {

  public image = new FileInput('Загрузить изображение', 'fas fa-download');

  public name: TextInput = {
    label: 'Название вознаграждения',
    placeholder: 'Введите название вознаграждения'
  };

  public cost: DecimalInput = {
    label: 'Стоимость вознаграждения',
    placeholder: 'Введите стоимость (BYN)',
    min: 1
  };

  public countRestrictions: DecimalInput = {
    placeholder: 'Введите количество вознаграждений',
    min: 1
  };

  public description: TextArea = {
    label: 'Описание вознаграждения',
    placeholder: 'Введите описание вознаграждения'
  };

  public wholeWorldDeliveryCost: DecimalInput = {
    placeholder: 'Введите стоимость доставки по всему миру (BYN)',
    min: 1
  };
}
