import { Component, OnInit, Input } from '@angular/core';
import { CategoryItem } from './CategoryItem';

@Component({
  selector: 'app-category-item',
  templateUrl: './category-item.component.html',
  styleUrls: ['./category-item.component.css']
})
export class CategoryItemComponent implements OnInit {

  constructor() { }

  public className = 'category';
  @Input() item: CategoryItem;
  ngOnInit() {
  }

  getClass(): string {
    if (this.item.active) {
      return 'active-category';
    } else {
      return 'category';
    }
  }

  onCategoryClick() {
    this.item.active = !this.item.active;
    this.className = this.getClass();
  }
}
