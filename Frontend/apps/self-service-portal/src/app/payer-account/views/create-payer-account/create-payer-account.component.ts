import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient } from 'fancy-hateoas-client';
import { ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-create-payer-account',
  templateUrl: './create-payer-account.component.html'
})
export class CreatePayerAccountComponent extends ViewBase {

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }
  
  _onSave() {
    if(this.viewModel) {
      this.viewModel.create().then((resourceUrl: string) => {
        this.router.navigate(['/payer-accounts/display', resourceUrl[0]]);
      });
    } 
  }
  
}
