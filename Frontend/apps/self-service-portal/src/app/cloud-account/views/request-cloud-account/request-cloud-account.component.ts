import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-request-cloud-account',
  templateUrl: './request-cloud-account.component.html'
})
export class RequestCloudAccountComponent extends ViewBase {

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }
  
  _onSave() {
    if(this.viewModel) {
      this.viewModel.template.create().then((resourceUrl: string) => {
        this.router.navigate(['/cloud-accounts/show', resourceUrl[0]]);
      });
    } 
  }

}
