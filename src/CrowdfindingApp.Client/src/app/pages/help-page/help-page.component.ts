import { Component, OnInit } from '@angular/core';
import { Base } from '../Base';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { GenericLookupItem } from 'src/app/models/common/GenericLookupItem';

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

  public tabs: GenericLookupItem<boolean, string>[] = [
    {key: false, value: 'Как работает платформа'},
    {key: false, value: 'О платформе'},
    {key: false, value: 'Парвила платформы'},
    {key: false, value: 'Trust & Safety'},
    {key: false, value: 'FAQ'},
    {key: true, value: 'Введение'},
    {key: false, value: 'Оформление проекта'},
    {key: false, value: 'Вознаграждения'},
    {key: false, value: 'Бюджет проекта'},
    {key: false, value: 'Стратегия продвижения'},
    {key: false, value: 'Сбор средств'},
    {key: false, value: 'Заключение'},
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
