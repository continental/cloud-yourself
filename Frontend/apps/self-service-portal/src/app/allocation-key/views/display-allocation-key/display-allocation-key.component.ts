import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient } from 'fancy-hateoas-client';
import { ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-display-allocation-key',
  templateUrl: './display-allocation-key.component.html'
})
export class DisplayAllocationKeyComponent extends ViewBase {

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }
  
  _onDelete() {
    this.viewModel.delete().then(() => {
      this.router.navigate(['/cloud-accounts/show', this.viewModel._links.displayCloudAccountVm.href]);
    });
  }
  
}
