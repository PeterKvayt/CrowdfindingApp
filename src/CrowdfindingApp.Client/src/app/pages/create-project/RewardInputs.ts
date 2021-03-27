import { FileInput } from 'src/app/components/inputs/file-input/FileInput';
import { TextInput } from 'src/app/components/inputs/text-input/TextInput';
import { DecimalInput } from 'src/app/components/inputs/decimal-input/DecimalInput';
import { TextArea } from 'src/app/components/inputs/text-area/TextArea';

export class RewardInputs {

  public image = new FileInput('Загрузить изображение', 'fas fa-download');

  public name: TextInput = {
    placeholder: 'Введите название вознаграждения'
  };

  public cost: DecimalInput = {
    placeholder: 'Введите стоимость (BYN)',
    min: 1
  };

  public countRestrictions: DecimalInput = {
    placeholder: 'Введите количество',
    min: 1
  };

  public description: TextArea = {
    placeholder: 'Введите описание вознаграждения'
  };

  public wholeWorldDeliveryCost: DecimalInput = {
    placeholder: 'Введите стоимость доставки по всему миру (BYN)',
    min: 1
  };
}
