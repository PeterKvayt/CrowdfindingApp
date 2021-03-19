import { Component, OnInit } from '@angular/core';
import { RuleCard } from './RuleCard';
import { AuthenticationService } from 'src/app/services/auth.service';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { Base } from '../Base';
import { Routes } from 'src/app/models/immutable/Routes';

@Component({
  selector: 'app-create-project-rules',
  templateUrl: './create-project-rules.component.html',
  styleUrls: ['./create-project-rules.component.css']
})
export class CreateProjectRulesComponent extends Base implements OnInit  {

  constructor(
    private authService: AuthenticationService,
    private titleService: Title,
    public router: Router,
    public activatedRoute: ActivatedRoute,

  ) { super(router, activatedRoute); }

  public requirements: RuleCard[] = [
    new RuleCard('Качественное оформление', 'Текст описания должен быть написан грамотным языком и четко структурирован. Также обязательным является наличие качественных фотоматериалов, иллюстраций или инфографики. Желательно наличие видео-обращения.', 'assets/img/artist.png'),
    new RuleCard('Актуальные подарки', 'Список подарков для спонсоров должен быть тщательно продуман и напрямую связан с результатами проекта. У проектов с отстраненными подарками нет шансов на успех.', 'assets/img/gift.png'),
    new RuleCard('Обоснованный бюджет', 'Бюджет проекта не должен быть раздут. Он должен включать в себя тот минимум, который необходим для успешной реализации проекта.', 'assets/img/budget.png'),
    new RuleCard('Соответствие закону и правилам', 'Проект не должен нарушать законов РБ и должен соответствовать правилам платформы PolessCrowdFinding.', 'assets/img/law.png'),
  ];

  public authorsRequirements: RuleCard[] = [
    new RuleCard('Возраст', 'На момент создания проекта автору должно быть не менее 18 лет.', 'assets/img/artist.png'),
    new RuleCard('Гражданство', 'Автор должен являтся гражданином Республики Беларусь.', 'assets/img/gift.png'),
    new RuleCard('Банковский счет', 'Автор должен иметь расчетный счет в любом банке Республики Берарусь.', 'assets/img/budget.png'),
  ];

  public projectCreationProcesess: RuleCard[] = [
    new RuleCard('1. Создание', 'Оформление проекта с помощью «редактора проектов».', 'assets/img/artist.png'),
    new RuleCard('2. Модерация', 'Модерация оформленного проекта.', 'assets/img/gift.png'),
    new RuleCard('3. Заключение договора', 'Подписание с автором договоров на публикацию проекта на платформе и сбор средств.', 'assets/img/budget.png'),
    new RuleCard('4. Публикация', 'Размещение проекта на сайте.', 'assets/img/budget.png'),
  ];

  ngOnInit() {
    this.titleService.setTitle('Правила создания проекта');
  }

  public onCreateProjectClick(): void {
    if (this.authService.isAuthenticated()) {
      this.redirect(Routes.projectCreate);
    } else {
      this.redirect(Routes.signIn);
    }
  }

}
