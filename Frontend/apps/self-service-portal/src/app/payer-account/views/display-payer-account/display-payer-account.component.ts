import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-display-payer-account',
  templateUrl: './display-payer-account.component.html'
})
export class DisplayPayerAccountComponent extends ViewBase {

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }
  
  _onDelete() {
    this.viewModel.delete().then(() => {
      this.router.navigate(['/payer-accounts/list', this.viewModel._links.listPayerAccountsVm.href]);
    });
  }
  
}
