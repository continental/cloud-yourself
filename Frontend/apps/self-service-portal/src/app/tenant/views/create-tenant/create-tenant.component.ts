import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient, ResourceBase, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-create-tenant',
  templateUrl: './create-tenant.component.html'
})
export class CreateTenantComponent extends ViewBase {

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }

  _onSave() {
    if(this.viewModel) {
      this.viewModel.create().then((resourceUrl: string) => {
        this.router.navigate(['/tenants/show', resourceUrl[0]]);
      });
    } 
  }

}
