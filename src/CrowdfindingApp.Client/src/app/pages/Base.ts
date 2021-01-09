import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { OnDestroy } from '@angular/core';

export class Base implements OnDestroy {

  constructor(
    public router: Router,
    public activeatedRoute: ActivatedRoute
  ) { }

  public subscriptions = new Subscription();

  private removeByProp<TArray, TValue>(array: TArray[], propName: string, value: TValue): void {
    for (let index = 0; index < array.length; index++) {
      if (array[index]
          && array[index].hasOwnProperty(propName)
          && (arguments.length > 2 && array[index][propName] === value )
        ) {
        array.splice(index, 1);
      }
    }
  }

  public redirect(route: string): void {
    this.router.navigate([route]);
  }

  public handleError(error: any): void {
    this.router.navigate(['error/' + this.activeatedRoute.snapshot.params['status'],
      {
        status: error.status,
        message: error.message
      }
    ]);
  }

  public ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}