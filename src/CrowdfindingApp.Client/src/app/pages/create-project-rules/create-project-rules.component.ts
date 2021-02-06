import { Component, OnInit } from '@angular/core';
import { RuleCard } from './RuleCard';

@Component({
  selector: 'app-create-project-rules',
  templateUrl: './create-project-rules.component.html',
  styleUrls: ['./create-project-rules.component.css']
})
export class CreateProjectRulesComponent implements OnInit {

  constructor() { }

  public rules: RuleCard[] = [
    new RuleCard('Качественное оформление', 'Текст описания должен быть написан грамотным языком и четко структурирован. Также обязательным является наличие качественных фотоматериалов, иллюстраций или инфографики. Желательно наличие видео-обращения.', 'assets/img/artist.png'),
    new RuleCard('Актуальные подарки', 'Список подарков для спонсоров должен быть тщательно продуман и напрямую связан с результатами проекта. У проектов с отстраненными подарками нет шансов на успех.', 'assets/img/gift.png'),
    new RuleCard('Обоснованный бюджет', 'Бюджет проекта не должен быть раздут. Он должен включать в себя тот минимум, который необходим для успешной реализации проекта.', 'assets/img/budget.png'),
    new RuleCard('Соответствие закону и правилам', 'Проект не должен нарушать законов РБ и должен соответствовать правилам платформы PolessCrowdFinding.', 'assets/img/law.png'),
  ];

  ngOnInit() {
    console.log(this.rules)
  }

}
