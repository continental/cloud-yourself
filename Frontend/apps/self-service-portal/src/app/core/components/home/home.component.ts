import { Component, OnDestroy } from '@angular/core';
import { ResourceBase } from 'fancy-hateoas-client';
import { AppComponent } from '../../../app.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {

  viewModel: any = null;

  constructor(public appComponent: AppComponent) {
    appComponent.viewModel$.subscribe(appComponentVm => {
      appComponentVm.fetch_homeVm().then((vm: any) => {
        this.viewModel = vm;
      })
    });
  }
}
