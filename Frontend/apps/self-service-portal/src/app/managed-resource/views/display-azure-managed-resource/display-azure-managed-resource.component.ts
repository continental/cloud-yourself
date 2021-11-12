import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-display-azure-managed-resource',
  templateUrl: './display-azure-managed-resource.component.html'
})
export class DisplayAzureManagedResourceComponent extends ViewBase {

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }
  
  _onDelete() {
    this.viewModel.delete().then(() => {
      this.router.navigate(['/managed-resources/list', this.viewModel._links.listManagedResourcesVm.href]);
    });
  }

}
