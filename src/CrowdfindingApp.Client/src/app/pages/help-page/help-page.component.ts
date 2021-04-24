import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { GenericLookupItem } from 'src/app/models/common/GenericLookupItem';
import { Routes } from 'src/app/models/immutable/Routes';

@Component({
  selector: 'app-help-page',
  templateUrl: './help-page.component.html',
  styleUrls: ['./help-page.component.css']
})
export class HelpPageComponent extends Base implements OnInit {

  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    private titleService: Title
  ) { super(router, activatedRoute); }

    public platformName = 'Crowdfinding app';
    public maxValue = '4 947';
    public tax = '13%';
    public profitPercent = '10%';

    public helpRoute = Routes.help;

  public tabs: GenericLookupItem<boolean, string>[] = [
    {key: true, value: 'Как работает платформа'}, // 0
    {key: false, value: 'О платформе'}, // 1
    {key: false, value: 'Правила платформы'}, // 2
    {key: false, value: 'Trust & Safety'}, // 3
    {key: false, value: 'FAQ'}, // 4
    {key: false, value: 'Начало'}, // 5
    {key: false, value: 'Оформление проекта'}, // 6
    {key: false, value: 'Вознаграждения'}, // 7
    {key: false, value: 'Бюджет проекта'}, // 8
    {key: false, value: 'Стратегия продвижения'}, // 9
    {key: false, value: 'Сбор средств'}, // 10
    {key: false, value: 'Заключение'}, // 11
  ];

  ngOnInit() {
    const currentTabIndex = this.activatedRoute.snapshot.queryParams.tab;
    if (currentTabIndex && this.tabs[currentTabIndex]) {
      this.onTabClick(this.tabs[currentTabIndex]);
    }
  }

  onTabClick(tab: GenericLookupItem<boolean, string>) {
    if (tab.key) { return; }
    this.tabs.find(x => x.key === true).key = false;
    tab.key = true;
  }

}
